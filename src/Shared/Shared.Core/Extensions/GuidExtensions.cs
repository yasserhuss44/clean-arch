namespace Core.Extensions;

public static class GuidExtensions
{
    public static bool IsEmpty(this Guid guid) => guid == Guid.Empty;

    public static bool NotEmpty(this Guid guid) => !guid.IsEmpty();

    public static bool IsNullOrEmpty(this Guid? guid) => !guid.HasValue || guid.Value.IsEmpty();

    public static bool NotNullOrEmpty(this Guid? guid) => !guid.IsNullOrEmpty();
}
