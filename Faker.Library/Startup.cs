﻿using System.Diagnostics.CodeAnalysis;
using Faker.Library;
using Faker.Library.Matchers;
using Faker.Library.Replacers;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace Faker.Library
{
    public class Startup
    {
        private readonly FakerMiddleware _fakerMiddleware;

        public Startup()
        {
            _fakerMiddleware = new FakerMiddleware(
                new YamlEndpointRepository(),
                new PluginMatcher(), 
                new PluginReplacer());
        }

        [SuppressMessage("ReSharper", "UnusedMember.Global",
            Justification = "Owin calls this method")]
        public void Configuration(IAppBuilder app)
        {
            app.Run(_fakerMiddleware.Handle);
        }
    }
}