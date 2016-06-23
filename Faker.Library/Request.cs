using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Owin;

namespace Faker.Library
{
    internal class Request
    {
        public string Url { get; set; }
        public string Method { get; set; } = "GET";
        public string[] Methods { get; set; } = { };
    }
}