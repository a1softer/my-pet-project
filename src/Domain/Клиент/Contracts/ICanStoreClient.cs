using System.Security.AccessControl;

namespace Domain.Клиент.Contracts
{
    public interface ICanStoreClient
    {
        Task<Клиент?> Получитьклиента (GetClientOptions параметр, CancellationToken ct = default);
    }
    public record GetClientOptions(Guid? Id = null, String? name = null);
}
