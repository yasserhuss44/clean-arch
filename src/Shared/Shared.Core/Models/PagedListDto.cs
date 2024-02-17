namespace Core.Models;

public partial class PagedListDto<T>
{
    public int? Offset { get; set; }

    public int? TotalItemsCount { get; set; }

    public ICollection<T> Items { get; set; }

}
