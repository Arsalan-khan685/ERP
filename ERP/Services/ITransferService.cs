using ERP.Models;
using ERP.Models.ViewModels;

namespace ERP.Services
{
    public interface ITransferService
    {
        List<Block> GetBlockWithSector(int sectorId);
        List<Street> GetStreetsWithBlock(int blockId);
        List<Plot> GetPlotsWithStreet(int streetId);
        List<dynamic> GetPlotDetails(int plotId);
        void AddTransferInformation(TransferViewModel model);

    }
}
