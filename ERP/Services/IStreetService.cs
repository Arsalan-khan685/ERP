using ERP.Models;

namespace ERP.Services
{
    public interface IStreetService
    {
        List<Block> GetBlocks();
        List<Sector> GetSectors();
        List<Street> GetStreetWithBlock();
        List<Block> GetBlockWithSector(int sectorId);
        List<Street> GetStreetWithBlockAndSectors();
        Sector GetSector(int id);
        int AddStreet(Street street);
        Street GetStreetByID(int id);
        void UpdateStreet(Street street);
        void DeleteStreet(int Id);
    }
}
