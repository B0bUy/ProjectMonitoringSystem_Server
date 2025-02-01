using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PMS_Serverv1.Data;
using PMS_Serverv1.Entities.UserManagement;
using PMS_Serverv1.Services;
using PMSv1_Shared.Entities.Contracts;

namespace PMS_Serverv1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly ApplicationDbContext _db;
        public AuthController(AuthService authService, ApplicationDbContext db)
        {
            _authService = authService;
            _db = db;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest user)
        {
            try
            {
                var registeredUser = await _authService.Register(user);
                if(registeredUser == null)
                    return BadRequest(new ApiResponse { StatusCode = 501, IsSuccess = false, Message = "Registration failed." });
                return Ok(new ApiResponse { StatusCode = 200, IsSuccess = true, Message = "Registration successful." });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse { StatusCode = 500, IsSuccess = false, Message = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                var user = await _authService.Authenticate(request.Username, request.Password);
                var token = _authService.GenerateJwtToken(user);
                return Ok(new ApiResponse<string> { IsSuccess = true, Message = "You have successfully logged in!", Result = token});
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            try
            {
                await _authService.ForgotPassword(email);
                return Ok("Password reset token sent.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
        {
            try
            {
                await _authService.ResetPassword(request.Email, request.Token, request.NewPassword);
                return Ok("Password reset successful.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("user/{username}")]
        public async Task<IActionResult> GetUserDetails(string username)
        {
            try
            {
                var user = await _db.Users.FirstOrDefaultAsync(u => u.Username == username);
                if (user == null)
                    return NotFound();

                return Ok(new User
                {
                    Username = user.Username,
                    Email = user.Email,
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e);
            }
        }
    }
}
