namespace Domain.Events;

public class ReservationFailedEvent
{
    public int ReservationId { get; }
    public string Reason { get; }

    public ReservationFailedEvent(int reservationId, string reason)
    {
        ReservationId = reservationId;
        Reason = reason;
    }
}
