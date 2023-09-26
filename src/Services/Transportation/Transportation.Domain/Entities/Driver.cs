namespace Transportation.Domain.Entities;

public class Driver : EntityBase<Guid>, IEntity<Guid>
{
    public Driver()
    {
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
