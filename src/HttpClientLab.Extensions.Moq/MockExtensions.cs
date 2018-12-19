using Moq;
using Xunit.Abstractions;

namespace HttpClientLab
{
    public static class MockExtensions
    {
        public static ISetupClient SetupForAnyClient(this Mock<IHttpClientBehaviour> httpServer) =>
            new SetupClient(httpServer);

        public static ISetupClient SetupForClient(this Mock<IHttpClientBehaviour> httpServer, string httpClientName) =>
            new SetupClient(httpServer, httpClientName);

        public static ISetupClient SetupForClient<T>(this Mock<IHttpClientBehaviour> httpServer) =>
            SetupForClient(httpServer, typeof(T).Name);

        public static void WriteHttpRequests(this Mock<IHttpClientBehaviour> mock, ITestOutputHelper output)
        {
            foreach (var invocation in mock.Invocations)
            {
                foreach (var arg in invocation.Arguments)
                {
                    output.WriteLine(arg.ToString());
                }
            }
        }
    }
}
