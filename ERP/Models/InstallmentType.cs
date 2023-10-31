using System.ComponentModel.DataAnnotations;

namespace ERP.Models
{
    public class InstallmentType
    {
        public int InstallmentTypeId { get; set; }
        [Required]
        public string Installment_Type { get; set; }
    }
}
