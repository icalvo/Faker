using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Faker.UI
{
    public class YamlEndpointRepository : IEndpointRepository
    {
        private static List<Endpoint> _endpoints;
        private static DateTime? _dateModified;

        public IEnumerable<Endpoint> All()
        {
            var path = System.Configuration.ConfigurationManager.AppSettings["ConfigurationFile"];
            if (_dateModified.HasValue && File.GetLastWriteTime(path) <= _dateModified.Value)
            {
                return _endpoints;
            }

            using (StreamReader file = File.OpenText(path))
            {
                var serializer = new Deserializer(namingConvention: new HyphenatedNamingConvention());
                _endpoints = serializer.Deserialize<IEnumerable<Endpoint>>(file).ToList();
                _dateModified = File.GetLastWriteTime(path);
            }

            return _endpoints;
        }
    }
}