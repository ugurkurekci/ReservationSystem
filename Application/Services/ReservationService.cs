using Application.Commands;
using Application.Validation;
using Application.Validation.Dto;
using Domain.Events;
using Domain.Models;
using Infrastructure.Messaging;

namespace Application.Services;
public class ReservationService
{

    private readonly IEventBus _eventBus;
    private readonly ValidationManager _validationManager;

    public ReservationService(IEventBus eventBus, ValidationManager validationManager)
    {
        _eventBus = eventBus;
        _validationManager = validationManager;
    }

    public async Task<ProjectResult> CreateReservationAsync(CreateReservationCommand command)
    {

        // Validate Request
        if (command == null)
        {
            return new ProjectResult { IsValid = false, Message = "Geçersiz istek" };
        }

        if (command.UserId <= 0 || command.DeviceId <= 0)
        {
            return new ProjectResult { IsValid = false, Message = "Geçersiz kullanıcı veya cihaz bilgisi" };
        }

        // Validate Business Rules
        var validationResult = await _validationManager.ValidateAsync(command);
        if (!validationResult.IsValid)
        {
            return new ProjectResult { IsValid = false, Message = validationResult.Message };
        }

        // Create Reservation

        var reservation = new Reservation
        {
            UserId = command.UserId,
            DeviceId = command.DeviceId,
            ReservationDate = DateTime.UtcNow
        };

        // db save operation


        // Publish Events
        var reservationEvent = new ReservationCreatedEvent(reservation.Id, reservation.UserId, reservation.DeviceId, reservation.ReservationDate);
        var pushNotificationEvent = new PushNotificationEvent(reservation.UserId, "Rezervasyonunuz başarıyla oluşturuldu.");
        var deviceStatusEvent = new DeviceStatusChangedEvent(reservation.DeviceId, true);

        await _eventBus.PublishQueueAsync(pushNotificationEvent, "reservation_events_direct", "reservation.notification");
        await _eventBus.PublishQueueAsync(deviceStatusEvent, "reservation_events_direct", "reservation.device");

        return new ProjectResult { IsValid = true, Message = "Rezervasyon başarıyla oluşturuldu." };
    }
}
