using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<TimeService> _logger;

        public TimeService(HttpClient httpClient, ILogger<TimeService> logger) 
        {
            _httpClient = httpClient;
            _logger = logger;
            _logger.LogInformation("Create TimeService");
        }

        private  void  EnsureSuccess(HttpStatusCode statusCode)
        {//TODO: Refactor it seems this code should be removed to somewhere
            //TODO - logic has to be changed. remove wraping around status code. check city on null 
            switch (statusCode)
            {
                case HttpStatusCode.BadRequest:
                    {
                        _logger.LogError("TimeService threw {statusCode}", statusCode);
                        throw new TimeBadRequestException("Bad Request of TimeService");
                    }
                case HttpStatusCode.InternalServerError:
                    {
                        _logger.LogError("TimeService threw {statusCode}", statusCode);
                        throw new TimeInternalServerErrorException($" InternalServerError of TimeService");
                    }
                    
            }
            _logger.LogInformation("TimeService return {statusCode} on time-request ", statusCode);
            return;
        }
        public async Task<TimeRoot> GetLocal()
        {
            _logger.LogInformation("Request time for  default city  in TimeService ");
            return await this.GetTime("kiev");
        }

        public async Task<TimeRoot> GetTime(string city)
        {
            _logger.LogInformation("Request time for  {city} in TimeService ", city);
            //_logger.Log(LogLevel.Information, "Request time for  {city} in TimeService ", city);

            HttpResponseMessage response = await _httpClient.GetAsync($"/api/Time/current/zone?timeZone=Europe/{city}");
            EnsureSuccess(response.StatusCode);

            var content = await response.Content.ReadAsStringAsync();//TODO - replace by GetStringAsync()
            return JsonConvert.DeserializeObject<TimeRoot>(content);//TODO - try to use System.Net.Http.Json package for deserialize
        }


    }
}
