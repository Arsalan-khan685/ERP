using ERP.Models;

namespace ERP.Services
{
    public interface IPlotTypeService
    {
        List<PlotType> GetPlotTypes();
        int AddPlotType(PlotType PlotType);
        PlotType GetPlotTypeByID(int id);
        void UpdatePlotType(PlotType PlotType);
        
        void DeletePlotType(int Id);

    }
}
