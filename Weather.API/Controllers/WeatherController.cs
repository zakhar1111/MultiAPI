using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

using Weather.API.Model;
using Weather.API.Services;

namespace Weather.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherService _weatherService;

        public WeatherController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        [HttpGet]
        public async Task<WeatherObject> Get()
        {
            var content = await _weatherService.Get();
            return JsonConvert.DeserializeObject<WeatherObject>(content);
        }

        [HttpGet("{id}", Name = "Get")]
        public async Task<WeatherObject> Get(string id)
        {
            var content = await _weatherService.Get(id);
            var weatherObject = JsonConvert.DeserializeObject<WeatherObject>(content);
            return weatherObject;
        }
    }
}
