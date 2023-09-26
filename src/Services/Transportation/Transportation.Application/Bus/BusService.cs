using Transportation.Application.Buses.DTOs;

namespace Transportation.Application.Buses;
public class BusService : IBusService
{
    private readonly ITransportationUnitOfWork<TransportationDbContext> unitOfWork;
    private readonly IMapper mapper;

    public BusService(ITransportationUnitOfWork<TransportationDbContext> unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public Task<PagedListDto<BusDto>> SearchBuses(BusFilter filter)
    {
        throw new NotImplementedException();
    }

    public async Task<BusDto> GetBus(Guid id)
    {
        var repository = unitOfWork.GetRepository<Bus>();

        var entity = await repository.GetByIdAsync(id);

        var dto = this.mapper.Map<BusDto>(entity);

        return dto;
    }

    public async Task<bool> CreateNewBus(CreateBusDto dto)
    {
        var repository = unitOfWork.GetRepository<Bus>();

        var student = mapper.Map<Bus>(dto);

        repository.Add(student);

        await unitOfWork.SaveChangesAsync();

        return await Task.FromResult(true);
    }

    public async Task<bool> UpdateBus(UpdateBusDto dto)
    {
        var repository = unitOfWork.GetRepository<Bus>();

        var student = await repository.GetByIdAsync(dto.Id);

        student.Update(name: dto.Name,
                       nameAr: dto.NameAr);

        repository.Update(student);

        await unitOfWork.SaveChangesAsync();

        return await Task.FromResult(true);
    }

    public async Task<bool> DeleteBus(Guid id)
    {
        var repository = unitOfWork.GetRepository<Bus>();

        var student = await repository.GetByIdAsync(id);

        repository.Remove(student);

        return await Task.FromResult(true);

    }

}

