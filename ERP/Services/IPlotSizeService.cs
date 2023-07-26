using ERP.Models;

namespace ERP.Services
{
    public interface IPlotSizeService
    {
        List<PlotSize> GetPlotSizes();
        int AddPlotSize(PlotSize plotSize);
        PlotSize GetPlotSizeByID(int id);
        void UpdatePlotSize(PlotSize plotSize);
        
        void DeletePlotSize(int Id);

    }
}
