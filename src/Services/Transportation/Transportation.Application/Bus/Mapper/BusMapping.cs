using Transportation.Application.Buses.DTOs;
 

namespace Transportation.Application.Buses.Mapping;

public sealed class BusMapping : Profile
{
    public BusMapping()
    {

        CreateMap<Bus, BusDto>();


        CreateMap<CreateBusDto,Bus >();


        CreateMap<UpdateBusDto,Bus >();
    }
}

