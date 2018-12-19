using Moq.Language.Flow;
using System;
using System.Linq.Expressions;
using System.Net.Http;

namespace HttpClientLab
{
    public interface ISetupClient
    {
        ISetup<IHttpClientBehaviour, HttpResponseMessage> ForRequest(Expression<Func<HttpRequestMessage, bool>> matchRequest);
        ISetup<IHttpClientBehaviour, HttpResponseMessage> ForAnyRequest();
    }
}