using System;
using System.Linq.Expressions;
using System.Net.Http;

namespace HttpClientLab
{
    public interface ISetupClient
    {
        ISetupRequest ForRequest(Func<HttpRequestMessage, bool> matchRequest);
        ISetupRequest ForAnyRequest();
    }
}
