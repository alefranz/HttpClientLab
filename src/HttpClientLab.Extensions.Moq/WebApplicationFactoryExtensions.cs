using Microsoft.AspNetCore.Mvc.Testing;
using Moq;

namespace HttpClientLab
{
    public static class WebApplicationFactoryExtensions
    {
        /// <summary>
        /// Configure the HttpClientFactory of the in-memory testing environemnt to use the mocked behaviour returned as out parameter.
        /// </summary>
        /// <typeparam name="TEntryPoint">The type of the application entry point, usually Startup.</typeparam>
        /// <param name="factory">The WebApplicationFactory instance.</param>
        /// <param name="httpClientBehaviour">The behaviour of the HttpClients to be configured.</param>
        /// <returns></returns>
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
