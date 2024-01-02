using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ERP.Models
{
    public class PlotType
    {
        public int PlotTypeId { get; set; }
        [Required(ErrorMessage = "Please Enter Plot Type")]
        [DisplayName("Plot Type")]
        public string? PlotTypeName { get; set; }

    }
}
