using Microsoft.Rest;
using Plexure.Service.Security.Sdk;
using System;

namespace Plexure.Service.Security.IntegrationTest
{
    internal class SdkClientHelper
    {
        /// <summary>
        /// Gets a new client that will use the supplied token. outputs a LastRequestHelper that gives access to the last request that the client has made
        /// </summary>
        /// <param name="token">the brearer token to use for Auth</param>
        /// <param name="responseHandler">a out paramerter that emits the Last response helper for the newly created cilient</param>
        /// <returns>The SDK client instance</returns>
        internal static Sdk.SdkClient GetAuthenticatedClient(string token)
        {
            Uri uri = new Uri(CommonConfig.url);
            TokenCredentials credentials = new TokenCredentials(token, "Bearer");

            var client = new SdkClient(uri, credentials);
            return client;
        }

        /// <summary>
        /// Gets a new workflow sdk client. outputs a LastRequestHelper that gives access to the last request that the client has made
        /// </summary>
        /// <param name="responseHandler">an out paramerter that emits the Last response helper for the newly created cilient</param>
        /// <returns>The SDK client instance</returns>
        internal static Sdk.SdkClient GetAnonymousClient()
        {
            Uri uri = new Uri(CommonConfig.url);
            var client = new SdkClient(uri);

            return client;
        }
    }
}