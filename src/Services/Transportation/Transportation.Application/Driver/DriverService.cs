using Transportation.Application.Drivers.DTOs;

namespace Transportation.Application.Drivers;
public class DriverService : IDriverService
{
    private readonly ITransportationUnitOfWork<TransportationDbContext> unitOfWork;
    private readonly IMapper mapper;

    public DriverService(ITransportationUnitOfWork<TransportationDbContext> unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public Task<PagedListDto<DriverDto>> SearchDrivers(DriverFilter filter)
    {
        throw new NotImplementedException();
    }

    public async Task<DriverDto> GetDriver(Guid id)
    {
        var repository = unitOfWork.GetRepository<Driver>();

        var entity = await repository.GetByIdAsync(id);

        var dto = this.mapper.Map<DriverDto>(entity);

        return dto;
    }

    public async Task<bool> CreateNewDriver(CreateDriverDto dto)
    {
        var repository = unitOfWork.GetRepository<Driver>();

        var course = mapper.Map<Driver>(dto);

        repository.Add(course);

        await unitOfWork.SaveChangesAsync();

        return await Task.FromResult(true);
    }

    public async Task<bool> UpdateDriver(UpdateDriverDto dto)
    {
        var repository = unitOfWork.GetRepository<Driver>();

        var course = await repository.GetByIdAsync(dto.Id);

        course.Update(name: dto.Name,
                       nameAr: dto.NameAr);

        repository.Update(course);

        await unitOfWork.SaveChangesAsync();

        return await Task.FromResult(true);
    }

    public async Task<bool> DeleteDriver(Guid id)
    {
        var repository = unitOfWork.GetRepository<Driver>();

        var course = await repository.GetByIdAsync(id);

        repository.Remove(course);

        return await Task.FromResult(true);

    }

}

