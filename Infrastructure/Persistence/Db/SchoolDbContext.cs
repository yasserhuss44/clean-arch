
namespace Infrastructure.Persisitence.Db;
public class SchoolDbContext : DbContext
{
    public SchoolDbContext(DbContextOptions<SchoolDbContext> options)
        : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyCommonSettings();

        modelBuilder.ApplyConfiguration(typeof(SchoolDbContext).Assembly);

        modelBuilder.ApplyCascadeSettings();

        base.OnModelCreating(modelBuilder);

    }

}

//add-migration -context SchoolDbContext "SchoolDbContext_InitialCreate"    -o ./Persistence/Db/Migrations

//dotnet ef migrations add InitialCreate  --context SchoolDbContext  --startup-project ./Apis  --project ./Infrastructure  --verbose -o ./Persistence/Db/Migrations 

//dotnet ef database update               --context SchoolDbContext  --startup-project ./Apis  --project ./Infrastructure  --verbose
