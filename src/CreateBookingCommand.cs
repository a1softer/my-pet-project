using System;
using Domain.Бронирование.Contracts

public record CreateBookingCommand (Guid EquipmentId, Guid ClientId);

public class CreateBookingCommandHander
{
    private readonly ICanStoreBooking StoreBooking;
    private readonly ICanStoreClient StoreClient;
    private readonly ICanStoreEquipment StoreEquipment
}
