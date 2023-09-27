using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transportation.Application.Buses.DTOs;

namespace School.Infrastructure.Integration.Transport;

internal class ITransportMapper : Profile
{
    public ITransportMapper()
    {
        CreateMap<BusDto, SchooBusDto>().ReverseMap();         
     
    }
}

