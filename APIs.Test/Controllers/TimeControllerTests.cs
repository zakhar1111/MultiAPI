using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using Time.API.Model;
using Time.API.Service;
using Weather.API.Controllers;

namespace APIs.Test
{
    [TestFixture]
    public class TimeControllerTests
    {
        private Mock<ITimeService> _timeServiceMock;
        private TimeController _timeController;

        [SetUp]
        public void Setup()
        {
            _timeServiceMock = new Mock<ITimeService>();
            _timeController = new TimeController(_timeServiceMock.Object);
        }

        [Test]
        public void GetLocalTime_WhenCalled_ReturnOk()
        {
            _timeServiceMock.Setup(x => x.GetLocal());

            var result = _timeController.Get().GetAwaiter().GetResult();

            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Test]
        public async Task GetLocalTimeAsync_WhenCalled_ReturnsOk()
        {
            _timeServiceMock.Setup(x => x.GetLocal());

            var result = await _timeController.Get();

            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Test]
        public void Get_withCity_WhenCalled_ReturnOk()
        {
            var testTime = TestGetLocal();
            _timeServiceMock.Setup(x => x.GetTime("kiev")).Returns(testTime);

            var result = _timeController.Get("kiev").GetAwaiter().GetResult();

            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Test]
        public async Task GetCityTimeAsync_WhenCalled_ReturnOk()
        {
            var testTime = await TestGetLocalAsync();
            _timeServiceMock.Setup(x => x.GetTime("kiev")).ReturnsAsync(testTime);

            var result = await _timeController.Get("kiev");

            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Test]
        public void TestGetTime()
        {
            //Arrange
           
            var testTime = TestGetLocal();
            _timeServiceMock.Setup(x => x.GetLocal()).Returns(testTime);
            

            //Act
            var result = _timeController.Get().GetAwaiter().GetResult();

            //Assert
            _timeServiceMock.Verify(x => x.GetLocal());
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Not.SameAs(testTime));
           
        }

        private Task<DtoTime> TestGetLocal()
        {
            var time = new DtoTime
            {
                timeZone = "Europe/Kiev",
                date = "05/20/2022",
                dayOfWeek = "Friday",
                time = "20:18"
            };
            return Task.FromResult(time);   
        }

        private async Task<DtoTime> TestGetLocalAsync()
        {
            var time = new DtoTime
            {
                timeZone = "Europe/Kiev",
                date = "05/20/2022",
                dayOfWeek = "Friday",
                time = "20:18"
            };
            return await Task.FromResult(time);//Task.FromResult(time);
        }
    }
}