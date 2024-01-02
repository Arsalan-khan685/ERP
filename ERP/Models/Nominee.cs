using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Models
{
    public class Nominee
    {
        public int NomineeID { get; set; }
        [Required(ErrorMessage = "Please Enter Name")]
        public string? NomineeName { get; set; }
        [Required(ErrorMessage = "Please Enter Father/Husband Name")]
        public string? NomineeGuardian { get; set; }
        [Required(ErrorMessage = "Please Enter CNIC")]
        public string? NomineeCNIC { get; set; }
        public int Relation_ID { get; set; }
        [ForeignKey("Relation_ID")]
        public Relation? relation { get; set; }
    }
}
