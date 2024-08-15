namespace HotelReservationMvc.Models
{
    public class ReservationService
    {

    public int ReservationId { get; set; }
    public Reservation Reservation { get; set; }

    public int ServiceId { get; set; }
    public Service Service { get; set; }

    public int Quantity { get; set; } // Hizmetin kaç kez alındığı
    public decimal TotalPrice { get; set; }
}
}

