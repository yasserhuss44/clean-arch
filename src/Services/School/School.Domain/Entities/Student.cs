namespace School.Domain.Entities;

public class Student : EntityBase<Guid>, IEntity<Guid>
{
    public Student()
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
