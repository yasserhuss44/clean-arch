namespace Infrastructure.Persistence.Db.Mapping;
public class StudentMapping : EntityTypeConfiguration<Student>
{
    public override void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.ToTable(nameof(Course), DBSchemaNames.School.ToString());
    }
}

