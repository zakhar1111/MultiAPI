using AutoMapper;
using Time.API.Model;

namespace Time.API.Profiles
{
    public class TimeProfile :Profile
    {
        public TimeProfile()
        {
            
            //model -> DTO
            CreateMap<TimeRoot, DtoTime>();

            //DTO -> model
            CreateMap<TimeRoot, DtoTime>().ReverseMap();
        }
    }
}
