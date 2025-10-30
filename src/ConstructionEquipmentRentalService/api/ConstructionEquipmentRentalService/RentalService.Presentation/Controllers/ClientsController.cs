using Domain.Клиент;
using Microsoft.AspNetCore.Mvc;
using RentalService.Common;

namespace RentalService.Presentation.Controllers
{
    [ApiController]
    [Route("api/clients")]
    public class ClientsController : ControllerBase
    {
        private static readonly List<Клиент> _clientStorage = new();

        [HttpPost]
        public IActionResult CreateClient([FromBody] CreateClientRequest request)
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

                return Ok(Envelope<ClientResponse>.Ok(response));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(Envelope<ClientResponse>.Error(ex.Message));
            }
            catch (Exception)
            {
                return StatusCode(500, Envelope<ClientResponse>.Error("Внутренняя ошибка сервера"));
            }
        }

        [HttpGet("{id:guid}")]
        public IActionResult GetById(Guid id)
        {
            try
            {
                var client = _clientStorage.FirstOrDefault(c => c.Id.Id == id);
                if (client is null)
                    return NotFound(Envelope<ClientResponse>.Error("Клиент не найден"));

                var response = new ClientResponse(
                    client.Id.Id,
                    client.ФИО.Значние,
                    client.Адрес.Значение,
                    client.Телефон.Номер
                );

                return Ok(Envelope<ClientResponse>.Ok(response));
            }
            catch (Exception)
            {
                return StatusCode(500, Envelope<ClientResponse>.Error("Внутренняя ошибка сервера"));
            }
        }
    }

    public record CreateClientRequest(
        Guid ClientId,
        string FullName,
        string Address,
        string Phone
    );

    public record ClientResponse(
        Guid Id,
        string FullName,
        string Address,
        string Phone
    );
}
