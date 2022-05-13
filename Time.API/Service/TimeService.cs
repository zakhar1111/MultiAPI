using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Time.API.Exceptions;
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

        private static  void  EnsureSuccess(HttpStatusCode statusCode)
        {
            switch (statusCode)
            {
                case HttpStatusCode.BadRequest:
                    throw new TimeBadRequestException("Bad Request of TimeService");

                case HttpStatusCode.InternalServerError:
                    throw new TimeInternalServerErrorException($" InternalServerError of TimeService");
            }
            return;
        }
        public async Task<TimeRoot> GetLocal()
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"/api/Time/current/zone?timeZone=Europe/kiev");
            EnsureSuccess(response.StatusCode);

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TimeRoot>(content);
        }

        public async Task<TimeRoot> GetTime(string city)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"/api/Time/current/zone?timeZone=Europe/{city}");
            EnsureSuccess(response.StatusCode);

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TimeRoot>(content);
        }


    }
}
