using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ERP.Models
{
    public class PlotSize
    {
        public int PlotSizeId { get; set; }
        [Required(ErrorMessage = "Please Enter Plot Size")]
        [DisplayName("Plot Size")]
        public string? Plot_Size { get; set; }
    }
}
