using Domain.Клиент.Contracts;
using Domain.Клиент;

namespace RentalService.Application.Services
{
    public class ClientService
    {
        private readonly ICanStoreClient _clientStore;

        public ClientService(ICanStoreClient clientStore)
        {
            _clientStore = clientStore;
        }

        public async Task<ClientResult> GetClientAsync(Guid clientId)
        {
            var client = await _clientStore.Получитьклиента(
                new GetClientOptions(Id: clientId)
            );

            if (client == null)
                return ClientResult.Failure("Клиент не найден");

            return ClientResult.Success(
                client.Id.Id,
                client.ФИО.Значние,
                client.Email.Email,
                client.Телефон.Номер,
                client.Адрес.Значение,
                "Клиент найден"
            );
        }
    }

    public record ClientResult
    {
        public bool IsSuccess { get; init; }
        public Guid? ClientId { get; init; }
        public string? FullName { get; init; }
        public string? Email { get; init; }
        public string? Phone { get; init; }
        public string? Address { get; init; }
        public string? Message { get; init; }
        public string? Error { get; init; }

        public static ClientResult Success(
            Guid clientId, string fullName, string email,
            string phone, string address, string message) =>
            new()
            {
                IsSuccess = true,
                ClientId = clientId,
                FullName = fullName,
                Email = email,
                Phone = phone,
                Address = address,
                Message = message
            };

        public static ClientResult Failure(string error) =>
            new()
            {
                IsSuccess = false,
                Error = error
            };
    }
}
