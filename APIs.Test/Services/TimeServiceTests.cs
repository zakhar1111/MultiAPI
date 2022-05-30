using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Time.API.Model;
using Time.API.Service;

namespace APIs.Test.Services
{
    [TestFixture]
    internal class TimeServiceTests
    {
        private readonly Mock<ILogger<TimeService>> _loggerMock = new Mock<ILogger<TimeService>>();
        //private readonly Mock<HttpClient> _httpClientMock = new Mock<HttpClient>();
        private HttpClient _httpClient;

        private  TimeService _timeService;

        [SetUp]
        public void Setup()
        {
            _httpClient = SetHttpClient(HttpStatusCode.OK);
            _timeService = new TimeService(_httpClient, _loggerMock.Object);
        }

        [Test]
        public async Task GetTime_for_city_ShouldLogAppropriateMassage_WhencityExist()
        {
            //Arrange
            var city = "kiev";
            var statusCode = HttpStatusCode.OK;

            //Act
            var timeResult = await _timeService.GetTime(city);

            //Assert
            //_loggerMock.Verify(logger => logger.LogInformation($"Request time for  {city} in TimeService "), Times.AtLeastOnce);
            _loggerMock.VerifyLog(logger => logger.LogInformation("Request time for  {city} in TimeService ", city), Times.AtLeastOnce);
            _loggerMock.VerifyLog(logger => logger.LogInformation("TimeService return {statusCode} on time-request ", statusCode), Times.AtLeastOnce);
        }
        [Test]
        public async Task GetTime_for_city_ShouldLogAppropriateMassage_WhencityNonExist()
        {
            //Arrange
            var city = "kiev1";
            var statusCode = HttpStatusCode.BadRequest;
            var _client = SetHttpClient(statusCode);
            _timeService = new TimeService(_client, _loggerMock.Object);


            try
            {
                //Act
                var timeResult = await _timeService.GetTime(city);
            }
            catch
            {
                //Assert
                _loggerMock.VerifyLog(logger => logger.LogError("TimeService threw {statusCode}", statusCode), Times.AtLeastOnce);
            }
        }

        private HttpClient SetHttpClient(HttpStatusCode statusCode)
        {
            var httpResponse = new HttpResponseMessage
            {
                StatusCode = statusCode,
                Content = new StringContent(String.Empty, Encoding.UTF8, "application/json")
            };
            string url = "http://localhost:1234";

            Mock<HttpMessageHandler> mockHandlerOK = new Mock<HttpMessageHandler>();
            mockHandlerOK.Protected().Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),//Is<HttpRequestMessage>(r => r.Method == HttpMethod.Get && r.RequestUri.ToString().StartsWith(url)),
                ItExpr.IsAny<CancellationToken>()
                ).ReturnsAsync(httpResponse);

            var retHttpClient = new HttpClient(mockHandlerOK.Object);
            retHttpClient.BaseAddress = new Uri(url);
            return retHttpClient;
        }
    }
}
