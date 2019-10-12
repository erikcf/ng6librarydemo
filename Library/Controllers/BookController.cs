using System.Linq;
using System.Threading.Tasks;
using Library.Commands;
using Library.Dto;
using Library.RequestModels;
using Library.Services;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    [Route("api/[controller]")]
    public class BookController : Controller
    {
        private readonly IBookService _bookService;
        private readonly CommandRunner _commandRunner;

        public BookController(IBookService bookService, CommandRunner commandRunner)
        {
            _bookService = bookService;
            _commandRunner = commandRunner;
        }

        [HttpPost("[action]")]
        [ProducesResponseType(typeof(BookDto), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateBook([FromBody] BookRequestModel bookRequestModel)
        {
            if(!ModelState.IsValid) { return BadRequest(); }

            var command = bookRequestModel.ToCommand();
            var validationErrors = _commandRunner.Validate(command, null);
            if (validationErrors.Any())
            {
                return BadRequest(validationErrors);
            }

            var id = await _commandRunner.Execute(command, null);
            return CreatedAtAction(nameof(GetBookById),new { id },BookDto.FromBook(await _bookService.GetBookById(id)));
        }

        [HttpGet("[action]")]
        [ProducesResponseType(typeof(BookDto), 200)]
        public async Task<IActionResult> GetAllBooks([FromQuery] string name)
        {
            var books = await _bookService.GetAllBooks(name);
            return Ok(books.Select(BookDto.FromBook));
        }

        [HttpGet("[action]/{id}")]
        [ProducesResponseType(typeof(BookDto), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetBookById([FromRoute] int id)
        {
            var book = await _bookService.GetBookById(id);
            if (book is null) { return NotFound(); }
            return Ok(BookDto.FromBook(book));
        }
    }
}
