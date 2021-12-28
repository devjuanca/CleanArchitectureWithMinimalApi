using Application.Interfaces;
using Infrastructure.Persistence;
namespace Presentation;
public static class InitialConfigurations
{
    public static void ApplySeeder(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var services = scope.ServiceProvider;

        var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();

        var currentUserService = scope.ServiceProvider.GetRequiredService<ICurrentUserService>();

        var context = scope.ServiceProvider.GetRequiredService<IApplicationDbContext>();

        try
        {
            ApplicationDbContextSeed.RunSeeders(context);
        }
        catch (Exception ex)
        {
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

            logger.LogError(ex, "An error occurred while migrating or seeding the database");

            throw;
        }
    }
}

