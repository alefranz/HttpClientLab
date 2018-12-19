using Moq;
using System.Net.Http;
using Xunit;

namespace HttpClientLab.IntegrationTests
{
    public class SetupClientTest
    {
        [Fact]
        public void ShouldSetupForAnyClientName()
        {
            var mock = new Mock<IHttpClientBehaviour>();
            var expected = new HttpResponseMessage();
            mock.SetupForAnyClient().ForAnyRequest().Returns(expected);
            var request = new HttpRequestMessage();

            var response = mock.Object.Handle(request, "Marvin");

            Assert.Same(expected, response);
        }

        [Fact]
        public void ShouldSetupForSpecificClientName()
        {
            var mock = new Mock<IHttpClientBehaviour>();
            var expected = new HttpResponseMessage();
            var clientName = "Marvin";
            mock.SetupForClient(clientName).ForAnyRequest().Returns(expected);
            var request = new HttpRequestMessage();

            var response = mock.Object.Handle(request, clientName);
            var other = mock.Object.Handle(request, "Arthur");

            Assert.Same(expected, response);
            Assert.Null(other);
        }

        [Fact]
        public void ShouldSetupForSpecificRequestAndAnyClientName()
        {
            var mock = new Mock<IHttpClientBehaviour>();
            var expected = new HttpResponseMessage();
            var request = new HttpRequestMessage();
            mock.SetupForAnyClient().ForRequest(x => x == request).Returns(expected);
            
            var response = mock.Object.Handle(request, "Marvin");
            var otherClientResponse = mock.Object.Handle(request, "Arthur");
            var otherRequestResponse = mock.Object.Handle(null, "Marvin");

            Assert.Same(expected, response);
            Assert.Same(otherClientResponse, response);
            Assert.Null(otherRequestResponse);
        }

        [Fact]
        public void ShouldSetupForSpecificRequestAndSpecificClientName()
        {
            var mock = new Mock<IHttpClientBehaviour>();
            var expected = new HttpResponseMessage();
            var clientName = "Marvin";
            var request = new HttpRequestMessage();
            mock.SetupForClient(clientName).ForRequest(x => x == request).Returns(expected);
            
            var response = mock.Object.Handle(request, "Marvin");
            var otherClientResponse = mock.Object.Handle(request, "Arthur");
            var otherRequestResponse = mock.Object.Handle(null, "Marvin");

            Assert.Same(expected, response);
            Assert.Null(otherClientResponse);
            Assert.Null(otherRequestResponse);
        }
    }
}
