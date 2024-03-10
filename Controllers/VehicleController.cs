using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto;
using vyroute.Dto;
using vyroute.Models;
using vyroute.Services;

namespace vyroute.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;
        private readonly IMapper _mapper;

        public VehicleController(IVehicleService vehicleService, IMapper mapper)
        {
            _vehicleService = vehicleService;
            _mapper = mapper;
        }
        [HttpGet("find")]
        public async Task<IActionResult> GetAllVehicles()
        {
            var vehicles = await _vehicleService.GetAllVehiclesAsync();
            return Ok(vehicles);
        }
        [HttpGet("withtransits/find")]
        public IActionResult GetAllVehiclesWithTransits()
        {
            var vehicles = _vehicleService.GetAllVehiclesWithTransits();
            return Ok(_mapper.Map<IEnumerable<VehicleResponseDTO>>(vehicles));
        }
        [HttpGet("available/find")]
        public IActionResult GetAvailableVehicleForTransit([FromQuery] Guid terminalID)
        {
            var vehicle =  _vehicleService.GetAvailableVehicleForTransit(terminalID);
            return Ok(_mapper.Map<VehicleResponseDTO>(vehicle));
        }

        [HttpGet("find/{vehicleId}")]
        public async Task<IActionResult> GetVehicleById(Guid vehicleId)
        {
            var vehicle = await _vehicleService.GetVehicleByIdAsync(vehicleId);
            if (vehicle == null)
            {
                return NotFound();
            }

            return Ok(vehicle);
        }

        [HttpPost("create")]
        public async Task<IActionResult> AddVehicleAsync([FromForm] VehicleInput newvehicle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            foreach (var image in newvehicle.Images)
            {
                if (image.Length > 0)
                {
                    string randomString = GenerateRandomString(3);
                    var fileName = newvehicle.VehicleNumber + randomString + Path.GetExtension(image.FileName);
                    var directoryPath = Path.Combine("wwwroot", "images", "vehicles", newvehicle.VehicleType.ToString());
                    var imageURL = Path.Combine("images", "vehicles", newvehicle.VehicleType.ToString(), fileName);
                    var filePath = Path.Combine(directoryPath, fileName);

                    // Check if the directory exists, create if not
                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }

                    // Check if the file already exists
                    if (!System.IO.File.Exists(filePath))
                    {
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await image.CopyToAsync(stream);
                        }

                        newvehicle.ImagePaths ??= [];

                        newvehicle.ImagePaths.Add(imageURL);
                    }
                    else
                    {
                        return Conflict("Image already Exists");
                    }
                }
            }


            var vehicleData = new Vehicle
            {
                VehicleNumber = newvehicle.VehicleNumber,
                VehicleColor = newvehicle.VehicleColor,
                VehicleType = newvehicle.VehicleType,
                Seats = newvehicle.Seats,
                Status = newvehicle.Status,
                ImagePaths = newvehicle.ImagePaths,
                TerminalId = newvehicle.TerminalId,
                TransporterId = newvehicle.TransporterId,
                VehiclePassengerCap = newvehicle.VehiclePassengerCap
                
            };

             _vehicleService.AddVehicle(vehicleData);
            // Return appropriate response, e.g., CreatedAtAction
            return Ok(newvehicle);
        }

        private string GenerateRandomString(int length)
        {
            const string chars = "0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        [HttpPut("update/{userId}")]

        public IActionResult UpdateVehicle(Guid vehicleId, [FromBody] Vehicle updatedVehicle)
        {
            var existingVehicle = _vehicleService.GetVehicleByIdAsync(vehicleId).Result;
            if (existingVehicle == null)
            {
                return NotFound();
            }

            // Perform updates on existingUser based on updatedUser
            existingVehicle.VehicleNumber = updatedVehicle.VehicleNumber;

            _vehicleService.UpdateVehicle(existingVehicle);
            // Return appropriate response, e.g., Ok
            return Ok();
        }

        [HttpDelete("{userId}")]
        public IActionResult DeleteVehicle(Guid vehicleId)
        {
            var vehicleToDelete = _vehicleService.GetVehicleByIdAsync(vehicleId).Result;
            if (vehicleToDelete == null)
            {
                return NotFound();
            }

            _vehicleService.DeleteVehicle(vehicleToDelete);
            // Return appropriate response, e.g., Ok
            return Ok();
        }
    }
}
