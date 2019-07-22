using System;
using System.Linq.Expressions;
using System.Net.Http;

namespace HttpClientLab
{
    public interface IHttpClientBehaviourBuilder
    {
        IHttpClientBehaviour Build();
        
        ISetupClient SetupForAnyClient();

        /// <summary>
        /// Create a builder for the behaviour of the specified named client.
        /// </summary>
        /// <param name="httpClientName">The name of the client to setup.</param>
        /// <returns>The builder to setup the behaviour of the named client.</returns>
        ISetupClient SetupForClient(string httpClientName);

        /// <summary>
        /// Create a builder for the behaviour of the specified typed client.
        /// </summary>
        /// <typeparam name="TClient">The type of the client to setup.</typeparam>
        /// <returns>The builder to setup the behaviour of the named client.</returns>
        ISetupClient SetupForClient<TClient>();
    }
}
