using System.Net.Http;

namespace HttpClientLab
{
    public interface IHttpClientBehaviour
    {
        HttpResponseMessage Handle(HttpRequestMessage request, string httpClientName);
    }
}