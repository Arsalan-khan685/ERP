using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ERP.Models
{
    public class InstallmentType
    {
        public int InstallmentTypeId { get; set; }
        [Required(ErrorMessage ="Please Enter Installment Type")]
        [DisplayName("Installment Type")]
        public string? Installment_Type { get; set; }
    }
}
