using System.ComponentModel.DataAnnotations;

namespace UserLoginNew
{
    public class UserCredentialsModel
    {
        [Required(ErrorMessage = "Login is required")]
        [MinLength(3), MaxLength(10)]
        public string Login { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [MinLength(2), MaxLength(20)]
        public string Password { get; set; }
    }
}
