using Domain.Events;
using Infrastructure.Messaging;

namespace Application.Sagas;

public class ReservationSagaOrchestrator
{

    private readonly IEventBus _eventBus;

    public ReservationSagaOrchestrator(IEventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public async Task StartSagaAsync(ReservationCreatedEvent reservationEvent)
    {
        try
        {

            // 1. Cihazı kilitle
            var deviceStatusEvent = new DeviceStatusChangedEvent(reservationEvent.DeviceId, true);
            await _eventBus.PublishQueueAsync(deviceStatusEvent, "reservation_events_direct", "reservation.device");

            // 2. Kullanıcıya bildirim gönder
            var pushNotificationEvent = new PushNotificationEvent(reservationEvent.UserId, "Rezervasyon işlemi başlatıldı.");
            await _eventBus.PublishQueueAsync(pushNotificationEvent, "reservation_events_direct", "reservation.notification");
        }
        catch (Exception ex)
        {
            // Hata olursa rollback event yayınla
            var rollbackEvent = new ReservationFailedEvent(reservationEvent.ReservationId, "Rezervasyon işlemi başarısız oldu.");
            await _eventBus.PublishQueueAsync(rollbackEvent, "reservation_events_direct", "reservation.rollback");
        }
    }
}
