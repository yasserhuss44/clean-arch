namespace Infrastructure.Persistence.Db.Mapping;
public class CourseMapping : EntityTypeConfiguration<Course>
{
    public override void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.ToTable(nameof(Course), DBSchemaNames.School.ToString());
    }
}

