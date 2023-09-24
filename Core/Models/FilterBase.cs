namespace Core.Models;

public partial class FilterBase
{
    public int Offset { get; set; } = 0;

    public int PageSize { get; set; } = 10;   
}