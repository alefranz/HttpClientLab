using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace HttpClientLab
{
    public class HttpClientBehaviour : IHttpClientBehaviour
    {
        public static IHttpClientBehaviourBuilder CreateDefaultBuilder() => new HttpClientBehaviourBuilder();

        public HttpResponseMessage Handle(HttpRequestMessage request, string httpClientName)
        {
            throw new NotImplementedException();
        }

        public List<HttpRequestMessage> Invocations { get; } = new List<HttpRequestMessage>();
    }
}
