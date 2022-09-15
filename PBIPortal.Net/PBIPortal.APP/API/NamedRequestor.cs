using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PBI.APP.API
{

    public class NamedRequestor
    {
        private readonly IHttpClientFactory _clientFactory;

        public NamedRequestor(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }








        public async Task<string> PutJsonUsePWD(string apiName, string postData)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, apiName);
            var httpClient = _clientFactory.CreateClient("PutJsonUsePWD");
            request.Content = new StringContent(postData.ToString(),Encoding.UTF8,"application/json");
            var response = await httpClient.SendAsync(request).ConfigureAwait(false);
            //  response.EnsureSuccessStatusCode();
            //if (response.IsSuccessStatusCode)
            //{ return response.StatusCode.ToString()}

            return response.StatusCode.ToString();
        }
        public async Task<string> GetUsePWD(string apiName)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, apiName);
            var httpClient = _clientFactory.CreateClient("GetUsePWD");
            var response = await httpClient.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

 
    }
}
