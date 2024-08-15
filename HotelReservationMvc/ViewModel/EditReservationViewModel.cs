using HotelReservationMvc.Models;
using System.ComponentModel.DataAnnotations;

namespace HotelReservationMvc.ViewModel
{
    public class EditReservationViewModel
    {
        public int CustomerId { get; set; }  // Seçilen müşterinin ID'si
        public int RoomId { get; set; }       // Seçilen odanın ID'si

        [Required(ErrorMessage = "Check-In Date is required.")]
        [DataType(DataType.Date)]
        public DateTime CheckInDate { get; set; }  // Giriş tarihi

        [Required(ErrorMessage = "Check-Out Date is required.")]
        [DataType(DataType.Date)]
        public DateTime CheckOutDate { get; set; }  // Çıkış tarihi

        [Range(0, double.MaxValue, ErrorMessage = "Total Price must be a positive value.")]
        public decimal TotalPrice { get; set; }  // Toplam fiyat
    }
}
