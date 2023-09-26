namespace Transportation.Infrastructure.Persistence.Db.Mapping;
public class BusMapping : EntityTypeConfiguration<Bus>
{
    public override void Configure(EntityTypeBuilder<Bus> builder)
    {
        builder.ToTable(nameof(Driver), DBSchemaNames.Transportation.ToString());
    }
}

