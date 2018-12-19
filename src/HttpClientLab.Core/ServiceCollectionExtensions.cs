using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Http;

namespace HttpClientLab
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddHttpClientBehaviour(this IServiceCollection services, IHttpClientBehaviour behaviour)
        {
            services.AddSingleton(behaviour);
            services.TryAddEnumerable(ServiceDescriptor.Singleton<IHttpMessageHandlerBuilderFilter, LabMessageHandlerBuilderFilter>());
            return services;
        }
    }
}
