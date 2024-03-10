using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using vyroute.Dto;
using vyroute.Services;

namespace vyroute.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagingController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public MessagingController(IEmailService emailService)
        {
            _emailService = emailService;
        }
        [HttpPost]
        public async Task<IActionResult> SendEmail([FromBody] EmailSend model)
        {
            var result = await _emailService.SendEmailAsync(model.Email, model.Subject);
            if (result)
            {
                return Ok("Email sent successfully");
            }
            return BadRequest("Error sending mail");
        }
    }
}
