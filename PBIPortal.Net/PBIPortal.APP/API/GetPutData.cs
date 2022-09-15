using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net.Http.Headers;
using System.Text;
 
namespace PBI.APP.API

{
   public class GetPutData
    {

        public static IServiceProvider Container { get; private set; }   



        private static string ClientAPIHost = "";  
        private static WebApiClient ClientToLocal;

        public static string username = "";
        public static string password = "";

        public void SetUserInfo(string usr ,string pwd)
        {
            username = usr;
            password = pwd;
        }

        /// <summary>
        /// 初始化获取配置数据
        /// </summary>
  
        public void Init(string apiPath)
        {
        
            Container = RegisterServices();

            var builder = new ConfigurationBuilder()
                 .AddJsonFile("appsettings.json")
                 .AddEnvironmentVariables()
                 .Build();
            // var configuration = builder.Build();
            //  string sPath = Environment.GetEnvironmentVariable("databaseHost");
            ClientAPIHost = builder["PBIAPIUrl"];
            ClientToLocal = new WebApiClient();

            ClientAPIHost += apiPath;

         

        }

        /// <summary>
        /// httpclient 调用
        /// </summary>
        /// <returns></returns>
        #region  

        private static IServiceProvider RegisterServices()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureHttpClients(serviceCollection);
            return serviceCollection.BuildServiceProvider();
        }
        

        private static void ConfigureHttpClients(IServiceCollection services)
        {

            services.AddHttpClient("GetUsePWD", client =>
            {
                string tk = username+":"+password;

                string ticket = Convert.ToBase64String(Encoding.UTF8.GetBytes(tk));

                client.BaseAddress = new Uri(ClientAPIHost);
                client.DefaultRequestHeaders.Add("Accept", "text/html");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", ticket);

            });

            services.AddHttpClient("PutJsonUsePWD", client =>
            {
                string tk = username + ":" + password;

                string ticket = Convert.ToBase64String(Encoding.UTF8.GetBytes(tk));

                client.BaseAddress = new Uri(ClientAPIHost);
                client.DefaultRequestHeaders.Add("Accept", "text/html");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", ticket);

            });
        }

        #endregion


        #region

        

        public string  GetResult(string apiName)
        {
            string c1 = "";
            try
            {
                //  List<Object> m = listobject.ConvertAll(s => (object)s);
               
                  c1 = ClientToLocal.GetUsePWD(apiName).GetAwaiter().GetResult();
                 

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.ToString());
            }

            return c1;

        }



        public string PutJson(string apiName,string jObject)
        {
            string c1 = "";
            try
            {
                //  List<Object> m = listobject.ConvertAll(s => (object)s);

                c1 = ClientToLocal.PutJsonUsePWD(apiName, jObject).GetAwaiter().GetResult();


            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.ToString());
            }

            return c1;

        }



        #endregion

    }


}
