using Microsoft.AspNetCore.Mvc.Testing;
using Moq;

namespace HttpClientLab
{
    public static class WebApplicationFactoryExtensions
    {
        public static WebApplicationFactory<TEntryPoint> WithHttpClientBehaviour<TEntryPoint>(
            this WebApplicationFactory<TEntryPoint> factory,
            out Mock<IHttpClientBehaviour> httpClientBehaviour)
             where TEntryPoint : class
        {
            httpClientBehaviour = new Mock<IHttpClientBehaviour>(MockBehavior.Strict);
            return factory.WithHttpClientBehaviour(httpClientBehaviour.Object);
        }
    }
}
