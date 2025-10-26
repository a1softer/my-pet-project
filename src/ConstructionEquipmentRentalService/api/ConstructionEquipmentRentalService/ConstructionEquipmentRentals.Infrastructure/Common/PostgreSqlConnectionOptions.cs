namespace ConstructionEquipmentRentals.Infrastructure.Common
{
    public sealed class PostgreSqlConnectionOptions
    {
        public required string HostName { get; init; }
        public required string DatabaseName { get; init; }
        public required string UserName { get; init; }
        public required string Password { get; init; }

        public string BuildConnectionString()
        {
            throw new NotImplementedException();
        }
    }
}
