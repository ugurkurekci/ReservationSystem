namespace Domain.Events;

public class ReservationCreatedEvent
{
    public int ReservationId { get; set; }
    public int UserId { get; set; }
    public int DeviceId { get; set; }
    public DateTime ReservationDate { get; set; }

    public ReservationCreatedEvent(int reservationId, int userId, int deviceId, DateTime reservationDate)
    {
        ReservationId = reservationId;
        UserId = userId;
        DeviceId = deviceId;
        ReservationDate = reservationDate;
    }
}