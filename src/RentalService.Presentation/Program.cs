using ConstructionEquipmentRentals.Infrastructure.Common;
using ConstructionEquipmentRentals.Infrastructure.Common.Configurations;
using Microsoft.Extensions.Options;

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

IOptions<PostgresConnectionOptions> opts = scope.ServiceProvider.GetRequiredService<
    IOptions<PostgresConnectionOptions>
>();

await using ApplicationDbContext dbContext =
    scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

app.MapControllers();

app.UseSwagger();

app.UseSwaggerUI();

app.Run();
