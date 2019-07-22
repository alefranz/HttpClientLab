using System;

namespace HttpClientLab
{
    public class HttpClientBehaviourBuilder : Setup<ISetupClient>, IHttpClientBehaviourBuilder
    {
        public IHttpClientBehaviour Build() => new HttpClientBehaviour();

        /// <inheritdoc/>
        public ISetupClient SetupForAnyClient() =>
            Add(new SetupClient(x => true));

        /// <inheritdoc/>
        public ISetupClient SetupForClient(string httpClientName) =>
            Add(new SetupClient(Match(httpClientName)));

        /// <inheritdoc/>
        public ISetupClient SetupForClient<TClient>() =>
            Add(new SetupClient(Match(typeof(TClient).Name)));

        private static Func<string, bool> Match(string httpClientName) =>
            x => string.Equals(x, httpClientName, StringComparison.OrdinalIgnoreCase);
    }
}
