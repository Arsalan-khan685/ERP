using ERP.Models;
using ERP.Models.ViewModels;

namespace ERP.Services
{
    public interface IPlotService
    {
       List<PlotViewModel> GetPlots();
       List<Sector> GetSectors();
       List<Block> GetBlocks();
       List<Street> GetStreets();
       List<PlotType> GetPlotTypes();
       List<PlotSize> GetPlotSizes();

        List<Block> GetBlockWithSector(int id);
        List<Street> GetStreetsWithBlock(int id);
        void AddPlot(PlotViewModel plot);

        string GetSectorName(int id);
        string GetBlockName(int id);
        Block GetBlockNameandId(int id);
        PlotViewModel GetPlotById(int id);
        void UpdatePlot(Plot plot);
        void DeletePlot(int id);
    }
}
