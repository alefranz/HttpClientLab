using System.Net.Http;

namespace HttpClientLab
{
    public interface ISetupRequest
    {
        IHttpClientBehaviourBuilder Returns(HttpResponseMessage response);
    }
}
