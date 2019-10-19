using System.Linq;
using System.Threading.Tasks;
using Library.Dtos;
using Library.RequestModels;
using Library.Services;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    [Route("api/[controller]")]
    public class BookController : Controller
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpPost("[action]")]
        [ProducesResponseType(typeof(BookDto), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateBook([FromBody] BookRequestModel bookRequestModel)
        {
            if (!ModelState.IsValid) { return BadRequest(); }

            var bookDto = await _bookService.CreateBookAsync(bookRequestModel);

            if (bookDto.ValidationErrors.Any())
            {
                return BadRequest(bookDto.ValidationErrors);
            }
 
            return CreatedAtAction(nameof(GetBookById),new { id = bookDto.BookId },bookDto);
        }

        [HttpGet("[action]")]
        [ProducesResponseType(typeof(BookDto), 200)]
        public async Task<IActionResult> GetAllBooks([FromQuery] string name)
        {
            var books = await _bookService.GetAllBooksAsync(name);
            return Ok(books);
        }

        [HttpGet("[action]/{id}")]
        [ProducesResponseType(typeof(BookDto), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetBookById([FromRoute] int id)
        {
            var bookDto = await _bookService.GetBookByIdAsync(id);
            if (bookDto is null) { return NotFound(); }
            return Ok(bookDto);
        }
    }
}
