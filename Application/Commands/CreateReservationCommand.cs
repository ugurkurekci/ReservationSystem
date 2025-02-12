namespace Application.Commands;

public class CreateReservationCommand
{
    public int UserId { get; set; }
    public int DeviceId { get; set; }

    public CreateReservationCommand(int userId, int deviceId)
    {
        UserId = userId;
        DeviceId = deviceId;
    }
}