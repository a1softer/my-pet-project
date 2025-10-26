var builder = WebApplication.CreateBuilder(args);

// Тестируем чтение конфигурации без Infrastructure зависимостей
var postgresConfig = builder.Configuration.GetSection("PostgreSqlConnectionOptions");

if (postgresConfig.Exists())
{
    Console.WriteLine("PostgreSQL Configuration Found:");
    Console.WriteLine($"Host: {postgresConfig["HostName"]}");
    Console.WriteLine($"Database: {postgresConfig["DatabaseName"]}");
    Console.WriteLine($"Username: {postgresConfig["UserName"]}");
    Console.WriteLine($"Password: {postgresConfig["Password"]}");
}
else
{
    Console.WriteLine("PostgreSQL configuration section not found");
}

// Тестируем TestConfiguration
var testConfig = builder.Configuration.GetSection("TestConfiguration");
if (testConfig.Exists())
{
    Console.WriteLine($"App: {testConfig["ApplicationName"]}");
    Console.WriteLine($"Env: {testConfig["Environment"]}");
    Console.WriteLine($"Retries: {testConfig["MaxRetryCount"]}");
}

builder.Services.AddControllers();

var app = builder.Build();

app.UseRouting();
app.MapControllers();

app.MapGet("/", () => "Construction Equipment Rentals API with Configuration - Working!");

app.Run();
