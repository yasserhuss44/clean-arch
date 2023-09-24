namespace Domain.Entities;

public class Course:EntityBase<Guid>,IEntity<Guid>
{
        public string Name { get; set; }
        public string NameAr { get; set; }
}
