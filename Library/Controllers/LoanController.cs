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
    public class LoanController : Controller
    {
        private readonly ILoanService _loanService;
        private readonly CommandRunner _commandRunner;

        public LoanController(ILoanService loanService, CommandRunner commandRunner)
        {
            _loanService = loanService;
            _commandRunner = commandRunner;
        }

        [HttpGet("[action]/{id}")]
        [ProducesResponseType(typeof(LoanDto), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetLoanById([FromRoute] int id)
        {
            var loan = await _loanService.GetLoanByIdAsync(id);
            if (loan is null)
            {
                return NotFound();
            }
            return Ok(LoanDto.FromLoanDto(loan));
        }

        [HttpGet("[action]/{id}")]
        [ProducesResponseType(typeof(LoanDto), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetLoansByUserId([FromRoute] int id)
        {
            var loans = await _loanService.GetLoansByUserIdAsync(id);
            if (!loans.Any()) { return NotFound(); }
            return Ok(loans.Select(LoanDto.FromLoanDto));
        }

        [HttpGet("[action]/{id}")]
        [ProducesResponseType(typeof(BookDto), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetLoansForBookById([FromRoute] int id)
        {
            var loans = await _loanService.GetLoansForBookByIdAsync(id);
            if (!loans.Any())
            {
                return NotFound();
            }
            return Ok(loans.Select(LoanDto.FromLoanDto));
        }

        [HttpPost("[action]")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateLoan([FromBody] CreateLoanRequestModel createLoanRequestModel)
        {
            if (!ModelState.IsValid) { return BadRequest(); }

            var command = createLoanRequestModel.ToCommand();
            var validationErrors = _commandRunner.Validate(command, null);
            if (validationErrors.Any())
            {
                return BadRequest(validationErrors);
            }

            var id = await _commandRunner.Execute(command, null);
            return CreatedAtAction(nameof(GetLoanById), new { id }, 
                LoanDto.FromLoanDto(await _loanService.GetLoanByIdAsync(id)));
        }

        [HttpPut("[action]/{id}")]
        [ProducesResponseType(typeof(LoanDto), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateLoan([FromRoute] int id, [FromBody] UpdateLoanRequestModel updateLoanRequestModel)
        {
            if (!ModelState.IsValid) { return BadRequest(); }

            var loan = await _loanService.GetLoanByIdAsync(id);
            if (loan is null) { return NotFound(); }

            var command = updateLoanRequestModel.ToCommand();
            var validationErrors = _commandRunner.Validate(command, loan);
            if (validationErrors.Any())
            {
                return BadRequest(validationErrors);
            }
            await _commandRunner.Execute(command, loan);

            return Ok(LoanDto.FromLoanDto(loan));
        }
    }
}
