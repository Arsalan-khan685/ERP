using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ERP.Models
{
    public class LoginModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [MinLength(5)]
        [MaxLength(50)]
        [PasswordPropertyText]
        public string Password { get; set; }
    }
}
