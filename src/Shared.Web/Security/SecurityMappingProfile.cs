using AutoMapper;

namespace Shared.Web.Security;

public sealed class SecurityMappingProfile : Profile
{
    public SecurityMappingProfile()
    {
        CreateMap<DxUser , User>()
            .ForMember(dest => dest.Dob , opt => opt.MapFrom(src => src.Dob.GetCrmDateFromDx()));
    }
}