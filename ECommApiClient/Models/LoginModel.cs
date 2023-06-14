using System.ComponentModel.DataAnnotations;

namespace ECommApiClient.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Username Required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password Required")]
        public string Password { get; set; }
    }
}
