using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ERP.Models
{
    public class Relation
    {
        public int RelationId { get; set; }
        [Required(ErrorMessage = "Please Enter Relation Name")]
        [DisplayName("Relation Type")]
        public string RelationName { get; set; }
    }
}
