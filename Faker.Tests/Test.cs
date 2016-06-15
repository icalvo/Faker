using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Faker.UI;
using FluentAssertions;
using Microsoft.Owin.Testing;
using Xunit;

namespace Faker.Tests
{
    public class ControllerTest : IDisposable
    {
        private readonly TestServer _server;

        public ControllerTest()
        {
            _server = TestServer.Create<Startup>();
        }


        [Fact]
        public void Defaults_WhenGet_ReturnsOkAndSpecifiedResult()
        {
            var response = _server.HttpClient.GetAsync("api/defaults").Result;
            var result = response.Content.ReadAsStringAsync().Result;
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Content.Headers.ContentType.MediaType = "application/json";
            result.Should().Be("91823746");
        }

        [Fact]
        public void Defaults_WhenPost_ReturnsNotFound()
        {
            var response = _server.HttpClient.PostAsync("api/defaults", new StringContent("")).Result;
            var result = response.Content.ReadAsStringAsync().Result;
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            result.Should().Be("FAKER did not find any valid response.");
        }

        [Fact]
        public void SpecifiedStatusCode_IsReturned()
        {
            var response = _server.HttpClient.GetAsync("api/statuscode").Result;
            var result = response.Content.ReadAsStringAsync().Result;
            response.StatusCode.Should().Be(HttpStatusCode.BadGateway);
            result.Should().NotBeEmpty();
        }
        
        public void Dispose()
        {
            _server.Dispose();
        }
    }
}
