using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using vyroute.Dto;
using vyroute.Models;
using vyroute.Services;


namespace vyroute.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransitController : ControllerBase
    {
        private readonly ITransitService _transitService;
        private readonly IMapper _mapper;

        public TransitController(ITransitService transitService, IMapper mapper)
        {
            _transitService = transitService;
            _mapper = mapper;
        }

        // GET: api/<TransitController/find
        [HttpGet("find")]
        public async Task<IActionResult> GetAllTransits()
        {
            var result = await _transitService.GetAllTransits();
            return Ok(result);
        }

        // GET api/<TransitController>/find/5
        [HttpGet("find/{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                var transit = await _transitService.GetTransitById(id);
                return Ok(transit);
            }
            catch (Exception ex)
            {
                var errorResponse = new ErrorResponseDto
                {
                    StatusCode = 500,
                    Message = "Internal Server Error: " + ex.Message
                };

                return StatusCode(500, errorResponse);
            }
        }
        // GET api/<TransitController>/available/find

        [HttpGet("available/find")]
        public async Task<IActionResult> GetTransitByDepartureDate([FromQuery] DateOnly departure, [FromQuery] Guid TerminalID)
        {
            try
            {
                var transit = await _transitService.GetTransitByDepartureDate(departure, TerminalID);
                return Ok(transit);

            }catch (Exception ex)
            {
                var errorResponse = new ErrorResponseDto
                {
                    StatusCode = 500,
                    Message = "Internal Server Error: " + ex.Message
                };

                return StatusCode(500, errorResponse);

            }
        }

        // POST api/<TransitController>/create
        [HttpPost("create")]
        public async Task<IActionResult> CreateTransit([FromBody] TransitDto transit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var data = new Transit
                {
                    DepartureDate = transit.DepartureDate,
                    ArrivalDate = transit.ArrivalDate,
                    TerminalId = transit.TerminalId,
                    VehicleID = transit.VehicleID,
                };
                var res = await _transitService.CreateTransit(data);
                return Ok(res);
            }
            catch (Exception ex)
            {

                var errorResponse = new ErrorResponseDto
                {
                    StatusCode = 500,
                    Message = "Internal Server Error: " + ex.Message
                };

                return StatusCode(500, errorResponse);
            }
        }
        // POST api/<TransitController>/getorcreate
        [HttpPost("getorcreate")]
        public async Task<IActionResult> GetOrCreateTransit([FromBody] TransitwithoutvehicleDto transit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var transits = await _transitService.GetOrCreateTransit(transit);
                return Ok(transits);
            }
            catch (Exception ex)
            {

                var errorResponse = new ErrorResponseDto
                {
                    StatusCode = 500,
                    Message = "Internal Server Error: " + ex.Message
                };

                return StatusCode(500, errorResponse);
            }
        }
        // PUT api/<TransitController>/5
        [HttpPut("update/{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TransitController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
