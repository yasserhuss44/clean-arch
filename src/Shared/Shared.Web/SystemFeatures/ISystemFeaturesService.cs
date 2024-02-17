using Shared.Application.SystemFeatures.DTOs;

namespace Shared.Application.SystemFeatures;
public interface ISystemFeaturesService
{
    Task<List<SystemFeatureItemDto>> Get(
        int category ,
        CancellationToken cancellationToken = default);

    Task<bool> IsFeatureEnabledAsync(
        string featureName ,
        CancellationToken cancellationToken = default);
}