
namespace Transportation.Infrastructure.Persistence.Db;
public class TransportationDbContext : DbContext
{
    public TransportationDbContext(DbContextOptions<TransportationDbContext> options)
        : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyCommonSettings();

        modelBuilder.ApplyConfiguration(typeof(TransportationDbContext).Assembly);

        modelBuilder.ApplyCascadeSettings();

        base.OnModelCreating(modelBuilder);

    }

}

//dotnet ef migrations add TransportationDbContext_AddReserveBus  --context TransportationDbContext  --startup-project ./Apis/Apis.csproj  --project ./Services/Transportation/Transportation.Infrastructure  --verbose -o ./Persistence/Db/Migrations 


//dotnet ef database update               --context TransportationDbContext  --startup-project ./Apis/Apis.csproj  --project ./Services/Transportation/Transportation.Infrastructure  --verbose
