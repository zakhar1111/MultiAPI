using System;

namespace Time.API.Service.Exceptions
{
    public sealed class TimeServiceInvalidZone : Exception
    {
        public TimeServiceInvalidZone()
        {}
        public TimeServiceInvalidZone(string message) : base(message) 
        { }
        public TimeServiceInvalidZone(string message, Exception inner) : base(message, inner) 
        { }
    }

}
