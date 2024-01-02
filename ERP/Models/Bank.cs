using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ERP.Models
{
    public class Bank
    {
        public int BankId { get; set; }
        [Required(ErrorMessage ="Please Enter Bank Name")]
        [DisplayName("Bank Name")]
        public string? BankName { get; set; }

    }
}
