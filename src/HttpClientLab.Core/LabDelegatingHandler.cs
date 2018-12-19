using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HttpClientLab
{
    public class LabDelegatingHandler : DelegatingHandler
    {
        private readonly string _name;
        private readonly IHttpClientBehaviour _behaviour;
        private readonly ILogger<LabDelegatingHandler> _logger;

        public LabDelegatingHandler(string name, IHttpClientBehaviour behaviour,
            ILogger<LabDelegatingHandler> logger)
        {
            _name = name;
            _behaviour = behaviour;
            _logger = logger;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpResponseMessage response;
            try
            {
                response = _behaviour.Handle(request, _name);
            } catch(Exception exception)
            {
                _logger.LogError("Unable to handle {request}\n{error}", request, exception.Message);
                throw;
            }
            return Task.FromResult(response);
        }
    }
}
