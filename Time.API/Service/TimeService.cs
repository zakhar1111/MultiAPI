﻿using Microsoft.AspNetCore.Http;
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
        {
            switch (statusCode)
            {
                case HttpStatusCode.BadRequest:
                    {
                        _logger.LogError($"TimeService threw {statusCode}");
                        throw new TimeBadRequestException("Bad Request of TimeService");
                    }
                case HttpStatusCode.InternalServerError:
                    {
                        _logger.LogError($"TimeService threw {statusCode}");
                        throw new TimeInternalServerErrorException($" InternalServerError of TimeService");
                    }
                    
            }
            _logger.LogInformation($" TimeService return {statusCode} on time-request ");
            return;
        }
        public async Task<DtoTime> GetLocal()
        {
            _logger.LogInformation(" Request time for  default city  in TimeService ");
            return await this.GetTime("kiev");
        }

        public async Task<DtoTime> GetTime(string city)
        {
            _logger.LogInformation($"Request time for  {city} in TimeService ");
            HttpResponseMessage response = await _httpClient.GetAsync($"/api/Time/current/zone?timeZone=Europe/{city}");
            EnsureSuccess(response.StatusCode);

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<DtoTime>(content);
        }


    }
}
