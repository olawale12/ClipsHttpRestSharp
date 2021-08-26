using ClipsHttpRestSharp.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ClipsHttpRestSharp.HttpMethod
{
    public sealed class HttpPostMethod
    {
        private static readonly Lazy<HttpPostMethod> httpPostMethodsInstance = new Lazy<HttpPostMethod>(() => new HttpPostMethod());


        public static HttpPostMethod GetHttpPostMethods
        {
            get
            {
                return httpPostMethodsInstance.Value;
            }
        }

        public HttpPostMethod()
        {

        }

        /// <summary>
        /// This is http post method with authorization header
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="Url"></param>
        /// <param name="AuthKey"></param>
        /// <param name="PostData"></param>
        /// <returns></returns>
        public async Task<TEntity> HttpPostMethodWithAuth<TEntity>(string Url, string AuthKey, dynamic PostData)
        {
            var restManager = RestClientManager.GetRestClientManager;

            var restUrl = restManager.setUrl(Url);
            var restRequest = restManager.PostRequestWithAuth(PostData, AuthKey);
            var restResponse = await restManager.ExcuteRestClient(restUrl, restRequest);
            var response = restManager.GetRestClientContent<TEntity>(restResponse);
            return response;
        }
        
        /// <summary>
        /// This is http post method with no authorization header
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="Url"></param>
        /// <param name="AuthKey"></param>
        /// <param name="PostData"></param>
        /// <returns></returns>
        public async Task<TEntity> HttpPostMethodWithNoAuth<TEntity>(string Url, dynamic PostData)
        {
            var restManager = RestClientManager.GetRestClientManager;

            var restUrl = restManager.setUrl(Url);
            var restRequest = restManager.PostRequestWithNoAuth(PostData);
            var restResponse = await restManager.ExcuteRestClient(restUrl, restRequest);
            var response = restManager.GetRestClientContent<TEntity>(restResponse);
            return response;
        }
    }
}
