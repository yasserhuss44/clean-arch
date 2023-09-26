namespace Transportation.Infrastructure.Persistence.Db.Mapping;
public class DriverMapping : EntityTypeConfiguration<Driver>
{
    public override void Configure(EntityTypeBuilder<Driver> builder)
    {
        builder.ToTable(nameof(Driver), DBSchemaNames.Transportation.ToString());
    }
}

