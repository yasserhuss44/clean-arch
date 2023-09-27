namespace Transportation.Domain.Entities;

public class Bus : EntityBase<Guid>, IEntity<Guid>
{
    public Bus()
    {
        this.Id= Guid.NewGuid();
    }
    public string Name { get; private set; }
    public string NameAr { get; private set; }
    public bool IsReserved { get; private set; }

    public void Reserve()
    {
        this.IsReserved = true;
    }

    public void Update(string name, string nameAr)
    {
        this.Name = name;
        this.NameAr = nameAr;
    }
}
