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

        public async Task<EquipmentResult> GetEquipmentAsync(Guid equipmentId)
        {
            var equipment = await _equipmentStore.Получитьоборудование(
                new GetEquipmentOptions(Id: equipmentId)
            );

            if (equipment == null)
                return EquipmentResult.Failure("Оборудование не найдено");

            return EquipmentResult.Success(
                equipment.Id.Id,
                equipment.Model.Value,
                equipment.Type.Name,
                equipment.RentalCostPerHour.Value,
                equipment.WearPrecentage.Procent,
                equipment.IsActive,
                "Оборудование найдено"
            );
        }

        public async Task<EquipmentListResult> GetAllEquipmentAsync()
        {
            return EquipmentListResult.Failure("Метод GetAllEquipmentAsync пока не реализован");
        }
    }

    public record EquipmentResult
    {
        public bool IsSuccess { get; init; }
        public Guid? EquipmentId { get; init; }
        public string? Model { get; init; }
        public string? Type { get; init; }
        public decimal? RentalCost { get; init; }
        public double? WearPercentage { get; init; }
        public bool? IsActive { get; init; }
        public string? Message { get; init; }
        public string? Error { get; init; }

        public static EquipmentResult Success(
            Guid equipmentId, string model, string type,
            decimal rentalCost, double wearPercentage, bool isActive,
            string message) =>
            new()
            {
                IsSuccess = true,
                EquipmentId = equipmentId,
                Model = model,
                Type = type,
                RentalCost = rentalCost,
                WearPercentage = wearPercentage,
                IsActive = isActive,
                Message = message
            };

        public static EquipmentResult Failure(string error) =>
            new()
            {
                IsSuccess = false,
                Error = error
            };
    }

    public record EquipmentListResult
    {
        public bool IsSuccess { get; init; }
        public List<EquipmentResult>? Equipment { get; init; }
        public string? Error { get; init; }

        public static EquipmentListResult Success(List<EquipmentResult> equipment) =>
            new() { IsSuccess = true, Equipment = equipment };

        public static EquipmentListResult Failure(string error) =>
            new() { IsSuccess = false, Error = error };
    }
}
