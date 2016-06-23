using System.Linq;
using System.Text.RegularExpressions;

namespace Faker.Library.Replacers
{
    internal class RegexReplacer : IReplacer
    {
        public EndpointResponse Replace(RequestMatch match)
        {

            return new EndpointResponse
            {
                Url = match.Actual.Url,
                Content = Replace(match, match.Endpoint.Response.Content),
                ContentType = Replace(match, match.Endpoint.Response.ContentType),
                StatusCode = match.Endpoint.Response.StatusCode
            };
        }

        private static string Replace(RequestMatch match, string stringToReplace)
        {
            if (stringToReplace == null) return null;

            return Replace(stringToReplace, match.Endpoint.Request.Url, match.Actual.Url);
            //return Replace(stringToReplace, match.Endpoint.Request.Method, match.Actual.Url);
        }

        private static string Replace(string stringToReplace, string regexExpression, string actual)
        {
            var regex = regexExpression.Regex();
            return regex.Replace(regex.Match(actual).Value, stringToReplace);
        }
    }
}