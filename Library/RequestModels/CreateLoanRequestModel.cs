using Library.Commands.Loans;

namespace Library.RequestModels
{
    public class CreateLoanRequestModel
    {
        public int BookId { get; set; }
        public int UserId { get; set; }

        public CreateLoanCommand ToCommand()
        {
            return new CreateLoanCommand
            {
                BookId = BookId,
                UserId = UserId
            };
        }
    }
}
