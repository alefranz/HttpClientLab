using HttpClientLab;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Api.IntegrationTests
{
    public class ValuesControllerTest : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly ITestOutputHelper _output;
        private readonly WebApplicationFactory<Startup> _factory;

        public ValuesControllerTest(ITestOutputHelper output, WebApplicationFactory<Startup> factory)
        {
            _output = output;
            _factory = factory;
        }

        [Fact]
        public async Task SetupDefaultBehaviourOfSpecificClientForSepcificRequest()
        {
            // Arrange
            var httpClientBehaviour = HttpClientBehaviour.CreateDefaultBuilder()
                .SetupForClient("GitHubClient")  // or .SetupForClient<GitHubClient> or .SetupForAnyClient()
                    .ForRequest(req => req.Method == HttpMethod.Get)  // direct access to the HttpRequestMessage
                                                                      // or .ForAnyRequest()
                    .Returns(new HttpResponseMessage(HttpStatusCode.OK)  // return simply a HttpResponseMessage
                    {
                        Content = new StringContent("Hello world!")
                    })
            .Build();
            var client = _factory
                .WithHttpClientBehaviour(httpClientBehaviour)
                .CreateClient();


            // Act
            var response = await client.GetAsync("/sample");

            // Print all http requests to get helpful info on failure
            httpClientBehaviour.WriteHttpRequests(_output);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var content = await response.Content.ReadAsStringAsync();
            Assert.Equal("Hello world!", content);
        }

        //[Fact]
        //public async Task SetupDefaultBehaviourOfSpecificClientForAnyRequest()
        //{
        //    // Arrange
        //    var client = _factory
        //        .WithHttpClientBehaviour(out var httpClientBehaviour)
        //        .CreateClient();

        //    httpClientBehaviour
        //        .SetupForClient("GitHubClient")  // or .SetupForClient<GitHubClient> or .SetupForAnyClient()
        //        .ForAnyRequest()
        //        .Returns(new HttpResponseMessage(HttpStatusCode.OK)  // return simply a HttpResponseMessage
        //        {
        //            Content = new StringContent("Hello world!")
        //        });

        //    // Act
        //    var response = await client.GetAsync("/sample");

        //    // Print all http requests to get helpful info on failure
        //    httpClientBehaviour.WriteHttpRequests(_output);

        //    // Assert
        //    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        //    var content = await response.Content.ReadAsStringAsync();
        //    Assert.Equal("Hello world!", content);
        //}

        //[Fact]
        //public async Task SetupDifferentBehaviours()
        //{
        //    // Arrange
        //    var client = _factory
        //        .WithHttpClientBehaviour(out var httpClientBehaviour)
        //        .CreateClient();

        //    var gitHubClientBehaviour = httpClientBehaviour.SetupForClient<GitHubClient>();

        //    gitHubClientBehaviour
        //        .ForRequest(req => req.Method == HttpMethod.Get)
        //        .Returns(new HttpResponseMessage(HttpStatusCode.OK)
        //        {
        //            Content = new StringContent("Hello world!")
        //        });
        //    gitHubClientBehaviour
        //        .ForRequest(req => req.Method == HttpMethod.Post)
        //        .Returns(new HttpResponseMessage(HttpStatusCode.Accepted));

        //    // Act
        //    var getResponse = await client.GetAsync("/sample");
        //    var postResponse = await client.PostAsync("/sample", null);

        //    // Print all http requests to get helpful info on failure
        //    httpClientBehaviour.WriteHttpRequests(_output);

        //    // Assert
        //    Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);
        //    Assert.Equal(HttpStatusCode.Accepted, postResponse.StatusCode);
        //}
    }
}
