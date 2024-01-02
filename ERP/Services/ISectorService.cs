using ERP.Models;

namespace ERP.Services
{
    public interface ISectorService
    {
        List<Sector> GetSectors();
        string AddSector(Sector sector);
        Sector GetSectorByID(int id);
        string UpdateSector(Sector sector);
        
        void DeleteSector(int Id);

    }
}
