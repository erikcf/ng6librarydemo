using System;
using System.Collections.Generic;

namespace Library.Helpers
{
    public static class StringExtensions
    {
        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            return source?.IndexOf(toCheck, comp) > 0;
        }

        public static void AddError(this ICollection<string> list, string field)
        {
            if (string.IsNullOrWhiteSpace(field))
            {
                list.Add(nameof(field));
            }
        }
    }
}
