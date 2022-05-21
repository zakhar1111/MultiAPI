using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weather.API.Controllers;
using Weather.API.Services;

namespace APIs.Test.Controllers
{
    [TestFixture]
    internal class WeatherControllerTests
    {
        private Mock<IWeatherService> _weatherServiceMock;
        private WeatherController _weatherController;

        [SetUp]
        public void Setup()
        {
            _weatherServiceMock = new Mock<IWeatherService>();
            _weatherController = new WeatherController(_weatherServiceMock.Object); 
        }

        [Test]
        public async Task GetWeatherLocalAsync_WhenCalled_ReturnOk()
        {
            _weatherServiceMock.Setup(x => x.Get());

            var result = await _weatherController.Get();

            _weatherServiceMock.Verify(x => x.Get());
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Test]
        public async Task GetWeatherCityAsync_WhenCalled_ReturnOk()
        {
            _weatherServiceMock.Setup(x => x.Get("amsterdam"));

            var result = await _weatherController.Get("amsterdam");

            _weatherServiceMock.Verify(x => x.Get("amsterdam"));
            result.Should().BeOfType(typeof(OkObjectResult));
        }
    }
}
