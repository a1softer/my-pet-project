using Domain.Клиент.Contracts;

namespace Domain.Оборудование.Contracts
{
    public interface ICanStoreEquipment
    {
        Task<Domain.Equipment.Equipment?> Получитьоборудование(GetEquipmentOptions параметр, CancellationToken ct = default);
        Task<Equipment.Equipment> Получитьоборудование(GetClientOptions options);
    }
    public record GetEquipmentOptions(Guid? Id = null, String? name = null);
}
