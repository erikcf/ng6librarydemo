using System.Linq;
using System.Threading.Tasks;
using Library.Commands;
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
        private readonly CommandRunner _commandRunner;

        public UserController(IUserService userService, CommandRunner commandRunner)
        {
            _userService = userService;
            _commandRunner = commandRunner;
        }

        [HttpGet("[action]")]
        [ProducesResponseType(typeof(UserDto), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> LogInUser([FromQuery] string email, [FromQuery] string password)
        {
            var user = await _userService.GetUserAsync(email, password);
            if (user is null) { return NotFound(); }
            return Ok(UserDto.FromUser(user));
        }


        [HttpPost("[action]")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateUser([FromBody] UserRequestModel userRequestModel)
        {
            if (!ModelState.IsValid) { return BadRequest(); }

            var command = userRequestModel.ToCommand();
            var validationErrors = _commandRunner.Validate(command, null);
            if (validationErrors.Any())
            {
                return BadRequest(validationErrors);
            }

            var id = await _commandRunner.Execute(command, null);
            return Created("GetUserById", id);
        }
    }
}
