using Presentation;
using Presentation.EndpointsRegistration;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.AddServices();

builder.Services.AddEndpointDefinitions(configuration);

var app = builder.Build();


// Configure the HTTP request pipeline.
app.ConfigurePipeline();

app.UseEndpointDefinitions();

app.ApplySeeder();

await app.RunAsync();

