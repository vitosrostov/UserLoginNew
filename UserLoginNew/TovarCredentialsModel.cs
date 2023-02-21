using System.ComponentModel.DataAnnotations;

namespace UserLoginNew
{
    public class TovarCredentialsModel
    {
        [Required(ErrorMessage = "tovar is required")]
        public int TovarNum { get; set; }
    }
}
