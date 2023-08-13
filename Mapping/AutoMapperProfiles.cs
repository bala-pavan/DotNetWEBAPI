using AutoMapper;
using WebSampleApplicationAPI.Models.Domain;
using WebSampleApplicationAPI.Models.DTO;

namespace WebSampleApplicationAPI.Mapping
{
    public class AutoMapperProfiles: Profile 
    {
        public AutoMapperProfiles()
        {
            // CreateMap<UserDTO,UserDomain>().ReverseMap(); //same mathod name for both.
            /*
            //different method name for both
            CreateMap<UserDTO,UserDomain>()
                 .ForMember(x=>x.Name,opt=>opt.MapFrom(x=>x.FullName))
                 .ReverseMap();
            */

            CreateMap<Region, RegionDto>().ReverseMap();
            CreateMap<AddRegionRequestDto,Region>().ReverseMap();
            CreateMap<UpdateRegionRequestDto,Region>().ReverseMap();
            CreateMap<AddWalkRequestDto,Walk>().ReverseMap();
            CreateMap<Walk,WalkDto>().ReverseMap();
            CreateMap<Difficulty, DifficultyDto>().ReverseMap();
            CreateMap<UpdateWalkRequestDto, Walk>().ReverseMap();
        }
    }

}
