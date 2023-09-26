namespace Transportation.Domain.Entities;

public class Bus : EntityBase<Guid>, IEntity<Guid>
{
    public Bus()
    {
        this.Id= Guid.NewGuid();
    }
    public string Name { get; set; }
    public string NameAr { get; set; }

    public void Update(string name, string nameAr)
    {
        this.Name = name;
        this.NameAr = nameAr;
    }
}
