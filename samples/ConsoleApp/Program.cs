using HttpClientLab;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Run().GetAwaiter().GetResult();
        }

        public static async Task Run()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging(b => b.AddConsole(c => c.IncludeScopes = true));

            // Setup the HttpClientFactory to mock the behaviour
            serviceCollection.AddHttpClientBehaviour(out var httpClientBehaviour);

            //define the behaviour
            httpClientBehaviour
                .SetupForAnyClient()
                .ForAnyRequest()
                .Returns(new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent("Hello world!")
                });

            serviceCollection.AddHttpClient<GitHubClient>(c =>
            {
                c.BaseAddress = new Uri("https://api.github.com/");
                c.DefaultRequestHeaders.Add("Accept", "application/vnd.github.v3+json");
                c.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (trust me, I'm really Mozilla!)");
            });

            var services = serviceCollection.BuildServiceProvider();

            var github = services.GetRequiredService<GitHubClient>();
            var something = await github.GetSomething();
            Console.WriteLine(something);

            Debug.Assert(something == "Hello world!");

            if (Debugger.IsAttached)
            {
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
        }

        private class GitHubClient
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
        }
    }
}
