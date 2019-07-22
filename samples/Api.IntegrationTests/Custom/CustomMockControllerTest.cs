using HttpClientLab;
using Microsoft.AspNetCore.Mvc.Testing;
using Moq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Api.IntegrationTests.Custom
{
    public class CustomMockControllerTest : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly ITestOutputHelper _output;
        private readonly WebApplicationFactory<Startup> _factory;

        public CustomMockControllerTest(ITestOutputHelper output, WebApplicationFactory<Startup> factory)
        {
            _output = output;
            _factory = factory;
        }

        [Fact]
        public async Task Setup_UsingDirectlyAnyMockingFramework()
        {
            // create an instance of IHttpClientBehaviour or mock it with your favorite mocking library
            var mockedHttpClientBehaviour = new Mock<IHttpClientBehaviour>();

            // Arrange
            var client = _factory
                .WithHttpClientBehaviour(mockedHttpClientBehaviour.Object)
                .CreateClient();

            mockedHttpClientBehaviour.Setup(x => x.Handle(It.IsAny<HttpRequestMessage>(), It.IsAny<string>()))
                .Returns(new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent("Hello world!")
                });

            // Act
            var response = await client.GetAsync("/sample");

            // Print all http requests to get helpful info on failure
            //mockedHttpClientBehaviour.WriteHttpRequests(_output);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var content = await response.Content.ReadAsStringAsync();
            Assert.Equal("Hello world!", content);
        }
    }
}
