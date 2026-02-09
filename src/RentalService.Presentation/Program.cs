using ConstructionEquipmentRentals.Infrastructure.Common;
using ConstructionEquipmentRentals.Infrastructure.Common.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Options;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder
    .Services.AddOptions<PostgresConnectionOptions>()
    .BindConfiguration(nameof(PostgresConnectionOptions));

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddControllers(); // fix #1 не было контроллеров, из-за этого был краш проги при app.MapControllers();

builder.Services.AddScoped<ApplicationDbContext>();

builder.Services.AddSwaggerGen(c =>
{
    string xmlFile = "RentalService.Presentation.xml";
    string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
    c.EnableAnnotations();
});

WebApplication app = builder.Build();

await using AsyncServiceScope scope = app.Services.CreateAsyncScope();

using ApplicationDbContext dbContext =
    scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

try
{
    await dbContext.Database.MigrateAsync();
    Console.WriteLine("migrations applied");
}
catch (Exception ex)
{
    Console.WriteLine($"Migrations applied {ex.Message}");
}

app.MapControllers();

app.UseSwagger();

app.UseSwaggerUI();

app.Run();
