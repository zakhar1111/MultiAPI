using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Time.API.Model;

namespace Time.API.Service
{
    public class TimeService : ITimeService
    {

        private IConfigurationRoot ConfigRoot;

        public TimeService(IConfiguration configRoot)
        {
            ConfigRoot = (IConfigurationRoot)configRoot;
        }

        public Uri BaseUri => new Uri(ConfigRoot.GetValue<string>("EndPoint:TimeAPI"));
        private HttpClient GetClient()
        {
            var client = new HttpClient();
            client.BaseAddress = this.BaseUri; // new Uri("https://www.timeapi.io");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }
        public async Task<TimeRoot> GetLocal()
        {
            using (var client = this.GetClient())
            {
                HttpResponseMessage response = await client.GetAsync($"/api/Time/current/zone?timeZone=Europe/kiev");
                var content = await response.Content.ReadAsStringAsync();

                TimeRoot timeContent = JsonConvert.DeserializeObject<TimeRoot>(content);
                return timeContent;
            }
        }

        public async Task<TimeRoot> GetTime(string city)
        {
            using (var client = this.GetClient())
            {
                HttpResponseMessage response = await client.GetAsync($"/api/Time/current/zone?timeZone=Europe/{city}");
                var content = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    TimeRoot timeContent = JsonConvert.DeserializeObject<TimeRoot>(content);
                    return timeContent;
                }
                else
                {
                    throw new Exception("invalid city");
                }
                
            }
        }


    }
}
