_HttpClientLab turns the HttpClientFactory into a lab, so you can mock the HttpClient to write tests._

## About HttpClientLab

HttpClientLab is a free, open source, testing tool for the .NET Core HttpClientFactory.
It is a solution to mock the behaviour of an external Http services, with an easy to use fluent interface, reducing the amount of boiler plate required, while allowing for full flexibility.

Key benefits:

- reduced boilerplate, from zero to a working test in seconds
- no need to learn a new syntax to define the HttpClient behaviour
  - direct access to `HttpRequestMessage` and `HttpResponseMesssage`
  - for advanced scenarios, can be used with your mocking framework of choice

It is licensed under [Apache License 2.0](https://github.com/alefranz/HttpClientLab/blob/master/LICENSE).

You can find more information in this [blog post](https://alessio.franceschelli.me/post/httpclientlab/).
If you like this project please don't forget to *star* it on [GitHub](https//github.com/alefranz/HttpClientLab) or let me know with a [tweet](https://twitter.com/AleFranz).

## Quick Start

Install the [HttpClientLab Nuget package](https://www.nuget.org/packages/HttpClientLab/).

In your ASP.NET Core integration tests, simply add the HttpClientLab behaviour using `.WithHttpClientBehaviour(out var httpClientBehaviour)` (but don't forget `using HttpClientLab`). If you are not used to this, see the full example in the section below.

```csharp
// This is the usual WebApplicationFactory<Startup> to create the client, injected into your test.
var client = _factory
    .WithHttpClientBehaviour(out var httpClientBehaviour)
    .CreateClient();
```

Then setup the behaviour.
```csharp
var gitHubClientBehaviour = httpClientBehaviour
    .SetupForClient("GitHubClient")  // or .SetupForClient<GitHubClient> or .SetupForAnyClient()
    .ForRequest(req => req.Method == HttpMethod.Get)  // direct access to the HttpRequestMessage
    // or .ForAnyRequest()
    .Returns(new HttpResponseMessage(HttpStatusCode.OK)  // return simply a HttpResponseMessage
    {
        Content = new StringContent("Hello world!")
    });
```

**As you can see, there is not much to learn as you have direct access to the `HttpRequestMessage` and `HttpResponseMessage`.**

You can print the full list of http requests and responses happened during the test printed to the XUnit output using:
```csharp
httpClientBehaviour.WriteHttpRequests(_output);
```

## Examples

Putting all together, to test a controller action with XUnit your test will look like:

```csharp
using HttpClientLab;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Api.IntegrationTests
{
    public class SampleControllerTest : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly ITestOutputHelper _output;
        private readonly WebApplicationFactory<Startup> _factory;

        public SampleControllerTest(ITestOutputHelper output, WebApplicationFactory<Startup> factory)
        {
            _output = output;
            _factory = factory;
        }

        [Fact]
        public async Task SampleTest()
        {
            // Arrange
            var client = _factory
                .WithHttpClientBehaviour(out var httpClientBehaviour)
                .CreateClient();
            var gitHubClientBehaviour = httpClientBehaviour
                .SetupForClient("GitHubClient")  // or .SetupForClient<GitHubClient> or .SetupForAnyClient()
                .ForRequest(req => req.Method == HttpMethod.Get)  // direct access to the HttpRequestMessage
                // or .ForAnyRequest()
                .Returns(new HttpResponseMessage(HttpStatusCode.OK)  // return simply a HttpResponseMessage
                {
                    Content = new StringContent("Hello world!")
                });
            // ...

            // Act
            // ...

            httpClientBehaviour.WriteHttpRequests(_output);

            // Assert
            // ...
        }
    }
}
```

You can find a full example of using it for in-memory integration tests of a ASP.NET Core app [here](samples/Api.IntegrationTests) or how to use it in any .NET Core app [here](samples/ConsoleApp/Program.cs).

### Specify different behaviours for same typed client
```csharp
var gitHubClientBehaviour = httpClientBehaviour.SetupForClient<GitHubClient>();

gitHubClientBehaviour
    .ForRequest(req => req.Method == HttpMethod.Get)
    .Returns(new HttpResponseMessage(HttpStatusCode.OK)
    {
        Content = new StringContent("Hello world!")
    });

gitHubClientBehaviour
    .ForRequest(req => req.Method == HttpMethod.Post)
    .Returns(new HttpResponseMessage(HttpStatusCode.Accepted));

```
