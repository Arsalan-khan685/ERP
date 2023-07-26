using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ERP.Models.ViewModels
{
    public class PlotViewModel
    {
        public int PlotId { get; set; }
        public string PlotNo { get; set; }
        public int PlotTypeId { get; set; }
        public string PlotTypeName { get; set; }
        public int PlotSizeId { get; set; }
        public string Plot_Size { get; set; }
        public int StreetId { get; set; }
        public string StreetName { get; set; }
        public int BlockId { get; set; }
        public string BlockName { get; set; }
        public int SectorId { get; set; }
        public string SectorName { get; set; }

    }
}
