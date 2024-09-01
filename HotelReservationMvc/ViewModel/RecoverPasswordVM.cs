using HotelReservationMvc.Models;
using System.ComponentModel.DataAnnotations;

namespace HotelReservationMvc.ViewModel
{
    public class RecoverPasswordVM
    {
        public string NewPassword { get; set; }
        [Compare("NewPassword", ErrorMessage = "Şifreler uyuşmuyor")]
        public string ConfirmPassword { get; set; }
        public string Userid { get; set; }
        public AppUser? User { get; set; }

        public string Token { get; set; }
    }
}
