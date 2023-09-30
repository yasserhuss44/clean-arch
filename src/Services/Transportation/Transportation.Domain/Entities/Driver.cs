namespace Transportation.Domain.Entities;

public class Driver : FullAuditedEntityBase<Guid>, IEntity<Guid>
{
    public Driver()
    {
        this.CreatedOn = DateTime.Now;
        this.CreatedBy = "Yasser";
        this.Id = Guid.NewGuid();
    }
    public string Name { get; set; }
    public string NameAr { get; set; }

    public void Update(string name, string nameAr)
    {
        this.Name = name;
        this.NameAr = nameAr;
    }
}
