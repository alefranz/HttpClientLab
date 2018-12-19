using System;
using Microsoft.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace HttpClientLab
{
    public class LabMessageHandlerBuilderFilter : IHttpMessageHandlerBuilderFilter
    {
        private readonly IHttpClientBehaviour _behaviour;
        private readonly ILoggerFactory _loggerFactory;

        public LabMessageHandlerBuilderFilter(IHttpClientBehaviour behaviour, ILoggerFactory loggerFactory)
        {
            _behaviour = behaviour;
            _loggerFactory = loggerFactory;
        }

        public Action<HttpMessageHandlerBuilder> Configure(Action<HttpMessageHandlerBuilder> next)
        {
            if (next == null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            return (builder) =>
            {
                next(builder);

                // We want it to be last to be as close to the wire as possible
                builder.AdditionalHandlers.Add(new LabDelegatingHandler(builder.Name, _behaviour, _loggerFactory.CreateLogger<LabDelegatingHandler>()));
            };
        }
    }
}
