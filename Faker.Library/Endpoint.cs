using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Owin;

namespace Faker.Library
{
    internal class Endpoint
    {
        public string Url { get; set; }
        public string Method { get; set; } = "GET";
        public string[] Methods { get; set; } = { };
        public string ContentType { get; set; } = "application/json";
        public string Content { get; set; }

        public string ReplacedContent(IOwinRequest request)
        {
            var regex = new Regex(Url);
            var toMatch = request.Path + request.QueryString;
            var match = regex.Match(toMatch);

            Dictionary<string, string> arguments = new Dictionary<string, string>();

            return arguments.Aggregate(
                Content, 
                (current, argument) => current.Replace("{" + argument.Key + "}", argument.Value));
        }
        
        public int StatusCode { get; set; } = 200;

        public string Matcher { get; set; }

        public string Replacer { get; set; }
    }
}