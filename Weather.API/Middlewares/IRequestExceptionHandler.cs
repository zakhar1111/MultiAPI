using System.Net.Http;

namespace Weather.API.Middlewares
{
    public interface IRequestExceptionHandler
    {
        ErrorType HandleException(HttpRequestException e);

    }
}
