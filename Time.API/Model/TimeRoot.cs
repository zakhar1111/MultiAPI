using System;

namespace Time.API.Model
{
    public class TimeRoot
    {
        public int Year { get; set; } //": 2022,
        public int Month { get; set; }//": 3,
        public int Day { get; set; }//": 9,
        public int Hour { get; set; }//": 20,
        public int Minute { get; set; }//": 4,

        public override string ToString() =>
            $"Year: {this.Year} Month: {this.Month} Day: {this.Day}  Hour: {this.Hour} Hour: {this.Hour} Minute: {this.Minute} Seconds: {this.Seconds} CurrentDate: {this.CurrentDateTime}";
        public int Seconds { get; set; }//": 43,
                                        //"milliSeconds": 158,
        public DateTime CurrentDateTime { get; set; }//": "2022-03-09T20:04:43.1582561",
                                                   //public Date CurentDate { get; set; }//": "03/09/2022",
                                                   //"time": "20:04",
                                                   //"timeZone": "Europe/Kiev",
                                                   //"dayOfWeek": "Wednesday",
    }
}
