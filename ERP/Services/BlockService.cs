using ERP.Models;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace ERP.Services
{
    public class BlockService : IBlockService
    {
        public string ConString { get; set; }
        public IConfiguration _configuration;
        public SqlConnection conn;

        public BlockService(IConfiguration configuration)
        {
            _configuration = configuration;
            ConString = _configuration.GetConnectionString("DBConnection");
        }
       
        public List<Block> GetBlockWithSector()
        {

            List<Block> blockslist = new List<Block>();
            try
            {
                using (conn = new SqlConnection(ConString))
                {
                    string query = "Select b.blockId,b.blockName,b.Sector_Id,s.SectorName " +
                                    "From Block b LEFT JOIN Sector s on " +
                                        "b.Sector_Id = s.SectorId";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.CommandType = CommandType.Text;
                    conn.Open();

                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read()) 
                    {                       

                        Block block = new Block
                        {
                            BlockId = (int)rdr["BlockId"],
                            BlockName = (string?)rdr["BlockName"],
                            Sector_Id = (int)rdr["Sector_Id"],

                            Sectors = new Sector
                            {
                                SectorId = (int)rdr["Sector_Id"],
                                SectorName = (string)rdr["SectorName"]
                            }
                        };                   

                        blockslist.Add(block);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return blockslist;
        }

        public List<Sector> GetSectors()
        {
            List<Sector> sectorsList = new List<Sector>();
            try
            {
                using (conn = new SqlConnection(ConString))
                {
                    conn.Open();
                    string query = "Select * from Sector";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.CommandType = System.Data.CommandType.Text;

                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        Sector sector = new Sector();
                        sector.SectorId = Convert.ToInt32(rdr["SectorId"]);
                        sector.SectorName = rdr["SectorName"].ToString();

                        sectorsList.Add(sector);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return sectorsList;
        }

        public int AddBlock(Block block)
        {
            int res = 0;
            try
            {
                using (conn = new SqlConnection(ConString))
                {
                    string query = "INSERT INTO BLOCK (BlockName,Sector_Id) VALUES (@BlockName,@Sector_Id)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@BlockName",block.BlockName);
                    cmd.Parameters.AddWithValue("@Sector_Id",block.Sector_Id);
                    conn.Open();
                    res = cmd.ExecuteNonQuery();

                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return res;
        }

        public Block GetBlockByID(int id)
        {
            Block block = new Block();
            try
            {
                using(conn=new SqlConnection(ConString))
                {
                    string query = "SELECT blockId,b.BlockName,b.Sector_Id,s.SectorName From block b LEFT JOIN" +
                                    " Sector s on b.Sector_Id = s.SectorId Where b.blockId = @BlockId";
                    SqlCommand cmd = new SqlCommand(query,conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@BlockId", id);
                    conn.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    if(rdr.Read())
                    {
                        // block.BlockId = (int)rdr["BlockId"];
                        block = new Block
                        {
                            BlockId = (int)rdr["BlockId"],
                            BlockName = (string)rdr["BlockName"],
                            Sector_Id = (int)rdr["Sector_Id"],
                            Sectors = new Sector
                            {
                                SectorId = (int)rdr["Sector_Id"],
                                SectorName = (string)rdr["SectorName"]
                            }
                        };
                    }

                }

            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return block;
        }

        public void UpdateBlock(Block block)
        {
            try
            {
                using(conn= new SqlConnection(ConString))
                {
                    string query = "UPDATE Block SET BlockName = @BlockName,Sector_Id = @SectorId WHERE BlockId = @BlockId";
                    SqlCommand cmd = new SqlCommand(query,conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@BlockName",block.BlockName);
                    cmd.Parameters.AddWithValue("@SectorId", block.Sector_Id);
                    cmd.Parameters.AddWithValue("@BlockId", block.BlockId);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        public void DeleteBlock(int Id)
        {
            try
            {
                using(conn = new SqlConnection(ConString))
                {
                    string query = "DELETE FROM BLOCK WHERE BlockId = @BlockId";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@BlockId", Id);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
