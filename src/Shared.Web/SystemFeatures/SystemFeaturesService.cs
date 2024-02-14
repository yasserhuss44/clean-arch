using Shared.Application.SystemFeatures.DTOs;

namespace Shared.Application.SystemFeatures;

public sealed class SystemFeaturesService :
    ISystemFeaturesService,
    IScopedService
{
    //private readonly ICrmApiClient crmProxy;
    private readonly IMapper mapper;

    public SystemFeaturesService(
        //ICrmApiClient crmProxy ,
        IMapper mapper)
    {
        //  this.crmProxy = crmProxy;
        this.mapper = mapper;
    }

    public async Task<List<SystemFeatureItemDto>> Get(
        int category,
        CancellationToken cancellationToken = default)
    {
        var features = new List<SystemFeatureItemDto> { new SystemFeatureItemDto {
            Id= 1,
            Name="Category"
        } };
        //await crmProxy.GetSystemFeaturesAsync(
        //        category ,
        //        cancellationToken);

        var systemFeatures = mapper.Map<List<SystemFeatureItemDto>>(features);

        return systemFeatures;

    }

    public async Task<bool> IsFeatureEnabledAsync(
        string featureName,
        CancellationToken cancellationToken = default)
    {
        var systemFeatures = await Get(0, cancellationToken);

        if (systemFeatures.IsNullOrEmpty())
            return false;

        var feature = systemFeatures.FirstOrDefault(s => s.Name.IsEqual(featureName));

        if (feature.IsNull())
            return false;

        return true;
    }
}