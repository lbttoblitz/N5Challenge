using Microsoft.EntityFrameworkCore;
using Nest;
using MediatR;
using UserPermissions.Api.Domain.Entities;
using UserPermissions.Api.Domain.Interfaces;
using UserPermissions.Api.Infrastructure.Data;
using UserPermissions.Api.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddMediatR(typeof(Program));

var esUrl = builder.Configuration.GetValue<string>("ElasticSearchUrl");
var settings = new ConnectionSettings(new Uri(esUrl))
    .DefaultIndex("permission")
    .DefaultMappingFor<PermissionElastic>(i => i
    .IndexName("permission"));

builder.Services.AddSingleton<IElasticClient>(new ElasticClient(settings));
builder.Services.AddDbContext<PermissionContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionDB"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

await SeedData();

app.UseAuthorization();
app.MapControllers();

app.Run();

async Task SeedData()
{
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var loggerFactory = services.GetService<ILoggerFactory>();
        try
        {
            var context = services.GetRequiredService<PermissionContext>();
            await context.Database.MigrateAsync();
            await PermissionTypesContextSeed.SeedAsync(context);
        }
        catch (Exception ex)
        {
            var logger = loggerFactory?.CreateLogger<Program>();
            logger?.LogError(ex.Message);
        }
    }
}
