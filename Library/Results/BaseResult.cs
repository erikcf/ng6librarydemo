using System.Collections.Generic;
using System.Linq;

namespace Library.Results
{
    public class BaseResult
    {
        public IEnumerable<string> ValidationErrors { get; set; }
        public bool HasErrors() => ValidationErrors?.Any() ?? false;
    }
}
