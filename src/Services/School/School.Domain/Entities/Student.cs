namespace School.Domain.Entities;

public class Student : FullAuditedEntityBase<Guid>, IEntity<Guid>
{
    public Student()
    { 
        this.Id = Guid.NewGuid();
    }
    public string Name { get; private set; }
    public string NameAr { get; private set; }
    public int GradeId { get; private set; }
    public Grade Grade { get; private set; }

    public void UpdateNames(string name, string nameAr)
    {
        this.Name = name;
        this.NameAr = nameAr;
    }

    public void AssignToGrade(int grade)
    {
        this.GradeId = grade;
    }

}
