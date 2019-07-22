using System;
using Xunit.Abstractions;

namespace HttpClientLab
{
    public static class HttpClientBehaviourExtensions
    {
        static readonly bool HasNotWindowsNewLine = Environment.NewLine != "\r\n";

        /// <summary>
        /// Write to the provided output the requests and responses received by the mocked behaviour.
        /// </summary>
        /// <param name="httpClientBehaviour">The behaviour to setup, obtained with WithHttpClientBehaviour on the WebApplicationFactory.</param>
        /// <param name="output">The output helper of the XUnit tests</param>
        public static void WriteHttpRequests(this IHttpClientBehaviour httpClientBehaviour, ITestOutputHelper output)
        {
            foreach (var invocation in httpClientBehaviour.Invocations)
            {
                //foreach (var arg in invocation.Arguments)
                //{
                    // TODO: better output
                    var argOutput = invocation.ToString();
                    // ToString does not use Environment.NewLine
                    if (HasNotWindowsNewLine) argOutput = argOutput.Replace("\r\n", Environment.NewLine);
                    output.WriteLine(argOutput);
                //}
            }
        }
    }
}
