namespace HotelReservationMvc.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string RoomNumber { get; set; }
        public int Capacity { get; set; }
        public bool IsAvailable { get; set; }
        public decimal PricePerNight { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

        // Bağlantılar
        public ICollection<Reservation> Reservations { get; set; }
    }
}
