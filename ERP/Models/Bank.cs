using System.ComponentModel.DataAnnotations;

namespace ERP.Models
{
    public class Bank
    {
        public int BankId { get; set; }
        [Required]
        public string BankName { get; set; }

    }
}
