using BaseLibrary.DTOs;
using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Services.Interfaces;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet("getallusers")]
        public async Task<ActionResult<List<User>>> GetAllUser()
        {
            var result = await _userService.GetAllAsync();
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _userService.GetByIdAsync(id);

            if (user != null)
            {
                return Ok(user);
            }
            return NotFound();
        }

        [HttpGet("GetUserByEmail")]
        public async Task<ActionResult<User>> GetUserByEmail([FromQuery] string userEmail)
        {
            var currentUser = await _userService.GetUserByEmail(userEmail);
            if (currentUser != null)
            {
                return Ok(currentUser);
            }
            return NotFound();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceResponse<User>>> UpdateUser([FromBody] UserDTO model, int id)
        {
            var result = await _userService.UpdateUser(model, id);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            var result = await _userService.DeleteAsync(id);
            if (result != null)
            {
                return Ok();
            }
            return NotFound();
        }

        [HttpPut("ChangePassword")]
        public async Task<ActionResult> ChangePassword([FromBody] UpdateDTO editPassword)
        {

            var result = await _userService.ChangePassword(editPassword);
            if (result != null)
            {
                return Ok();
            }
            return NotFound();
        }

        [HttpGet("usersBySearch")]
        public async Task<ActionResult<List<User>>> SearchUsers(string searchText)
        {
            var result = await _userService.SearchUsers(searchText);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();

        }
    }
}
