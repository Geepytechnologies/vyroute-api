using Microsoft.AspNetCore.Mvc;
using vyroute.Dto;
using vyroute.Models;
using vyroute.Services;


namespace vyroute.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpGet("find")]
        public async Task<IActionResult> GetAllBookings()
        {
            var result = await _bookingService.GetAllBookings();
            return Ok(result);
        }

        [HttpGet("find/{id}")]
        public async Task<IActionResult> GetBooking(Guid id)
        {
            try
            {
                var booking = await _bookingService.GetBookingByIdAsync(id);
                return Ok(booking);
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
        public async Task<IActionResult> CreateABooking([FromBody] BookingDto booking)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if(booking.UserType == UserType.guestUser)
                {
                    booking.UserID = null;
                }
                var bookingdata = new Booking
                {
                    UserID = booking.UserID,
                    UserType = booking.UserType,
                    BookingType = booking.BookingType,
                    Trip = booking.Trip,
                    Amount = booking.Amount,
                    DepartureDate = booking.DepartureDate,
                    ArrivalDate = booking.ArrivalDate,
                    Passengers = booking.Passengers,
                    Seats = booking.Seats,
                    NextOfKinName = booking.NextOfKinName,
                    NextOfKinPhone = booking.NextOfKinPhone,
                    TerminalId = booking.TerminalId,
                    TransitId = booking.TransitId,
                };
            
                var bookingresult = await _bookingService.CreateBooking(bookingdata);

                return Ok(bookingresult);
                    
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
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
