
namespace School.Infrastructure.Persistence.Db;
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

//dotnet ef migrations add SchoolDbContext_InitialCreate  --context SchoolDbContext  --startup-project ./Apis/Apis.csproj  --project ./Services/School/School.Infrastructure  --verbose -o ./Persistence/Db/Migrations 

//dotnet ef database update               --context SchoolDbContext  --startup-project ./Apis/Apis.csproj  --project ./Services/School/School.Infrastructure  --verbose
