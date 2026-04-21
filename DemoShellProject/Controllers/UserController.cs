using DemoShellProject.Models;
using DemoShellProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace DemoShellProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _service;
        private readonly ILogger<UserController> _logger;

        public UserController(UserService service, ILogger<UserController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] User user)
        {
            if (_service.ValidateUser(user))
            {
                _logger.LogInformation("User {UserId} logged in successfully", user.Id);
                return Ok("Login success");
            }
            else
            {
                _logger.LogError("Login failed for {UserId}", user.Id);
                return Unauthorized("Login failed");
            }
        }

        [HttpPost("update")]
        public IActionResult Update([FromBody] User user)
        {
            try
            {
                _service.UpdateUser(user);
                _logger.LogInformation("User {UserId} profile updated", user.Id);
                return Ok("Profile updated");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Profile update failed for {UserId}", user.Id);
                return BadRequest("Update failed");
            }
        }
    }
}
