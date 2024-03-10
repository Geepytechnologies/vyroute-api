using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using vyroute.Dto;
using vyroute.Models;
using vyroute.Services;

namespace vyroute.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterModel user)
        {
            if(await _authService.RegisterUser(user))
            {
                return Ok("Successful");
            }
            return BadRequest("Something went wrong");

        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginModel user)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result =  await _authService.Login(user);
            if(result == true)
            {
                var tokenstring = await _authService.GenerateTokenstringAsync(user);
                var refreshToken =  _authService.GenerateRefreshTokenString();
                var _applicationUser = await _authService.GetUser(user.Username);
                _applicationUser.RefreshToken = refreshToken;
                _applicationUser.RefreshTokenExpiry = DateTime.Now.AddHours(5);

                await _authService.UpdateUser(_applicationUser);
                // Create a cookie
                var cookieOptions = new CookieOptions
                {
                    
                   
                    Expires = DateTimeOffset.Now.AddDays(1) // Set cookie expiration (optional)
                };

                // Add the cookie to the response
                Response.Cookies.Append("refresh", refreshToken, cookieOptions);

                var userDetails = new UserLoginResponseDetails
                {
                    UserId = _applicationUser.Id,
                    Email = _applicationUser.Email,
                    FirstName = _applicationUser.Firstname,
                    LastName = _applicationUser.Lastname,
                    Phone = _applicationUser.PhoneNumber,
                    AccessToken = tokenstring,
                    IsAdmin = _applicationUser.IsAdmin
                    

                };
                var loginResponse = new LoginResponse
                {
                    StatusCode = 200,
                    Message = "Login successful",
                    Result = userDetails
                };
                return Ok(loginResponse);
            }
            return BadRequest();
        }


        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken(RefreshTokenModel model)
        {
            var result = await _authService.RefreshToken(model);
            if (result.Status)
            {
                return Ok(result);
            }
            return Unauthorized();
        }

        [HttpGet]
        public async Task<IActionResult> UserAdmin(string email)
        {
            var result = await _authService.GetUserAdminStatusAsync(email);
            return Ok(result);
        }
        
    }
}
