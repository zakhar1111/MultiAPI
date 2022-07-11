﻿using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Weather.API.Exceptions;
using Weather.API.Model;

namespace Weather.API.Services
{
    public class WeatherService : IWeatherService
    {
        private IConfiguration _configRoot;
        private HttpClient _httpClient;

        private string APPID
        {//TODO - replace by Option Pattern
            get 
            { 
                return _configRoot.GetValue<string>("EndPoint:APPID");
            }
        }
      
        private string DefaultCity
        {//TODO - replace by Option Pattern
            get 
            { 
                return _configRoot.GetValue<string>("Location:Default");
            }
        }

        public WeatherService(IConfiguration configRoot,HttpClient httpClient)
        {//TODO - remove dependency for IConfiguration, move it to startup
            _configRoot = configRoot;
            _httpClient = httpClient;
        }
        public async Task<WeatherObject> Get()
        {
            return await Get(DefaultCity);
        }

        public async Task<WeatherObject> Get(string city)
        {//TODO - add mapping RootWeather --> WeatherObject -- see 21 Rahul
            //TODO - put Root object that return Weather[]
            string APIURL = $"?q={city}&appid={APPID}&units=metric&cnt=1";
            var response = await _httpClient.GetAsync(APIURL);

            EnsureSuccess(response.StatusCode);

            var content =  await response.Content.ReadAsStringAsync();//TODO - replace by GetStringAsync()
            var result = JsonConvert.DeserializeObject<WeatherObject>(content);
            return result;
        }

        private static void EnsureSuccess(HttpStatusCode statusCode)
        {//TODO: Refactor it seems this code should be removed to somewhere
            //TODO - logic should check city == null || empty
            if (statusCode == HttpStatusCode.BadRequest)
                throw new WeatherBadRequestException("Bad Request of WeatherService");
            if (statusCode == HttpStatusCode.NotFound)
                throw new WeatherNotFoundException("WeatherService does Not Found city ");
        }
    }
}
