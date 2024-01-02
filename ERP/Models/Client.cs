using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Models
{
    public class Client
    {
        public int ClientID { get; set; }
        [Required(ErrorMessage = "Please Enter Name")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Please Enter Father/Husband Name")]
        public string? GuardianName { get; set; }
        [Required(ErrorMessage = "Please Enter CNIC")]
        public string? CNIC { get; set; }
        [Required(ErrorMessage = "Please Enter PassportNo")]
        public string? PassportNo { get; set; }
        [Required(ErrorMessage = "Please Enter Mailing Address")]
        public string? MailingAddress { get; set; }
        [Required(ErrorMessage = "Please Enter Permanent Address")]
        public string? PermanentAddress { get; set; }
        [Required(ErrorMessage = "Please Enter Phone No")]
        public string? PhoneNo { get; set; }
        [Required(ErrorMessage = "Please Enter Email")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Please Select Plot")]
        public int Plot_ID { get; set; }
        [ForeignKey("Plot_ID")]
        public Plot? plot { get; set; }
    }
}
