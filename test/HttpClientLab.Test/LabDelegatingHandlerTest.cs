using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace HttpClientLab.Test
{
    public class LabDelegatingHandlerTest
    {
        [Fact]
        public async Task SendAsync_ShouldCallProcess()
        {
            var name = "TestClient";
            var serverMock = new Mock<IHttpClientBehaviour>();
            var request = new HttpRequestMessage();
            var response = new HttpResponseMessage();
            serverMock.Setup(server => server.Handle(request, name)).Returns(response);
            var fixture = new TestableDelegatingHandler(name, serverMock.Object);

            var actualResponse = await fixture.InvokeSendAsync(request);

            Assert.Same(response, actualResponse);
        }

        private class TestableDelegatingHandler : LabDelegatingHandler
        {
            public TestableDelegatingHandler(string name, IHttpClientBehaviour behaviour) : base(name, behaviour, new Logger<LabDelegatingHandler>(new NullLoggerFactory()))
            {

            }

            public Task<HttpResponseMessage> InvokeSendAsync(HttpRequestMessage request)
            {
                return SendAsync(request, CancellationToken.None);
            }
        }
    }
}
