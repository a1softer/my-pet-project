using Domain.Equipment;
using Microsoft.AspNetCore.Mvc;
using RentalService.Common;

namespace RentalService.Presentation.Controllers
{
    [ApiController]
    [Route("api/equipments")]
    public class EquipmentController : ControllerBase
    {
        private static readonly List<Equipment> _equipmentStorage = new();

        [HttpPost]
        public IActionResult Create([FromBody] CreateEquipmentRequest request)
        {
            try
            {
                var id = IDEquipment.CreateNew();
                var rentalCostPerHour = RentalCost.Create(request.Price);
                var model = EquipmentModel.Create(request.Model);
                var type = ТипОборудования.FromName(request.Type);
                var lastMaintenanceDate = LastDateTO.Create(request.DateLastTO);
                var wearPercentage = StateProcent.Create(request.Wear);

                var equipment = new Equipment(id, rentalCostPerHour, model, type, lastMaintenanceDate, wearPercentage);

                _equipmentStorage.Add(equipment);

                var response = new EquipmentResponse(
                    equipment.Id.Id,
                    equipment.RentalCostPerHour.Value,
                    equipment.LastMaintenanceDate.Date,
                    equipment.Type.Name,
                    equipment.Model.Value,
                    equipment.WearPrecentage.Procent
                );

                return Ok(Envelope<EquipmentResponse>.Ok(response));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(Envelope<EquipmentResponse>.Error(ex.Message));
            }
            catch (Exception)
            {
                return StatusCode(500, Envelope<EquipmentResponse>.Error("Внутренняя ошибка сервера"));
            }
        }

        [HttpGet("{id:guid}")]
        public IActionResult GetById(Guid id)
        {
            try
            {
                var equipment = _equipmentStorage.FirstOrDefault(e => e.Id.Id == id);
                if (equipment is null)
                    return NotFound(Envelope<EquipmentResponse>.Error("Оборудование не найдено"));

                var response = new EquipmentResponse(
                    equipment.Id.Id,
                    equipment.RentalCostPerHour.Value,
                    equipment.LastMaintenanceDate.Date,
                    equipment.Type.Name,
                    equipment.Model.Value,
                    equipment.WearPrecentage.Procent
                );

                return Ok(Envelope<EquipmentResponse>.Ok(response));
            }
            catch (Exception)
            {
                return StatusCode(500, Envelope<EquipmentResponse>.Error("Внутренняя ошибка сервера"));
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var responses = _equipmentStorage.Select(e => new EquipmentResponse(
                    e.Id.Id,
                    e.RentalCostPerHour.Value,
                    e.LastMaintenanceDate.Date,
                    e.Type.Name,
                    e.Model.Value,
                    e.WearPrecentage.Procent
                )).ToList();

                return Ok(Envelope<List<EquipmentResponse>>.Ok(responses));
            }
            catch (Exception)
            {
                return StatusCode(500, Envelope<List<EquipmentResponse>>.Error("Внутренняя ошибка сервера"));
            }
        }

        [HttpPatch("{id:guid}/wear")]
        public IActionResult IncreaseWear(Guid id, [FromBody] IncreaseWearRequest request)
        {
            try
            {
                var equipment = _equipmentStorage.FirstOrDefault(e => e.Id.Id == id);
                if (equipment is null)
                    return NotFound(Envelope<EquipmentResponse>.Error("Оборудование не найдено"));

                var response = new EquipmentResponse(
                    equipment.Id.Id,
                    equipment.RentalCostPerHour.Value,
                    equipment.LastMaintenanceDate.Date,
                    equipment.Type.Name,
                    equipment.Model.Value,
                    equipment.WearPrecentage.Procent
                );

                return Ok(Envelope<EquipmentResponse>.Ok(response));
            }
            catch (Exception)
            {
                return StatusCode(500, Envelope<EquipmentResponse>.Error("Внутренняя ошибка сервера"));
            }
        }
    }

    public record CreateEquipmentRequest(
        decimal Price,
        DateOnly DateLastTO,
        string Type,
        double Wear,
        string Model
    );

    public record EquipmentResponse(
        Guid Id,
        decimal RentalCost,
        DateOnly LastMaintenanceDate,
        string EquipmentType,
        string Model,
        double WearPercentage
    );

    public record IncreaseWearRequest(double Amount);
}
