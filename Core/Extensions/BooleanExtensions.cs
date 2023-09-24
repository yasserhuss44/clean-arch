namespace Core.Extensions;
public static class BooleanExtensions
{
    public static bool IsFalsy(this bool flag) => flag == false;
    public static bool NotFalsy(this bool flag) => !flag.IsFalsy();
    public static bool IsNullOrFalsy(this bool? flag) => !flag.HasValue || flag.Value.IsFalsy();
    public static bool NotNullOrFalsy(this bool? flag) => !flag.IsNullOrFalsy();
    public static bool IsNull(this bool? flag) => !flag.HasValue;
    public static bool NotNull(this bool? flag) => !flag.IsNull();
    public static bool IsFalsy(this object obj) => bool.TryParse(obj.ToString() , out var result).IsFalsy() || result.IsFalsy();
    public static bool NotFalsy(this object obj) => !IsFalsy(obj);
}
