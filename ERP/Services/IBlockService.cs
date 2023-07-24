using ERP.Models;

namespace ERP.Services
{
    public interface IBlockService
    {
        List<Sector> GetSectors();
        List<Block> GetBlockWithSector();
        int AddBlock(Block block);
        Block GetBlockByID(int id);
        void UpdateBlock(Block block);

        void DeleteBlock(int Id);
    }
}
