using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Library.Dtos;
using Library.RequestModels;
using Library.Services;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("[action]")]
        [ProducesResponseType(typeof(UserDto), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> LogInUser([FromQuery] string email, [FromQuery] string password)
        {
            var user = await _userService.GetUserAsync(email, password);
            if (user is null) { return NotFound(); }
            return Ok(user);
        }

        [HttpPost("[action]")]
        [ProducesResponseType(typeof(UserDto), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateUser([FromBody] UserRequestModel userRequestModel)
        {
            if (!ModelState.IsValid) { return BadRequest(); }

            try
            {
                var result = await _userService.CreateUserAsync(userRequestModel);
                if (result.HasErrors()) { return BadRequest(result.ValidationErrors); }

                return CreatedAtAction(nameof(GetUserById), new { id = result.UserDto.UserId }, result.UserDto);
            }
            catch (ValidationException validationException)
            {
                return StatusCode(500, validationException.Message);
            }
        }

        [HttpGet("[action]/{id}")]
        [ProducesResponseType(typeof(UserDto), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetUserById([FromRoute] int id)
        {
            var userDto = await _userService.GetUserByIdAsync(id);
            if (userDto is null) { return NotFound(); }
            return Ok(userDto);
        }
    }
}
