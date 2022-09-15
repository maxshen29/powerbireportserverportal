using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using PBIPortal.Core.Enums;
using PBIPortal.Core.Services;

namespace PBI.APP.API
{
    class WebApiClient
    {
        private readonly NamedRequestor _requestor;

        public WebApiClient()
        {
            try
            {
                var _httpClientFactory =   GetPutData.Container.GetRequiredService<IHttpClientFactory>();
                _requestor = new NamedRequestor(_httpClientFactory);
            }
            catch (Exception ex)
            {
                Logger.Info(LoggerType.Info,ex.ToString() , ex.Message, ex.Message);

            }

           
        }

    
 

        public async Task<string> GetUsePWD(string ApiName)
        {
            var JsonStr = await _requestor.GetUsePWD(ApiName);
            return JsonStr;
        }

        public async Task<string> PutJsonUsePWD(string apiName, string postData)
        {
            var JsonStr = await _requestor.PutJsonUsePWD(apiName, postData);
            return JsonStr;
        }




    }
}
