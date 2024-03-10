using Microsoft.AspNetCore.Mvc;
using vyroute.Dto;
using vyroute.Models;
using vyroute.Services;


namespace vyroute.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TerminalController : ControllerBase
    {
        private readonly ITerminalService _terminalService;

        public TerminalController(ITerminalService terminalService)
        {
            _terminalService = terminalService; 
        }
        [HttpGet("find")]
        public async Task<IActionResult> GetAllTerminals()
        {
            var result = await _terminalService.GetAllTerminals();
            return Ok(result);
        }

        [HttpGet("find/{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                var terminal = await _terminalService.GetTerminalById(id);
                return Ok(terminal);
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

        [HttpGet("find/departure")]
        public async Task<IActionResult> Get([FromQuery] string departure)
        {
            if (string.IsNullOrEmpty(departure))
            {
                return BadRequest("The 'departure' query parameter is required.");
            }
            try
            {
                var terminals = await _terminalService.GetTerminalByDeparture(departure);
                return Ok(terminals);
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

        [HttpPost("create")]
        public async Task<IActionResult> CreateTerminal([FromBody] TerminalDto terminal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var data = new Terminal
                {
                    Departure = terminal.Departure,
                    Arrival = terminal.Arrival,
                    Price = terminal.Price,
                    DepartureState = terminal.DepartureState,
                    ArrivalState = terminal.ArrivalState
                };
                var res = await _terminalService.CreateTerminal(data);
                return Ok(terminal);
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

        [HttpPut("update/{id}")]
        public IActionResult UpdateTerminal(Guid terminalId, [FromBody] Terminal updatedTerminal)
        {
            var existingTerminal = _terminalService.GetTerminalById(terminalId).Result;
            if (existingTerminal == null)
            {
                return NotFound();
            }


            _terminalService.UpdateTerminal(updatedTerminal);
            // Return appropriate response, e.g., Ok
            return Ok();
        }

        // DELETE api/<TerminalController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
