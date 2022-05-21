using System;

namespace Weather.API.Exceptions
{
    public class WeatherException : Exception
    {
        public WeatherException(string message) : base(message)
        {

        }
    }

    public class WeatherBadRequestException : WeatherException
    {
        public WeatherBadRequestException(string message) : base(message)
        {

        }
    }

    public class WeatherNotFoundException : Exception
    {
        public WeatherNotFoundException(string message): base(message)  
        {

        }
    }
}
