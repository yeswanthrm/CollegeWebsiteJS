using AutoMapper;
using CollegeWebsiteAPI.DTOs;
using CollegeWebsiteAPI.Entities;

namespace CollegeWebsiteAPI.Helpers
{
    public class AutoMapperProfiles : Profile
    {       

        public AutoMapperProfiles()
        {
            CreateMap<Registration, RegisterDto>().ReverseMap();
            CreateMap<Registration, ViewAllRegistrationsDto>().ReverseMap();
        }
    }
}
