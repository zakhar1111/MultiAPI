using System;

namespace Time.API.Exceptions
{
    public class TimeException : Exception
    {
        public TimeException(string massage) : base(massage)
        {
        
        }
    }

    public class TimeBadRequestException : TimeException
    {
        public TimeBadRequestException(string massage) : base(massage)  
        {

        }
    }

    public class TimeInternalServerErrorException : TimeException
    {
        public TimeInternalServerErrorException(string massage) : base(massage)
        {

        }
    }
}
