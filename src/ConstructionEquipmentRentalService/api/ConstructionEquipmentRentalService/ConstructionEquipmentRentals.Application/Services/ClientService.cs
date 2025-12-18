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
        public async Task<ClientDto> GetClientByIdAsync(Guid clientId)
        {
            var options = new GetClientOptions(Id: clientId);
            var client = await _clientStore.Получитьклиента(options); // ← добавлен await

            if (client is null)
                throw new ArgumentException($"Клиент с ID {clientId} не найден");

            return ClientDto.FromDomain(client);
        }
        public ClientDto CreateClient(CreateClientRequest request)
        {
            try
            {
                var id = Ид_клиента.Create(request.ClientId);
                var фио = ФИО_клиента.Create(request.FullName);
                var адрес = Адрес_клиента.Create(request.Address);
                var телефон = Контактный_телефон.Create(request.Phone);
                var почта = Почта.Create(request.Email);

                var client = new Клиент(id, фио, почта, телефон, адрес);

                return ClientDto.FromDomain(client);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException($"Ошибка создания клиента: {ex.Message}", ex);
            }
        }
    }
    public record ClientDto(
        Guid Id,
        string FullName,
        string Email,
        string Phone,
        string Address
    )
    {
        public static ClientDto FromDomain(Клиент client)
        {
            return new ClientDto(
                Id: client.Id.Id,
                FullName: client.ФИО.Значние,
                Email: client.Email.Email,
                Phone: client.Телефон.Номер,
                Address: client.Адрес.Значение
            );
        }
    }
    public record CreateClientRequest(
        Guid ClientId,
        string FullName,
        string Address,
        string Phone,
        string Email
    );
}
