using System.Linq;
using System.Threading.Tasks;
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

        public LoanController(ILoanService loanService)
        {
            _loanService = loanService;
        }

        [HttpGet("[action]/{id}")]
        [ProducesResponseType(typeof(LoanDto), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetLoanById([FromRoute] int id)
        {
            var loanDto = await _loanService.GetLoanByIdAsync(id);
            if (loanDto is null) { return NotFound(); }
            return Ok(loanDto);
        }

        [HttpGet("[action]/{id}")]
        [ProducesResponseType(typeof(LoanDto), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetLoansByUserId([FromRoute] int id)
        {
            var loanDtos = await _loanService.GetLoansByUserIdAsync(id);
            if (!loanDtos.Any()) { return NotFound(); }
            return Ok(loanDtos);
        }

        [HttpGet("[action]/{id}")]
        [ProducesResponseType(typeof(BookDto), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetLoansForBookById([FromRoute] int id)
        {
            var loanDtos = await _loanService.GetLoansForBookByIdAsync(id);
            if (!loanDtos.Any()) { return NotFound(); }
            return Ok(loanDtos);
        }

        [HttpPost("[action]")]
        [ProducesResponseType(typeof(LoanDto),201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateLoan([FromBody] CreateLoanRequestModel createLoanRequestModel)
        {
            if (!ModelState.IsValid) { return BadRequest(); }

            var loanResult = await _loanService.CreateLoanAsync(createLoanRequestModel);

            if (loanResult.HasErrors())
            {
                return BadRequest(loanResult.ValidationErrors);
            }

            return CreatedAtAction(nameof(GetLoanById), new { id = loanResult.LoanDto.LoanId }, loanResult.LoanDto);
        }

        [HttpPut("[action]/{id}")]
        [ProducesResponseType(typeof(LoanDto), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> UpdateLoan([FromRoute] int id, [FromBody] UpdateLoanRequestModel updateLoanRequestModel)
        {
            if (!ModelState.IsValid) { return BadRequest(); }

            var loanResult = await _loanService.UpdateLoanAsync(id, updateLoanRequestModel);
            if (loanResult is null) { return NotFound(); }
            if (loanResult.HasErrors())
            {
                return BadRequest(loanResult.ValidationErrors);
            }

            return Ok(loanResult.LoanDto);
        }
    }
}
