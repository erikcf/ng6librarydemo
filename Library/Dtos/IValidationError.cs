using System.Collections.Generic;

namespace Library.Dtos
{
    public interface IValidationError
    {
        IEnumerable<string> ValidationErrors { get; set; }
    }
}
