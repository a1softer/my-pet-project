namespace ConstructionEquipmentRentals.Infrastructure.Common.Configurations
{
    public sealed class PostgresConnectionOptions
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Port { get; set; } = string.Empty;
        public string Database {  get; set; } = string.Empty;

        public string AsConnectionString()
        {
            string template = "Host=localhost;Port={0};Username={1};Password={2};Database={3}";
            return string.Format(template, Port, UserName, Password, Database);
        }
    }
}