using Application.Interfaces;
using Application.Mappings;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Presentation;
using System.IO;
using System.Threading.Tasks;

namespace IntegrationTests;

public class Testing
{
    //private static IConfigurationRoot _configuration;
    private static IServiceScopeFactory _scopeFactory;

    [SetUp]
    public static void RunBeforeAnyTest()
    {
        var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Development.json", true, true)
                .AddEnvironmentVariables();
        /*_configuration = */
        builder.Build();

        var webBuilder = WebApplication.CreateBuilder(new WebApplicationOptions { EnvironmentName = "Testing" });

        webBuilder.AddServices();

        var services = webBuilder.Services;

        services.AddLogging();

        services.AddSingleton(Mock.Of<IWebHostEnvironment>(w =>
               w.EnvironmentName == "Development" &&
               w.ApplicationName == "Tech.CleanArchitecture.Presentation"));

        services.AddTransient(provider => Mock.Of<ILogger>());
        services.AddAutoMapper(typeof(MappingProfile));


        _scopeFactory = services.BuildServiceProvider().GetService<IServiceScopeFactory>();

        EnsureDatabase();
    }

    [OneTimeTearDown]
    public static async Task RunAfterAnyTests()
    {
        await ResetState();
    }

    private static void EnsureDatabase()
    {
        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetService<IApplicationDbContext>();

        context?.Database.EnsureCreated();

    }

    public static async Task ResetState()
    {
        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetService<IApplicationDbContext>();

        //context.Database.EnsureDeleted();

        //await context.SaveChangesAsync();

        await Task.CompletedTask;
    }

    public static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
    {
        using var scope = _scopeFactory.CreateScope();

        var mediator = scope.ServiceProvider.GetService<ISender>();

        return await mediator.Send(request);
    }

}
