using Microsoft.AspNetCore.Mvc.Testing;

namespace HttpClientLab
{
    public static class WebApplicationFactoryExtensions
    {
        public static WebApplicationFactory<TEntryPoint> WithHttpClientBehaviour<TEntryPoint>(
            this WebApplicationFactory<TEntryPoint> factory,
            IHttpClientBehaviour httpClientBehaviour)
             where TEntryPoint : class =>
                factory.WithWebHostBuilder(builder =>
                    builder.ConfigureServices(services =>
                        services.AddHttpClientBehaviour(httpClientBehaviour)));
    }
}
