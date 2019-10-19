using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Library.Dtos
{
    public class BaseDto
    {
        [JsonIgnore]
        public IEnumerable<string> ValidationErrors { get; set; } = new List<string>();
    }
}
