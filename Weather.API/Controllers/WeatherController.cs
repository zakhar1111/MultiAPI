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
        public async Task<IActionResult> Get()
        {
            var weatherDefaultContent = await _weatherService.Get();
            return new OkObjectResult(weatherDefaultContent);
        }

        [HttpGet("{city}", Name = "Get")]
        public async Task<IActionResult> Get(string city)
        {
            //TODO - add mapping RootWeather --> WeatherObject -- see 21 Rahul
            //TODO - add Mapping Rootobject to DTOWeather as 21 Rahul
            var content = await _weatherService.Get(city);
            return new OkObjectResult(content);
        }
    }
}
