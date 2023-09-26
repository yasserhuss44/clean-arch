using Transportation.Application.Drivers.DTOs;


namespace Transportation.Application.Services.SystemFeatures;

public sealed class DriverMapping : Profile
{
    public DriverMapping()
    {

        CreateMap<DriverDto, Driver>().ReverseMap();


        CreateMap<CreateDriverDto, Driver>().ReverseMap(); 


        CreateMap<UpdateDriverDto, Driver>().ReverseMap();
    }
}

