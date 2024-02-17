namespace Shared.Application.SystemFeatures.DTOs;

public sealed class SystemFeatureItemDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<SubSystemFeatureItemDto> SubFeatures { get; set; }
}

public sealed class SubSystemFeatureItemDto
{
    public int Id { get; set; }
    public string Name { get; set; }

}