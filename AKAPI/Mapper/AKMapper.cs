using AKAPI.Models;
using AutoMapper;

namespace AKAPI.Mapper
{
    public class AKMapper : Profile
    {
        public AKMapper()
        {
            CreateMap<NationalPark, NationalParkDTO>().ReverseMap();
        }
    }
}
