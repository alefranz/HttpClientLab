using HttpClientLab;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Moq;

namespace Api.IntegrationTests.Custom
{
    public class TestWebApplicationFactory<TStartup> : WebApplicationFactory<Startup>
        where TStartup : Startup
    {
        // create an instance of IHttpClientBehaviour or mock it with your favorite mocking library
        public Mock<IHttpClientBehaviour> MockedHttpClientBehaviour { get; } = new Mock<IHttpClientBehaviour>();

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // pass the instance of IHttpClientBehaviour
                services.AddHttpClientBehaviour(MockedHttpClientBehaviour.Object);
            });
        }
    }
}
