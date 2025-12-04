using Domain.Клиент.Contracts;

namespace Domain.Оборудование.Contracts
{
    public interface ICanStoreEquipment
    {
        Task<Domain.Equipment.Equipment?> Получитьоборудование(GetClientOptions параметр, CancellationToken ct = default);
    }
    public record GetEquipmentOptions(Guid? Id = null, String? name = null);
}
