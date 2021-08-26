using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ClipsHttpRestSharp.Client
{
    public sealed class RestClientManager
    {
        private static readonly Lazy<RestClientManager> restClientMmanagerInstance = new Lazy<RestClientManager>(() => new RestClientManager());
        RestClient _restClient;
        RestRequest _restRequest;

        public static RestClientManager GetRestClientManager
        {
            get
            {
                return restClientMmanagerInstance.Value;
            }
        }

        public RestClientManager()
        {

        }

        /// <summary>
        /// this method is use to set the rest client url and time out
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public RestClient setUrl(string url)
        {
            _restClient = new RestClient(url);
            _restClient.Timeout = -1;
            return _restClient;
        }

        public RestRequest PostRequestWithAuth(dynamic postDetails, string authKey)
        {
            _restRequest = new RestRequest(Method.POST);
             _restRequest.AddHeader("Authorization", authKey);
             _restRequest.AddHeader("Content-Type", "application/json");
              var bodyData = JsonConvert.SerializeObject(postDetails);
              _restRequest.AddParameter("application/json", bodyData, ParameterType.RequestBody);

            return _restRequest;
        }
        
        public RestRequest PostRequestWithNoAuth<T>(T postDetails)
        {
            _restRequest = new RestRequest(Method.POST);
             _restRequest.AddHeader("Content-Type", "application/json");
              var bodyData = JsonConvert.SerializeObject(postDetails);
              _restRequest.AddParameter("application/json", bodyData, ParameterType.RequestBody);

            return _restRequest;
        }

        /// <summary>
        /// this method is use to set the http get request method
        /// </summary>
        /// <returns>RestRequest</returns>
        public RestRequest GetRequestWithNoAuth()
        {
            _restRequest = new RestRequest(Method.GET);
            return _restRequest;
        }

        /// <summary>
        /// this method is use to set the http get request method with authorization header
        /// </summary>
        /// <returns>RestRequest</returns>
        public RestRequest GetRequestWithAuth(string authKey)
        {
            _restRequest = new RestRequest(Method.GET);
            _restRequest.AddHeader("Authorization", authKey);
            return _restRequest;
        }


        /// <summary>
        /// this method is use to execute the rest client in async
        /// </summary>
        /// <param name="restClient"></param>
        /// <param name="restRequest"></param>
        /// <returns>IRestResponse</returns>
        public async Task<IRestResponse> ExcuteRestClient(RestClient restClient, RestRequest restRequest)
        {
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12 | System.Net.SecurityProtocolType.Tls11 | System.Net.SecurityProtocolType.Tls11;
            return await restClient.ExecuteAsync(restRequest);

        }

        /// <summary>
        /// this method is use to deserialize the rest client response to any given class object
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="response"></param>
        /// <returns>TEntity</returns>
        public TEntity GetRestClientContent<TEntity>(IRestResponse response)
        {
            var content = response.Content;
            var result = JsonConvert.DeserializeObject<TEntity>(content);
            return result;
        }

    }
}
