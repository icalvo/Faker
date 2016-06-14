using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Owin;
using Owin;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

[assembly: OwinStartup(typeof(Faker.UI.Startup))]

namespace Faker.UI
{
    public class Startup
    {
        private static IEnumerable<Response> _responses;
        private static DateTime? _dateModified;

        public void Configuration(IAppBuilder app)
        {
            LoadResponses();
            app.Run(context =>
            {
                LoadResponses();

                var r = _responses
                    .FirstOrDefault(x => x.Matches(context.Request));

                if (r == null)
                {
                    throw new NotImplementedException();
                }

                context.Response.ContentType = r.ContentType;
                context.Response.StatusCode = r.Status(context.Request);
                return context.Response.WriteAsync(r.Content);
            });
        }

        private static void LoadResponses()
        {
            var configPath = System.Configuration.ConfigurationManager.AppSettings["ConfigurationFile"];
            var path = Path.Combine(configPath, "configuration.yaml");
            if (_dateModified.HasValue && File.GetLastWriteTime(path) <= _dateModified.Value)
            {
                return;
            }
            using (StreamReader file = File.OpenText(path))
            {
                var serializer = new Deserializer(namingConvention: new HyphenatedNamingConvention());
                _responses = serializer.Deserialize<IEnumerable<Response>>(file);
                _dateModified = File.GetLastWriteTime(path);
            }
        }
    }

    public class Response
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
        public bool Matches(IOwinRequest request)
        {
            var toMatch = request.Path + request.QueryString;

            return 
                toMatch == Url &&
                (Method == request.Method ||
                 Methods.Contains(request.Method));
        }
    }
}