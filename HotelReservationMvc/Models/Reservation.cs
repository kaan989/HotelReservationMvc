namespace HotelReservationMvc.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int RoomId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal TotalPrice { get; set; }

        // Bağlantılar
        public Customer Customer { get; set; }
        public Room Room { get; set; }
        public ICollection<ReservationService> ReservationServices { get; set; }
    }
}
