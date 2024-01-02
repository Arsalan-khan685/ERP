using MessagePack;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Models
{
    public class Street
    {        
        public int StreetId { get; set; }
        [Required(ErrorMessage = "Please Enter Street")]
        [DisplayName("Street Name")]
        public string? StreetName { get; set; }
        [DisplayName("Block")]
        [Required(ErrorMessage = "Select Block Please")]
        public int Block_Id { get; set; }
        [ForeignKey("Block_Id")]
        public Block? Blocks { get; set; }
    }
}
