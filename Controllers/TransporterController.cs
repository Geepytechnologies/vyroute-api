using Microsoft.AspNetCore.Mvc;
using vyroute.Dto;
using vyroute.Models;
using vyroute.Services;

namespace vyroute.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransporterController : ControllerBase
    {
        private readonly ITransporterService _transporterService;

        public TransporterController(ITransporterService transporterservice)
        {
            _transporterService = transporterservice;
        }


        [HttpGet("find")]
        public async Task<IActionResult> GetAllTransporters()
        {
           var result =  await _transporterService.GetAllTransporters();
            return Ok(result);
        }

        [HttpGet("find/{id}")]
        public async Task<IActionResult> GetTransporter(Guid id)
        {
            var result = await _transporterService.FindTransporterByID(id);
            return Ok(result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> AddATransporter([FromForm] TransporterInput transporter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(transporter.Images == null)
            {
                return BadRequest("Image is Null");
            }
            foreach (var image in transporter.Images)
            {
                if (image.Length > 0)
                {
                    string randomString = GenerateRandomString(3);
                    var fileName = transporter.FirstName + "_" + transporter.LastName + randomString + Path.GetExtension(image.FileName);
                    var directoryPath = Path.Combine("wwwroot", "images", "transporters");
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

                        transporter.ImagePaths ??= [];

                        transporter.ImagePaths.Add(filePath);
                    }
                    else
                    {
                        return Conflict("Image already Exists");
                    }
                }
            }
            var transporterData = new Transporter
            {
                FirstName = transporter.FirstName,
                LastName = transporter.LastName,
                MiddleName = transporter.MiddleName,
                Birthyear = transporter.Birthyear,
                State = transporter.State,
                Lga = transporter.Lga,
                Images = transporter.ImagePaths
            };

            var res = _transporterService.AddTransporter(transporterData);

            return Ok(res);
            
        }

        [HttpPut("update/{id}")]
        public void UpdateATransporter(Guid id, [FromBody] string value)
        {
        }

        [HttpDelete("delete/{id}")]
        public void Delete(int id)
        {
        }

        private string GenerateRandomString(int length)
        {
            const string chars = "0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
