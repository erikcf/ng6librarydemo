using System.Collections.Generic;

namespace Library.Logic
{
    public static class ErrorHelper
    {
        public static void AddError(ICollection<string> list, string field)
        {
            if (string.IsNullOrWhiteSpace(field))
            {
                list.Add(nameof(field));
            }
        }
    }
}
