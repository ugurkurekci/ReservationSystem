namespace Domain.Events;

public class DeviceStatusChangedEvent
{
    public int DeviceId { get; set; }
    public bool IsReservationStatus { get; set; }
    public DeviceStatusChangedEvent(int deviceId, bool status)
    {
        DeviceId = deviceId;
        IsReservationStatus = status;
    }
}