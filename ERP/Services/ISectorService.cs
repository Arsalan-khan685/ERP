using ERP.Models;

namespace ERP.Services
{
    public interface ISectorService
    {
        List<Sector> GetSectors();
        int AddSector(Sector sector);
        Sector GetSectorByID(int id);
        void UpdateSector(Sector sector);
        
        void DeleteSector(int Id);

    }
}
