using HotelReservationMvc.Models;

namespace HotelReservationMvc.ViewModel
{
    public class CreateRoomViewModel
    {
        public int Id { get; set; }
        public string RoomNumber { get; set; }
        public int Capacity { get; set; }
        public bool IsAvailable { get; set; }
        public decimal PricePerNight { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }


    }
}
