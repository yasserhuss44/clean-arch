
namespace Core.Base;

public partial class FullAuditedEntityBase<TKey> : EntityBase<TKey>
{

    public bool IsActive { get; set; } = true;

    public bool IsDeleted { get; set; }

    public string CreatedBy { get; set; } = "System";

    public DateTime CreatedOn { get; set; }= DateTime.Now;

    public string? UpdatedBy { get; set; } = "System";

    public DateTime? UpdatedOn { get; set; } = DateTime.Now;
}
