using System.Collections.Generic;
using System.Net.Http;

namespace HttpClientLab
{
    public interface IHttpClientBehaviour
    {
        HttpResponseMessage Handle(HttpRequestMessage request, string httpClientName);
        List<HttpRequestMessage> Invocations { get; }
    }
}
