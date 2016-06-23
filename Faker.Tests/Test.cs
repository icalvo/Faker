using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Faker.Library;
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
        public async Task Defaults_WhenGet_ReturnsOkAndSpecifiedResult()
        {
            var actual = new ResponseAndResult(await _server.HttpClient.GetAsync("api/defaults"));
            actual.Response.StatusCode.Should().Be(HttpStatusCode.OK);
            actual.Response.Content.Headers.ContentType.MediaType = "application/json";
            actual.Result.Should().BeEmpty();
        }

        [Fact]
        public async Task Defaults_WhenPost_ReturnsNotFound()
        {
            var actual = new ResponseAndResult(await _server.HttpClient.PostAsync("api/defaults", new StringContent("")));
            actual.ShouldNotMatchAnyRule();
        }

        [Fact]
        public async Task MultipleMethods_WhenGetAndPosts_ReturnsOkAndSpecifiedResult()
        {
            var actual = new ResponseAndResult(await _server.HttpClient.GetAsync("api/multiplemethods"));
            actual.Response.StatusCode.Should().Be(HttpStatusCode.OK);
            actual.Response.Content.Headers.ContentType.MediaType = "application/json";
            actual = new ResponseAndResult(await _server.HttpClient.PostAsync("api/multiplemethods", new StringContent("")));
            actual.Response.StatusCode.Should().Be(HttpStatusCode.OK);
            actual.Response.Content.Headers.ContentType.MediaType = "application/json";
        }

        [Fact]
        public async Task SpecifiedStatusCode_IsReturned()
        {
            var actual = new ResponseAndResult(await _server.HttpClient.GetAsync("api/match1/statuscode"));
            actual.Response.StatusCode.Should().Be(HttpStatusCode.BadGateway);
            actual.Result.Should().NotBeEmpty();
        }

        [Fact]
        public async Task UrlMatching_WhenMatch_Works()
        {
            var actual = new ResponseAndResult(await _server.HttpClient.GetAsync("api/match1/457"));
            actual.Response.StatusCode.Should().Be(HttpStatusCode.OK);
            actual.Result.Should().Be("match1");
        }

        [Fact]
        public async Task UrlMatching_WhenNoMatch_Discards()
        {
            var actual = new ResponseAndResult(await _server.HttpClient.GetAsync("api/match1/invalid"));
            actual.ShouldNotMatchAnyRule();
        }

        public void Dispose()
        {
            _server.Dispose();
        }
    }


    public class ResponseAndResult
    {
        public ResponseAndResult(HttpResponseMessage response)
        {
            Response = response;
            Result = response.Content.ReadAsStringAsync().Result;
        }

        public HttpResponseMessage Response { get; }
        public string Result { get; }
    }

    public static class ObjectAssertionsExtensions
    {
        public static void ShouldNotMatchAnyRule(this ResponseAndResult actual)
        {
            actual.Response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            actual.Result.Should().Be("FAKER did not find any valid response.");
        }
    }
}
