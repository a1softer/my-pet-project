using Domain.Оборудование.Contracts;
using Domain.Equipment;

namespace RentalService.Application.Services
{
    public class EquipmentService
    {
        private readonly ICanStoreEquipment _equipmentStore;

        public EquipmentService(ICanStoreEquipment equipmentStore)
        {
            _equipmentStore = equipmentStore;
        }

        public async Task<EquipmentDto> GetEquipmentByIdAsync(Guid equipmentId)
        {
            var options = new Domain.Клиент.Contracts.GetClientOptions(Id: equipmentId);
            var equipment = await _equipmentStore.Получитьоборудование(options);

            if (equipment is null)
                throw new ArgumentException($"Оборудование с ID {equipmentId} не найдено");

            return EquipmentDto.FromDomain(equipment);
        }

        public async Task<List<EquipmentDto>> GetAllEquipmentAsync()
        {
            // Создаем Task, чтобы был await
            return await Task.FromResult(new List<EquipmentDto>()); 
        }
    }
    public record EquipmentDto(
        Guid Id,
        decimal RentalCost,
        DateOnly LastMaintenanceDate,
        string EquipmentType,
        string Model,
        double WearPercentage,
        bool IsActive
    )
    {
        public static EquipmentDto FromDomain(Equipment equipment)
        {
            return new EquipmentDto(
                Id: equipment.Id.Id,
                RentalCost: equipment.RentalCostPerHour.Value,
                LastMaintenanceDate: equipment.LastMaintenanceDate.Date,
                EquipmentType: equipment.Type.Name,
                Model: equipment.Model.Value,
                WearPercentage: equipment.WearPrecentage.Procent,
                IsActive: equipment.IsActive
            );
        }
    }
}
