using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Base;


[Serializable]
public partial class EntityBase : IEntity
{
    public bool IsActive { get; set; } = true;

    public bool IsDeleted { get; set; }
}

[Serializable]
public partial class EntityBase<TKey> : EntityBase, IEntity<TKey>
{
    public TKey Id { get; set; }

}

public interface IExcludeFromDeleteRestrict
{

}
public partial class FullAuditedEntityBase<TKey> : EntityBase<TKey>
{

    public string CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public string UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }
}

public partial class FullAuditedEntityBase : EntityBase, IEntity
{
    public string CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public string UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

}



[Serializable]
public partial class LookupEntityBase<TKey> : FullAuditedEntityBase<TKey>
{
    public string NameAr { get; set; }

    public string NameEn { get; set; }

    public bool IsActive { get; set; }

    [NotMapped]
    public string LName => NameAr;
}
public class LookupEntityBase : EntityBase
{
    public LookupEntityBase()
    {

    }
    public LookupEntityBase(int val, string nameAr, string nameEn = "")
    {
        NameAr = nameAr;
        NameEn = nameEn;
        Id = val;
        CreatedBy = "Admin";
        IsDeleted = false;
        IsActive = true;
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


[Serializable]
public partial class LookupEntityBase2<TKey> : FullAuditedEntityBase<TKey>
{
    public LookupEntityBase2()
    {
        IsActive = true;
    }
    public string NameAr { get; set; }

    public string NameEn { get; set; }

    public bool? IsActive { get; set; }
}


/// <summary>
/// Defines an entity. It's primary key may not be "Id" or it may have a composite primary key.
/// Use <see cref="IEntity{TKey}"/> where possible for better integration to repositories and other structures in the framework.
/// </summary>
public interface IEntity
{

}

/// <summary>
/// Defines an entity with a single primary key with "Id" property.
/// </summary>
/// <typeparam name="TKey">Type of the primary key of the entity</typeparam>
public interface IEntity<TKey> : IEntity
{
    /// <summary>
    /// Unique identifier for this entity.
    /// </summary>
    TKey Id { get; set; }
}
