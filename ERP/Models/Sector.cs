using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ERP.Models
{
    public class Sector
    {
        public int SectorId { get; set; }
        [Required(ErrorMessage ="Please Enter Sector")]
        public string? SectorName { get; set; }
    }
}
