using System;
using System.Net.Http;

namespace HttpClientLab
{
    public class SetupRequest : Matcher<HttpRequestMessage>, ISetupRequest
    {
        private HttpResponseMessage _response;

        internal SetupRequest(Func<HttpRequestMessage, bool> matchRequest) : base(matchRequest)
        {
        }

        public IHttpClientBehaviourBuilder Returns(HttpResponseMessage response)
        {
            _response = response;
        }
    }
}
