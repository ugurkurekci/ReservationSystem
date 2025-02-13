namespace Domain.Events;

public class ReservationFailedEvent
{
    public int ReservationId { get; }

    public string Detail { get; }
    public string Reason { get; }

    public ReservationFailedEvent(int reservationId, string reason,string detail)
    {
        ReservationId = reservationId;
        Reason = reason;
        Detail = detail;
    }
}
