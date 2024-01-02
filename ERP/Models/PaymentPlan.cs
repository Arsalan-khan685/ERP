using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Models
{
    public class PaymentPlan
    {
        public int PaymentPlanID { get; set; }
        [Required(ErrorMessage = "Please Enter Installment Duration")]
        public string? InstallmentDuration { get; set; }
        [Required(ErrorMessage = "Please Enter Total Price")]
        [DisplayName("Total Price")]
        public decimal TotalPrice { get; set; }
        [DisplayName("Down Payment")]
        public decimal DownPayment { get; set; }
        [DisplayName("On Confirmation")]
        public int OnConfirmation { get; set; }
        [DisplayName("Allocation Fee")]
        public int AllocationFee { get; set; }
        [DisplayName("Form Fee")]
        public int FormFee { get; set; }
        public string? Description { get; set; }
        [Required(ErrorMessage = "Please Enter Balance")]
        public decimal Balance { get; set; }
        [Required]
        public int InstallmentType_ID { get; set; }
        [DisplayName("One Installment Amount")]
        public decimal OneInstallmentAmount { get; set; }
        [DisplayName("No Of Installment")]
        public int NoOfInstallment { get; set; }
        [DisplayName("Total Installment Amount")]
        public decimal TotalInstallmentAmount { get; set; }
        [Required]
        public int PlotType_ID { get; set; }
        public int PlotSize_ID { get; set; }
        [ForeignKey("InstallmentType_ID")]
        public InstallmentType? InstallmentType { get; set; }
        [ForeignKey("PlotType_ID")]
        public PlotType? PlotType { get; set; }
        [ForeignKey("PlotSize_ID")]
        public PlotSize? PlotSize { get; set; }
    }
}
