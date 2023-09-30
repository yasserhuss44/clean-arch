namespace School.Domain.Entities;

public class Course : FullAuditedEntityBase<Guid>, IEntity<Guid>
{
    public Course()
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
