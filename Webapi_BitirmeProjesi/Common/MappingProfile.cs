using AutoMapper;
using Webapi_BitirmeProjesi.Entities;
using Webapi_BitirmeProjesi.DTOs;

namespace Webapi_BitirmeProjesi.Common
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<UserRegisterModel, User>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => ((RoleEnum)src.RoleId).ToString()));

            CreateMap<CompanyRegisterModel, Company>();

            CreateMap<CreateEventModel, Event>();
        }
    }
}
