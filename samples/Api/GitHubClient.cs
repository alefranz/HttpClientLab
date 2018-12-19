using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Api
{
    public class GitHubClient
    {
        public GitHubClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        private HttpClient _httpClient;

        public async Task<string> GetSomething()
        {
            var response = await _httpClient.GetAsync("/");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<HttpStatusCode> PostSomething()
        {
            var response = await _httpClient.PostAsync("/", null);
            return response.StatusCode;
        }
    }
}
