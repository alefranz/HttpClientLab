using Moq;
using Xunit.Abstractions;
using System;

namespace HttpClientLab
{
    public static class MockExtensions
    {
        static readonly bool HasNotWindowsNewLine = Environment.NewLine != "\r\n";

        /// <summary>
        /// Create a builder for the behaviour of any client.
        /// </summary>
        /// <param name="httpServer">The behaviour to setup, obtained with WithHttpClientBehaviour on the WebApplicationFactory.</param>
        /// <returns>The builder to setup the behaviour for any client.</returns>
        public static ISetupClient SetupForAnyClient(this Mock<IHttpClientBehaviour> httpServer) =>
            new SetupClient(httpServer);

        /// <summary>
        /// Create a builder for the behaviour of the specified named client.
        /// </summary>
        /// <param name="httpClientBehaviour">The behaviour to setup, obtained with WithHttpClientBehaviour on the WebApplicationFactory.</param>
        /// <param name="httpClientName">The name of the client to setup.</param>
        /// <returns>The builder to setup the behaviour of the named client.</returns>
        public static ISetupClient SetupForClient(this Mock<IHttpClientBehaviour> httpClientBehaviour, string httpClientName) =>
            new SetupClient(httpClientBehaviour, httpClientName);

        /// <summary>
        /// Create a builder for the behaviour of the specified typed client.
        /// </summary>
        /// <typeparam name="TClient">The type of the client to setup.</typeparam>
        /// <param name="httpClientBehaviour">The behaviour to setup, obtained with WithHttpClientBehaviour on the WebApplicationFactory.</param>
        /// <returns>The builder to setup the behaviour of the named client.</returns>
        public static ISetupClient SetupForClient<TClient>(this Mock<IHttpClientBehaviour> httpClientBehaviour) =>
            SetupForClient(httpClientBehaviour, typeof(TClient).Name);

        /// <summary>
        /// Write to the provided output the requests and responses received by the mocked behaviour.
        /// </summary>
        /// <param name="httpClientBehaviour">The behaviour to setup, obtained with WithHttpClientBehaviour on the WebApplicationFactory.</param>
        /// <param name="output">The output helper of the XUnit tests</param>
        public static void WriteHttpRequests(this Mock<IHttpClientBehaviour> httpClientBehaviour, ITestOutputHelper output)
        {
            foreach (var invocation in httpClientBehaviour.Invocations)
            {
                foreach (var arg in invocation.Arguments)
                {
                    // TODO: better output
                    var argOutput = arg.ToString();
                    // ToString does not use Environment.NewLine
                    if (HasNotWindowsNewLine) argOutput = argOutput.Replace("\r\n", Environment.NewLine);
                    output.WriteLine(argOutput);
                }
            }
        }
    }
}
