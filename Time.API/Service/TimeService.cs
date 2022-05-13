using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Time.API.Model;
using Time.API.Service.Exceptions;

namespace Time.API.Service
{
    public class TimeService : ITimeService
    {

       // private IConfigurationRoot ConfigRoot;
        private readonly HttpClient _httpClient;

        public TimeService(HttpClient httpClient)//IConfiguration configRoot, 
        {
            //ConfigRoot = (IConfigurationRoot)configRoot;
            _httpClient = httpClient;
        }

       /* public Uri BaseUri => new Uri(ConfigRoot.GetValue<string>("EndPoint:TimeAPI"));
        private HttpClient GetClient()
        {
            var client = new HttpClient();
            client.BaseAddress = this.BaseUri; // new Uri("https://www.timeapi.io");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }*/

        private static async Task EnsureSuccess(HttpStatusCode statusCode, HttpContent content)
        {
            if (statusCode == HttpStatusCode.OK)
            {
                return;
            }
            var httpContent = await content.ReadAsStringAsync().ConfigureAwait(false);

            switch (statusCode)
            {
                case HttpStatusCode.BadRequest:
                    throw new TimeServiceInvalidZone($"Time service cal result Bad Request: {httpContent}");
                default:
                    throw new Exception($"Time service cal result unexpected: {httpContent}");

            }
        }
        public async Task<TimeRoot> GetLocal()
        {
            //using (var client = this.GetClient())
            //{
                HttpResponseMessage response = await _httpClient.GetAsync($"/api/Time/current/zone?timeZone=Europe/kiev");
                //await EnsureSuccess(response.StatusCode, response.Content);

                var content = await response.Content.ReadAsStringAsync();

                TimeRoot timeContent = JsonConvert.DeserializeObject<TimeRoot>(content);
                return timeContent;
            //}
        }

        public async Task<TimeRoot> GetTime(string city)
        {
            //using (var client = this.GetClient())
            //{
                HttpResponseMessage response = await _httpClient.GetAsync($"/api/Time/current/zone?timeZone=Europe/{city}");
                //await EnsureSuccess(response.StatusCode, response.Content);

                var content = await response.Content.ReadAsStringAsync();

                TimeRoot timeContent = JsonConvert.DeserializeObject<TimeRoot>(content);
                return timeContent;
            //}
        }


    }
}
