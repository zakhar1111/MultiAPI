namespace Weather.API.Model
{
    public class DTOWeatherbject
    {
        //public string cod { get; set; }
        //public int message { get; set; }
        public int cnt { get; set; }
        public List[] list { get; set; }
        public DTOCity city { get; set; }
    }

    public class DTOCity
    {
        //public int id { get; set; }
        public string name { get; set; }
       // public Coord coord { get; set; }
       // public string country { get; set; }
        //public int population { get; set; }
        //public int timezone { get; set; }
       // public int sunrise { get; set; }
       // public int sunset { get; set; }
    }

    //public class Coord
    //{
        //public float lat { get; set; }
        //public float lon { get; set; }
    //}

    public class List
    {
        public int dt { get; set; }
        public DTOMain main { get; set; }
        public DTOWeather[] weather { get; set; }
        public DTOClouds clouds { get; set; }
        public DTOWind wind { get; set; }
        //public int visibility { get; set; }
        //public float pop { get; set; }
        //public DTORain rain { get; set; }
        //public DTOSys sys { get; set; }
        public string dt_txt { get; set; }
    }

    public class DTOMain
    {
        public float temp { get; set; }
        //public float feels_like { get; set; }
        //public float temp_min { get; set; }
        //public float temp_max { get; set; }
        //public int pressure { get; set; }
        //public int sea_level { get; set; }
        //public int grnd_level { get; set; }
        public int humidity { get; set; }
       // public float temp_kf { get; set; }
    }

    public class DTOClouds
    {
        //public int all { get; set; }
    }

    public class DTOWind
    {
        public float speed { get; set; }
        //public int deg { get; set; }
        //public float gust { get; set; }
    }

    //public class DTORain
    //{
    //    public float _3h { get; set; }
    //}

    //public class DTOSys
    //{
    //    public string pod { get; set; }
    //}

    public class DTOWeather
    {
        //public int id { get; set; }
        public string main { get; set; }
        //public string description { get; set; }
        //public string icon { get; set; }
    }

}
