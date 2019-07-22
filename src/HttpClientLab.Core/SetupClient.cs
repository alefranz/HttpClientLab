using System;
using System.Net.Http;

namespace HttpClientLab
{
    public class SetupClient : SetupMatcher<string, ISetupRequest>, ISetupClient
    {
        public ISetupRequest ForAnyRequest() => Add(new SetupRequest(x => true));

        public ISetupRequest ForRequest(Func<HttpRequestMessage, bool> matchRequest) =>
            Add(new SetupRequest(matchRequest));

        internal SetupClient(Func<string, bool> httpClientNameMatcher) : base(httpClientNameMatcher)
        {
        }
    }
}
