namespace Domain.Models;
public class Reservation
{

    public int Id { get; set; }
    public int UserId { get; set; }
    public int DeviceId { get; set; }
    public DateTime ReservationDate { get; set; }
    public string Status { get; set; } = "Pending"; 

}