using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Faker.Library
{
    internal static class CompiledRegexStore
    {
        private static readonly Dictionary<string, Regex> Store
            = new Dictionary<string, Regex>();


        public static Regex Regex(this string expression)
        {
            Regex regex;
            if (Store.TryGetValue(expression, out regex))
            {
                return regex;
            }

            regex = new Regex(expression, RegexOptions.Compiled);
            Store[expression] = regex;
            return regex;
        }
    }
}