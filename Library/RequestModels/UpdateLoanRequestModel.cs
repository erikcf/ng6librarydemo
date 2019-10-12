using Library.Commands.Loans;

namespace Library.RequestModels
{
    public class UpdateLoanRequestModel
    {
        public bool? Active { get; set; }

        public UpdateLoanCommand ToCommand()
        {
            return new UpdateLoanCommand
            {
                Active = Active
            };
        }
    }
}
