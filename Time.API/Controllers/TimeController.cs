using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Time.API.Exceptions;
using Time.API.Model;
using Time.API.Service;
//using System.Web.Http;

namespace Weather.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeController : ControllerBase
    {
        private readonly ITimeService _timeService;

        public TimeController(ITimeService timeService)
        {
            _timeService = timeService;  
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var timeLocalContent = await _timeService.GetLocal();
            return new OkObjectResult(timeLocalContent);
        }

        [HttpGet("{cityName}", Name = "Get")]
        //[Route("europe/{id}")]
        public async Task<IActionResult> Get(string cityName)
        {
            var timeContent = await _timeService.GetTime(cityName);
            return new OkObjectResult(timeContent);
        }
    }
}