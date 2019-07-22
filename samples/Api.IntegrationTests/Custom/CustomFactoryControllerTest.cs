using HttpClientLab;
using Moq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Api.IntegrationTests.Custom
{
    public class CustomFactoryControllerTest : IClassFixture<TestWebApplicationFactory<Startup>>
    {
        private readonly ITestOutputHelper _output;
        private readonly TestWebApplicationFactory<Startup> _factory;

        public CustomFactoryControllerTest(ITestOutputHelper output, TestWebApplicationFactory<Startup> factory)
        {
            _output = output;
            _factory = factory;
        }

        [Fact]
        public async Task Setup_UsingDirectlyAnyMockingFramework()
        {
            // Arrange
            var client = _factory.CreateClient();

            var mock = _factory.MockedHttpClientBehaviour;
            mock.Setup(x => x.Handle(It.IsAny<HttpRequestMessage>(), It.IsAny<string>()))
                .Returns(new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent("Hello world!")
                });

            // Act
            var response = await client.GetAsync("/sample");

            // Print all http requests to get helpful info on failure
            //mock.WriteHttpRequests(_output);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var content = await response.Content.ReadAsStringAsync();
            Assert.Equal("Hello world!", content);
        }
    }
}
