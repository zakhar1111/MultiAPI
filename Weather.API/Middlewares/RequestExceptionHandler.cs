using System.Net;
using System.Net.Http;

namespace Weather.API.Middlewares
{
    public class RequestExceptionHandler : IRequestExceptionHandler
    {
        public ErrorType HandleException(HttpRequestException e)
        {
            var Status = e.StatusCode;
            //TODO - var error = Status switch { }
            if (Status == HttpStatusCode.NotFound)
                return new ErrorType { cod = "404", message = $"city not found. {e.Message}" };
            if (Status == HttpStatusCode.Unauthorized)
                return new ErrorType { cod = "401", message = $"Invalid API key. Please see http://openweathermap.org/faq#error401 for more info. {e.Message}" };
            return new ErrorType { cod = "500", message = e.Message };

        }
    }
}
