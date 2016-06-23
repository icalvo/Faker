using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Faker.Library
{
    internal static class CompiledRegexStore
    {
        private static readonly Dictionary<string, Regex> Store
            = new Dictionary<string, Regex>();


        public static Regex Regex(this Endpoint endpoint)
        {
            Regex regex;
            if (Store.TryGetValue(endpoint.Url, out regex))
            {
                return regex;
            }

            regex = new Regex(endpoint.Url, RegexOptions.Compiled);
            Store[endpoint.Url] = regex;
            return regex;
        }
    }
}