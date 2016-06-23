using System.Linq;

namespace Faker.Library.Replacers
{
    internal class RegexReplacer : IReplacer
    {
        public EndpointResponse Replace(EndpointMatch match)
        {

            return new EndpointResponse
            {
                Url = match.RequestedUrl,
                Content = Replace(match, match.Endpoint.Content),
                ContentType = Replace(match, match.Endpoint.ContentType),
                StatusCode = match.Endpoint.StatusCode
            };
        }

        private static string Replace(EndpointMatch match, string stringToReplace)
        {
            var replacedContent =
                match.Replacements
                .Aggregate(
                    stringToReplace,
                    (content, subst) => content.Replace($"{{{subst.Key}}}", subst.Value));
            return replacedContent;
        }

    }
}