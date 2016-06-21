using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Owin;

namespace Faker.Library
{
    public class Endpoint
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

        public int Status(IOwinRequest request)
        {
            if (StatusCodeParameter == null)
            {
                return StatusCode;
            }

            var regex = new Regex(Url);
            var toMatch = request.Path + request.QueryString;
            var match = regex.Match(toMatch);

            return int.Parse(match.Groups[StatusCodeParameter].Value);
        }

        public int StatusCode { get; set; } = 200;
        public string StatusCodeParameter { get; set; }

        public EndpointResponse TryMatch(IOwinRequest request)
        {
            var requestPath = request.Path + request.QueryString;

            if (
                requestPath == "/" + Url.TrimStart('/') &&
                (Method == request.Method ||
                 Methods.Contains(request.Method)))
            {
                return new EndpointResponse
                {
                    Url = requestPath,
                    Content = Content,
                    ContentType = ContentType,
                    Method = request.Method,
                    StatusCode = Status(request)
                };
            }

            return null;
        }
    }
}