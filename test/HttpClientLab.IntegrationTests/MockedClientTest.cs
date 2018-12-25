using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using System.Text;

namespace HttpClientLab.IntegrationTests
{
    public class MockedClientTest
    {
        [Fact]
        public async Task ShouldUseMockServer()
        {
            // Arrange
            (var services, var httpServer) = BuildServicesWithClientNaivelyMocked();
            var client = services.GetRequiredService<TestClient>();

            // Act
            var result = await client.DoAsync();

            // Assert
            Assert.True(result);
            httpServer.Verify(server => server.Handle(
                It.Is<HttpRequestMessage>(
                    req => req.RequestUri.AbsoluteUri == "https://example.com/test" && req.Method == HttpMethod.Get
                ), "TestClient"),
                Times.Once);
        }

        [Fact]
        public async Task WriteHttpRequests_ShouldWriteRequest()
        {
            // Arrange
            (var services, var httpServer) = BuildServicesWithClientNaivelyMocked();
            var client = services.GetRequiredService<TestClient>();
            var output = new StringOutput();

            // Act
            await client.DoAsync();
            httpServer.WriteHttpRequests(output);

            // Assert
            Assert.Equal(
                @"Method: GET, RequestUri: 'https://example.com/test', Version: 2.0, Content: <null>, Headers:
{
}
TestClient
",
                output.Content);
        }

        [Fact]
        public async Task WriteHttpRequests_ShouldWriteAllRequests()
        {
            // Arrange
            (var services, var httpServer) = BuildServicesWithClientNaivelyMocked();
            var client = services.GetRequiredService<TestClient>();
            var output = new StringOutput();

            // Act
            await client.DoAsync();
            await client.DoAsync();
            httpServer.WriteHttpRequests(output);

            // Assert
            Assert.Equal(
                @"Method: GET, RequestUri: 'https://example.com/test', Version: 2.0, Content: <null>, Headers:
{
}
TestClient
Method: GET, RequestUri: 'https://example.com/test', Version: 2.0, Content: <null>, Headers:
{
}
TestClient
",
                output.Content);
        }

        [Fact]
        public async Task WriteHttpRequests_ShouldWriteRequestsFromAllClients()
        {
            // Arrange
            (var services, var httpServer) = BuildServicesWithClientNaivelyMocked();
            var client = services.GetRequiredService<TestClient>();
            var client2 = services.GetRequiredService<TestClient>();
            var output = new StringOutput();

            // Act
            await client.DoAsync();
            await client2.DoAsync();
            httpServer.WriteHttpRequests(output);

            // Assert
            Assert.Equal(
                @"Method: GET, RequestUri: 'https://example.com/test', Version: 2.0, Content: <null>, Headers:
{
}
TestClient
Method: GET, RequestUri: 'https://example.com/test', Version: 2.0, Content: <null>, Headers:
{
}
TestClient
",
                output.Content);
        }

        private (ServiceProvider, Mock<IHttpClientBehaviour>) BuildServicesWithClientNaivelyMocked()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddHttpClient<TestClient>(c =>
            {
                c.BaseAddress = new Uri("https://example.com/");
            });
            var httpServer = new Mock<IHttpClientBehaviour>();
            httpServer.Setup(server => server.Handle(It.IsAny<HttpRequestMessage>(), It.IsAny<string>())).Returns(new HttpResponseMessage());
            serviceCollection.AddHttpClientBehaviour(httpServer.Object);
            var services = serviceCollection.BuildServiceProvider();
            return (services, httpServer);
        }

        private class TestClient
        {
            public TestClient(HttpClient httpClient)
            {
                _httpClient = httpClient;
            }

            private HttpClient _httpClient;

            public async Task<bool> DoAsync()
            {
                var response = await _httpClient.GetAsync("/test");
                return response.IsSuccessStatusCode;
            }
        }

        private class StringOutput : ITestOutputHelper
        {
            StringBuilder _stringBuilder = new StringBuilder();

            public string Content => _stringBuilder.ToString();

            public void WriteLine(string message)
            {
                _stringBuilder.Append(message);
                _stringBuilder.Append(Environment.NewLine);
            }

            public void WriteLine(string format, params object[] args)
            {
                throw new NotImplementedException();
            }
        }
    }
}
