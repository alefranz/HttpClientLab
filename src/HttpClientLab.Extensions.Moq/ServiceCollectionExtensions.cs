using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace HttpClientLab
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddHttpClientBehaviour(
            this IServiceCollection services,
            out Mock<IHttpClientBehaviour> behaviour)
        {
            behaviour = new Mock<IHttpClientBehaviour>(MockBehavior.Strict);
            return services.AddHttpClientBehaviour(behaviour.Object);
        }
    }
}
