using Domain.Клиент;
using Microsoft.AspNetCore.Mvc;
using RentalService.Common;

namespace RentalService.Presentation.Controllers
{
    /// <summary>
    /// Контроллер для управления клиентами
    /// </summary>
    [ApiController]
    [Route("api/clients")]
    public class ClientsController : ControllerBase
    {
        private static readonly List<Клиент> _clientStorage = new();

        /// <summary>
        /// Создает нового клиента
        /// </summary>
        /// <param name="request">Данные для создания клиента</param>
        /// <returns>Созданный клиент</returns>
        [HttpPost]
        public IResult CreateClient([FromBody] CreateClientRequest request)
        {
            try
            {
                var id = Ид_клиента.Create(request.ClientId);
                var фио = ФИО_клиента.Create(request.FullName);
                var адрес = Адрес_клиента.Create(request.Address);
                var телефон = Контактный_телефон.Create(request.Phone);

                var client = new Клиент(id, фио, null!, телефон, адрес);

                _clientStorage.Add(client);

                var response = new ClientResponse(
                    client.Id.Id,
                    client.ФИО.Значние,
                    client.Адрес.Значение,
                    client.Телефон.Номер
                );

                return Envelope<ClientResponse>.Ok(response);
            }
            catch (ArgumentException ex)
            {
                return Envelope<ClientResponse>.BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return Envelope<ClientResponse>.InternalError("Внутренняя ошибка сервера");
            }
        }

        /// <summary>
        /// Получает клиента по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор клиента</param>
        /// <returns>Клиент</returns>
        [HttpGet("{id:guid}")]
        public IResult GetById(Guid id)
        {
            try
            {
                var client = _clientStorage.FirstOrDefault(c => c.Id.Id == id);
                if (client is null)
                    return Envelope<ClientResponse>.NotFound("Клиент не найден");

                var response = new ClientResponse(
                    client.Id.Id,
                    client.ФИО.Значние,
                    client.Адрес.Значение,
                    client.Телефон.Номер
                );

                return Envelope<ClientResponse>.Ok(response);
            }
            catch (Exception)
            {
                return Envelope<ClientResponse>.InternalError("Внутренняя ошибка сервера");
            }
        }
    }

    /// <summary>
    /// Запрос на создание клиента
    /// </summary>
    /// <param name="ClientId">Идентификатор клиента</param>
    /// <param name="FullName">ФИО клиента</param>
    /// <param name="Address">Адрес клиента</param>
    /// <param name="Phone">Контактный телефон</param>
    public record CreateClientRequest(
        Guid ClientId,
        string FullName,
        string Address,
        string Phone
    );

    /// <summary>
    /// Ответ с информацией о клиенте
    /// </summary>
    /// <param name="Id">Идентификатор</param>
    /// <param name="FullName">ФИО</param>
    /// <param name="Address">Адрес</param>
    /// <param name="Phone">Телефон</param>
    public record ClientResponse(
        Guid Id,
        string FullName,
        string Address,
        string Phone
    );
}
