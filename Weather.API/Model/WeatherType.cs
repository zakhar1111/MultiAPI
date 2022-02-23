using System;

namespace Weather.API.Model
{
    public class WeatherType
    {
        public class clouds
        {
            public string all { get; set; }
            public override string ToString() =>
                $"clouds: {all}";
        }

        public class wind
        {
            public double speed { get; set; }
            public override string ToString() =>
                $"wind: {this.speed}";
        }
        public class main
        {
            public double feels_like { get; set; }
            public double temp { get; set; }
            public int humidity { get; set; }
            public int pressure { get; set; }
            public override string ToString() =>
               $"temp: {this.temp} feel_like: {this.feels_like} humidity: {this.humidity}  pressure: {this.pressure}";
        }
        public class root
        {
            public main main { get; set; } //temp
            public wind wind { get; set; } //speed
            public clouds clouds { get; set; }//cloud
            public double dt { get; set; }//date ms

            public string dt_txt { get; set; }

            public override string ToString() =>
                 $" date= {this.GetDate()}  {main.ToString()}  {wind.ToString()}  {clouds.ToString()}  dt_txt = {this.dt_txt}";
            public DateTime GetDate ()
            {
                var day = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Local).ToLocalTime();
                day = day.AddSeconds(this.dt).ToLocalTime();
                return day;
            }

        }

    }
}
