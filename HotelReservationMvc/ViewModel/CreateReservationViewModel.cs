using HotelReservationMvc.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace HotelReservationMvc.ViewModel
{
    public class CreateReservationViewModel
    {
        public int RoomId { get; set; }
        public int CustomerId { get; set; }

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
