using System.ComponentModel.DataAnnotations;

namespace HotelReservationMvc.ViewModel
{
    public class LoginViewModel
    {
        [Display(Name = "Email Address")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
