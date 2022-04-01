using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Smart.FA.Catalog.Core.Domain;
using Smart.FA.Catalog.Core.Domain.User.Enumerations;
using Smart.FA.Catalog.Core.Domain.ValueObjects;
using Smart.FA.Catalog.Infrastructure.Persistence;

namespace Smart.FA.Catalog.Web.Extensions.Middlewares;

public static class EntityFrameworkMigrationExtensions
{
    //TODO: Find a better place to apply migrations to decouple Infra logic entirely
    /// <summary>
    /// Apply Migrations in the Infrastructure when the data server is up and running.
    /// </summary>
    /// <param name="builder"></param>
    public static void ApplyMigrations(this WebApplicationBuilder builder)
    {
        ServiceProvider? services = builder.Services.BuildServiceProvider();
        for (int i = 0; i < 10; i++)
        {
            try
            {
                using var connection = new SqlConnection(builder.Configuration.GetConnectionString("Catalog"));
                using (var scope = services.CreateScope())
                {
                    var catalogContext = scope.ServiceProvider.GetRequiredService<CatalogContext>();
                    Console.WriteLine($"trying to connect to {connection.DataSource}...");
                    catalogContext.Database.Migrate();
                    Seed(catalogContext);
                }

                break;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            Thread.Sleep(20000);
        }
    }

    public static void Seed(CatalogContext catalogContext)
    {
        var trainer = new Trainer(Name.Create("Victor", "vD").Value,
            TrainerIdentity.Create("1", ApplicationType.Default).Value,"Developer", "Hello I am Victor van Duynen",
            Language.Create("FR").Value);

        var trainer2 = new Trainer(Name.Create("Maxime", "Poulain").Value,
            TrainerIdentity.Create("2", ApplicationType.Default).Value,"Developer", "Hello I am Maxime Poulain",
            Language.Create("FR").Value);
        catalogContext.Trainers.Add(trainer);
        catalogContext.Trainers.Add(trainer2);

        catalogContext.SaveChanges();
    }
}
