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

        private readonly HttpClient _httpClient;

        public TimeService(HttpClient httpClient) 
        {
            _httpClient = httpClient;
        }

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
                HttpResponseMessage response = await _httpClient.GetAsync($"/api/Time/current/zone?timeZone=Europe/kiev");
                //await EnsureSuccess(response.StatusCode, response.Content);

                var content = await response.Content.ReadAsStringAsync();

                TimeRoot timeContent = JsonConvert.DeserializeObject<TimeRoot>(content);
                return timeContent;
        }

        public async Task<TimeRoot> GetTime(string city)
        {
                HttpResponseMessage response = await _httpClient.GetAsync($"/api/Time/current/zone?timeZone=Europe/{city}");
                //await EnsureSuccess(response.StatusCode, response.Content);

                var content = await response.Content.ReadAsStringAsync();

                TimeRoot timeContent = JsonConvert.DeserializeObject<TimeRoot>(content);
                return timeContent;
        }


    }
}
