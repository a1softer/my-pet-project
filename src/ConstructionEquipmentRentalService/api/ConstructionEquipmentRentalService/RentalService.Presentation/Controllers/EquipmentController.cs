using Domain.Equipment;
using Microsoft.AspNetCore.Mvc;
using RentalService.Common;

namespace RentalService.Presentation.Controllers
{
    /// <summary>
    /// Контроллер для управления оборудованием
    /// </summary>
    [ApiController]
    [Route("api/equipments")]
    public class EquipmentController : ControllerBase
    {
        private static readonly List<Equipment> _equipmentStorage = new();

        /// <summary>
        /// Создает новое оборудование
        /// </summary>
        /// <param name="request">Данные для создания оборудования</param>
        /// <returns>Созданное оборудование</returns>
        [HttpPost]
        public IResult Create([FromBody] CreateEquipmentRequest request)
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

                return Envelope<EquipmentResponse>.Ok(response);
            }
            catch (ArgumentException ex)
            {
                return Envelope<EquipmentResponse>.Error(ex.Message);
            }
            catch (Exception)
            {
                return Envelope<EquipmentResponse>.Error("Внутренняя ошибка сервера");
            }
        }

        /// <summary>
        /// Получает оборудование по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор оборудования</param>
        /// <returns>Оборудование</returns>
        [HttpGet("{id:guid}")]
        public IResult GetById(Guid id)
        {
            try
            {
                var equipment = _equipmentStorage.FirstOrDefault(e => e.Id.Id == id);
                if (equipment is null)
                    return Envelope<EquipmentResponse>.Error("Оборудование не найдено");

                var response = new EquipmentResponse(
                    equipment.Id.Id,
                    equipment.RentalCostPerHour.Value,
                    equipment.LastMaintenanceDate.Date,
                    equipment.Type.Name,
                    equipment.Model.Value,
                    equipment.WearPrecentage.Procent
                );

                return Envelope<EquipmentResponse>.Ok(response);
            }
            catch (Exception)
            {
                return Envelope<EquipmentResponse>.Error("Внутренняя ошибка сервера");
            }
        }

        /// <summary>
        /// Получает все оборудование
        /// </summary>
        /// <returns>Список всего оборудования</returns>
        [HttpGet]
        public IResult GetAll()
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

                return Envelope<List<EquipmentResponse>>.Ok(responses);
            }
            catch (Exception)
            {
                return Envelope<List<EquipmentResponse>>.Error("Внутренняя ошибка сервера");
            }
        }
    }

    /// <summary>
    /// Запрос на создание оборудования
    /// </summary>
    /// <param name="Price">Стоимость аренды за час</param>
    /// <param name="DateLastTO">Дата последнего ТО</param>
    /// <param name="Type">Тип оборудования</param>
    /// <param name="Wear">Процент износа</param>
    /// <param name="Model">Модель оборудования</param>
    public record CreateEquipmentRequest(
        decimal Price,
        DateOnly DateLastTO,
        string Type,
        double Wear,
        string Model
    );

    /// <summary>
    /// Ответ с информацией об оборудовании
    /// </summary>
    /// <param name="Id">Идентификатор</param>
    /// <param name="RentalCost">Стоимость аренды</param>
    /// <param name="LastMaintenanceDate">Дата последнего ТО</param>
    /// <param name="EquipmentType">Тип оборудования</param>
    /// <param name="Model">Модель</param>
    /// <param name="WearPercentage">Процент износа</param>
    public record EquipmentResponse(
        Guid Id,
        decimal RentalCost,
        DateOnly LastMaintenanceDate,
        string EquipmentType,
        string Model,
        double WearPercentage
    );
}
