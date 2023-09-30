using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection;

namespace Core.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void ApplyCommonSettings(this ModelBuilder modelBuilder)
        {

            foreach (var property in modelBuilder.Model.GetEntityTypes()
          .SelectMany(t => t.GetProperties())
          .Where(p => p.ClrType == typeof(decimal)))
            {
                property.SetProviderClrType(typeof(decimal));
            }

            foreach (var property in modelBuilder.Model.GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(p => p.ClrType == typeof(DateTime)))
            {
                property.SetProviderClrType(typeof(DateTime));
            }
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                    .SelectMany(t => t.GetProperties())
                    .Where(p => p.ClrType == typeof(string) && (p.Name.Contains("Description") ||
                    p.Name.Contains("Comment") || p.Name.Contains("Note"))))
            {
                if (property.GetMaxLength() == null)
                    property.SetMaxLength(1000);
            }
            foreach (var property in modelBuilder.Model.GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(p => p.ClrType == typeof(string)))
            {
                if (property.GetMaxLength() == null)
                    property.SetMaxLength(250);
            }

            foreach (var property in modelBuilder.Model.GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(p => p.ClrType == typeof(Guid) && p.Name == "Id"))
            {
                property.SetDefaultValueSql("newsequentialid()");
            }



            //foreach (var property in modelBuilder.Model.GetEntityTypes()
            //    .Where(e => e.ClrType.BaseType == typeof(LookupEntityBase))
            //.SelectMany(t => t.GetProperties())
            //.Where(p => p.ClrType == typeof(int) && p.Name == "Id"))
            //{
            //    property.SetValueGenerationStrategy(
            //       SqlServerValueGenerationStrategy.SequenceHiLo);
            //}

            foreach (var property in modelBuilder.Model.GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(p => p.Name == "CreatedOn"))
            {
                property.SetDefaultValueSql("getDate()");
            }

            //   modelBuilder.SetQueryFilter<EntityBase>(e => e.IsDeleted == false);

        }

        public static void ApplyCascadeSettings(this ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                entityType.SetTableName(entityType.DisplayName());

                //  if (!entityType.ClrType.GetInterfaces().Contains(typeof(IExcludeFromDeleteRestrict)))
                entityType.GetForeignKeys()
                    .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade)
                    .ToList()
                    .ForEach(fk => fk.DeleteBehavior = DeleteBehavior.Restrict);
            }
        }

        public static void ApplyConfiguration(this ModelBuilder modelBuilder,Assembly assembly)
        {
            var typeConfigurations =assembly.GetTypes().Where(type =>
      (type.BaseType?.IsGenericType ?? false)
      && (type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>)));

            foreach (var typeConfiguration in typeConfigurations)
            {
                var configuration = (IMappingConfiguration)Activator.CreateInstance(typeConfiguration);

                configuration.ApplyConfiguration(modelBuilder);
            }
        }
    }

   
}
