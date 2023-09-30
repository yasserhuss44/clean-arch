using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Base;

[Serializable]
public partial class LookupEntityBase<TKey>
{
    public string NameAr { get; set; }

    public string NameEn { get; set; }

    public bool IsActive { get; set; }

    [NotMapped]
    public string LName => NameAr;
}
public class LookupEntityBase: EntityBase
{
    public LookupEntityBase()
    { }
    public LookupEntityBase(int val, string nameAr, string nameEn = "")
    {
        NameAr = nameAr;
        NameEn = nameEn;
        Id = val;
        CreatedBy = "Admin";
        IsDeleted = false;
        IsActive = true;
        Description = nameAr;
    }
    public int Id { get; set; }

    public string NameAr { get; set; }

    public string Description { get; set; }

    public string NameEn { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public string CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }
}
