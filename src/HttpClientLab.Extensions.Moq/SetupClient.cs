using Moq;
using Moq.Language.Flow;
using System;
using System.Linq.Expressions;
using System.Net.Http;

namespace HttpClientLab
{
    internal class SetupClient : ISetupClient
    {
        private readonly Mock<IHttpClientBehaviour> _httpServer;
        private readonly string _httpClientName;

        internal SetupClient(Mock<IHttpClientBehaviour> httpServer)
        {
            _httpServer = httpServer;
        }

        internal SetupClient(Mock<IHttpClientBehaviour> httpServer, string httpClientName)
        {
            _httpServer = httpServer;
            _httpClientName = httpClientName;
        }

        public ISetup<IHttpClientBehaviour, HttpResponseMessage> ForAnyRequest() => 
            _httpServer.Setup(s => s.Handle(
                It.IsAny<HttpRequestMessage>(),
                It.Is<string>(name => _httpClientName == null || name.Equals(_httpClientName, StringComparison.InvariantCultureIgnoreCase))));

        public ISetup<IHttpClientBehaviour, HttpResponseMessage> ForRequest(Expression<Func<HttpRequestMessage, bool>> matchRequest) =>
            _httpServer.Setup(s => s.Handle(
                It.Is(matchRequest),
                It.Is<string>(name => _httpClientName == null || name.Equals(_httpClientName, StringComparison.InvariantCultureIgnoreCase))));
    }
}
