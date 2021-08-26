using ClipsHttpRestSharp.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ClipsHttpRestSharp.HttpMethod
{
    public sealed class HttpGetMethods
    {
        private static readonly Lazy<HttpGetMethods> httpMethodsInstance = new Lazy<HttpGetMethods>(() => new HttpGetMethods());


        public static HttpGetMethods GetHttpMethods
        {
            get
            {
                return httpMethodsInstance.Value;
            }
        }

        public HttpGetMethods()
        {

        }


        /// <summary>
        /// this is an async method that is use to call http endpoint get method 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="Url"></param>
        /// <returns>TEntity</returns>
        public async Task<TEntity> HttpGetMethodWithNoAuth<TEntity>(string Url)
        {
            var restManager = RestClientManager.GetRestClientManager;

            var restUrl = restManager.setUrl(Url);
            var restRequest = restManager.GetRequestWithNoAuth();
            var restResponse = await restManager.ExcuteRestClient(restUrl, restRequest);
            var response = restManager.GetRestClientContent<TEntity>(restResponse);
            return response;
        }

        /// <summary>
        /// this is an async method that is use to call http endpoint get method with authorization key
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="Url"></param>
        /// <param name="authKey"></param>
        /// <returns></returns>
        public async Task<TEntity> HttpGetMethodWithAuth<TEntity>(string Url, string authKey)
        {
            var restManager = RestClientManager.GetRestClientManager;

            var restUrl = restManager.setUrl(Url);
            var restRequest = restManager.GetRequestWithAuth(authKey);
            var restResponse = await restManager.ExcuteRestClient(restUrl, restRequest);
            var response = restManager.GetRestClientContent<TEntity>(restResponse);
            return response;
        }
    }
}
