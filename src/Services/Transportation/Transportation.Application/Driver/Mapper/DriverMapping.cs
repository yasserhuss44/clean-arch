using Transportation.Application.Drivers.DTOs;
 

namespace Transportation.Application.Services.SystemFeatures;

public sealed class DriverMapping : Profile
{
    public DriverMapping()
    {

        CreateMap<DriverDto,Driver >();


        CreateMap<CreateDriverDto,Driver >();


        CreateMap<UpdateDriverDto,Driver >();
    }
}

