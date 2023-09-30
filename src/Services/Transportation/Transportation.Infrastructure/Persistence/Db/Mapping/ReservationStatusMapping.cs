namespace Transportation.Infrastructure.Persistence.Db.Mapping;
public class ReservationStatusMapping : EntityTypeConfiguration<ReservationStatus>
{
    public override void Configure(EntityTypeBuilder<ReservationStatus> builder)
    {
        builder.ToTable(nameof(ReservationStatus), DBSchemaNames.Transportation.ToString());

        builder.HasQueryFilter(d => !d.IsDeleted);

        var companyTypes = typeof(ReservationStatuses).GetEnumLookups();

        builder.HasData(companyTypes);
    }
}