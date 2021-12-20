using NSwag;
using NSwag.AspNetCore;
using NSwag.Generation.Processors.Security;
using Presentation.EndpointsRegistration;

namespace Presentation.EndpointsDefinition;

public class SwaggerEndpointDefinition : IEndpointDefinition
{
    public void DefineEndpoints(WebApplication app)
    {
        var configuration = app.Configuration;

        app.UseOpenApi();
        app.UseSwaggerUi3();
    }

    public void DefineServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddEndpointsApiExplorer();

        services.AddSwaggerDocument();
    }
}

