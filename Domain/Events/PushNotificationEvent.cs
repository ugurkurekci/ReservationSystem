namespace Domain.Events;

public class PushNotificationEvent
{
    public int UserId { get; set; }
    public string Message { get; set; }
    public PushNotificationEvent(int userId, string message)
    {
        UserId = userId;
        Message = message;
    }
}