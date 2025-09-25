using AutoMapper;
using stamp_back.Dto;
using stamp_back.Models;

namespace stamp_back.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
                CreateMap<User,UserDto>();
        }
    }
}
