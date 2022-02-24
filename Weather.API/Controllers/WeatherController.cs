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

namespace Weather.API.Controllers
{
    [Route("api/[controller]")]   
    [ApiController]              
    public class WeatherController : ControllerBase
    {
        private IConfigurationRoot ConfigRoot;
        public WeatherController(IConfiguration configRoot)
        {
            ConfigRoot = (IConfigurationRoot)configRoot;
        }


        [HttpGet]                
        public string Get()
        {
            return getWeather(this.DefaultLocation);
            
        }

        [HttpGet("{id}", Name = "Get")]
        public string Get(string id)
        {
            return getWeather(id);
        }

        
      
        public string DefaultLocation
        {
            get
            { 
                string location = ConfigRoot.GetValue<string>("Location:Default");
                return location;
            }
        }

        public string APPID
        {
            get
            {
                string weatherAppID = ConfigRoot.GetValue<string>("EndPoint:APPID");
                return weatherAppID;
            }
        }

        

        private string AddCityToUri(string city)
        {
            return $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={APPID}&units=metric&cnt=1";
        }

        private string getWeather(string city)
        {
            var url = AddCityToUri(city);
            //var s = new HttpClient();
            using (var web = new WebClient())
            {
                var json = web.DownloadString(url);
                var result = JsonConvert.DeserializeObject<WeatherType.root>(json);
                WeatherType.root outPut = result;
                return outPut.ToString();
            }
        }
     
    }
}
