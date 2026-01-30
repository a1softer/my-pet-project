using ConstructionEquipmentRentals.Infrastructure.Common;
using ConstructionEquipmentRentals.Infrastructure.Common.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json");

var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
var section = config.GetSection("PostgresConnectionOptions");

builder.Services.AddControllers();
builder.Services.AddOptions<PostgresConnectionOptions>(nameof(PostgresConnectionOptions)).BindConfiguration(nameof(PostgresConnectionOptions));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<ApplicationDbContext>();

builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = "RentalService.Presentation.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
    c.EnableAnnotations();
});

var app = builder.Build();



await using var scope = app.Services.CreateAsyncScope();



var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI();

app.Run();