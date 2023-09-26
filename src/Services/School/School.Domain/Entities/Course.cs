namespace School.Domain.Entities;

public class Course : EntityBase<Guid>, IEntity<Guid>
{
    public Course()
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
