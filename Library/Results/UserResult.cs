using System.Collections.Generic;
using System.Linq;
using Library.Dtos;

namespace Library.Results
{
    public class UserResult : BaseResult
    {
        public UserDto UserDto { get; set; }
    }
}
