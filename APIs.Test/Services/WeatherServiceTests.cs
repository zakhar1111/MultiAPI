
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Weather.API.Services;
using Weather.API.Model;
using FluentAssertions;

namespace APIs.Test.Services
{
    [TestFixture]
    internal class WeatherServiceTests
    {
        private Mock<IWeatherService> _weatherServiceMock;

        private Mock<IConfiguration> _configurationMock;
        private Mock<HttpClient> _httpClientMock;

        private WeatherService _weatherService;
        [SetUp]
        public void Setup()
        {
            _weatherServiceMock = new Mock<IWeatherService>();
            _configurationMock = new Mock<IConfiguration>();    
            _httpClientMock = new Mock<HttpClient>();   
            _weatherService = new WeatherService(_configurationMock.Object, _httpClientMock.Object);
        }

        [Test]
        public async Task WeatherLocalAsync_WhenCalled_WeatherObject()
        {
            WeatherObject val = new WeatherObject(); // It.IsAny<WeatherObject>();
            _weatherServiceMock.Setup(x => x.Get("kiev")).ReturnsAsync(val);

            var result = await _weatherServiceMock.Object.Get("kiev");


            Assert.IsInstanceOf<WeatherObject>(result);
            result.Should().BeOfType(typeof(WeatherObject));
            result.Should().Be(val);


            _weatherServiceMock.Verify(x => x.Get("kiev"));
            

        }

        [Test]
        public async Task WeatherCityAsync_WhenCalled_WeatherObject()
        {
            WeatherObject val = new WeatherObject();// It.IsAny<WeatherObject>();
            string city = It.IsAny<string>();

            _weatherServiceMock.Setup(x => x.Get("kiev")).ReturnsAsync(()=>val);
            
            var output = await _weatherService.Get("kiev");
            output.Should().Be(val);
        }
    }
}
