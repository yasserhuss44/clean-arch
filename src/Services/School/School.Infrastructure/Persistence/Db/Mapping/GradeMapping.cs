namespace School.Infrastructure.Persistence.Db.Mapping;
public class GradeMapping : EntityTypeConfiguration<Grade>
{
    public override void Configure(EntityTypeBuilder<Grade> builder)
    {
        builder.ToTable(nameof(Grade), DBSchemaNames.School.ToString());

        builder.HasQueryFilter(d => !d.IsDeleted);

        var companyTypes = typeof(Grades).GetEnumLookups();

        builder.HasData(companyTypes);
    }
}