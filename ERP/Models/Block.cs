using MessagePack;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Models
{
    public class Block
    {        
        public int BlockId { get; set; }
        [Required(ErrorMessage ="Please Enter Block")]
        public string? BlockName { get; set; }
        [DisplayName("Sector")]
        [Required(ErrorMessage ="Select Sector Please")]
        public int Sector_Id { get; set; }
        [ForeignKey("Sector_Id")]
        public Sector? Sectors { get; set; }
    }
}
