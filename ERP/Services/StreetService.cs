using ERP.Models;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace ERP.Services
{
    public class StreetService : IStreetService
    {
        public string ConString { get; set; }
        public IConfiguration _configuration;
        public SqlConnection conn;

        public StreetService(IConfiguration configuration)
        {
            _configuration = configuration;
            ConString = _configuration.GetConnectionString("DBConnection");
        }

        public List<Block> GetBlocks()
        {
            List<Block> blocksList = new List<Block>();
            try
            {
                using (conn = new SqlConnection(ConString))
                {
                    conn.Open();
                    string query = "Select * from Block";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.CommandType = System.Data.CommandType.Text;

                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        Block block = new Block();
                        block.BlockId = Convert.ToInt32(rdr["BlockId"]);
                        block.BlockName = rdr["BlockName"].ToString();

                        blocksList.Add(block);
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
            return blocksList;
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

        public List<Street> GetStreetWithBlock()
        {

            List<Street> streetslist = new List<Street>();
            try
            {
                using (conn = new SqlConnection(ConString))
                {
                    string query = "Select b.StreetId,b.StreetName,b.Block_Id,s.BlockName " +
                                    "From Street b LEFT JOIN Block s on " +
                                        "b.Block_Id = s.BlockId";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.CommandType = CommandType.Text;
                    conn.Open();

                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read()) 
                    {                       

                        Street street = new Street
                        {
                            StreetId = (int)rdr["StreetId"],
                            StreetName = (string?)rdr["StreetName"],
                            Block_Id = (int)rdr["Block_Id"],

                            Blocks = new Block
                            {
                                BlockId = (int)rdr["Block_Id"],
                                BlockName = (string)rdr["BlockName"]
                            }
                        };                   

                        streetslist.Add(street);
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
            return streetslist;
        }

        public List<Block> GetBlockWithSector(int sectorId)
        {

            List<Block> blockslist = new List<Block>();
            try
            {
                using (conn = new SqlConnection(ConString))
                {
                    string query = "Select BlockId,BlockName From Block Where Sector_Id = @Sector_Id";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Sector_Id", sectorId);
                    conn.Open();

                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {

                        Block block = new Block
                        {
                            BlockId = (int)rdr["BlockId"],
                            BlockName = (string?)rdr["BlockName"]
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

        public List<Street> GetStreetWithBlockAndSectors()
        {
            List<Street> streetslist = new List<Street>();
            try
            {
                using (conn = new SqlConnection(ConString))
                {
                    string query = "Select s.StreetId,s.StreetName,s.Block_Id,b.BlockName,b.Sector_Id,sc.SectorName " +
                                    "From Street s LEFT JOIN Block b on s.Block_Id = b.BlockId LEFT JOIN Sector sc on " +
                                    " b.Sector_Id =  sc.SectorId ";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.CommandType = CommandType.Text;
                    conn.Open();

                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {

                        Street street = new Street
                        {
                            StreetId = (int)rdr["StreetId"],
                            StreetName = (string?)rdr["StreetName"],
                            Block_Id = (int)rdr["Block_Id"],

                            Blocks = new Block
                            {
                                BlockId = (int)rdr["Block_Id"],
                                BlockName = (string)rdr["BlockName"],

                                Sectors = new Sector
                                {
                                    SectorId = (int)rdr["Sector_Id"],
                                    SectorName = (string)rdr["SectorName"]
                                }
                            }
                        };

                        streetslist.Add(street);
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
            return streetslist;
        }
             
        public int AddStreet(Street street)
        {
            int res = 0;
            try
            {
                using (conn = new SqlConnection(ConString))
                {
                    string query = "INSERT INTO Street (StreetName,Block_Id) VALUES (@StreetName,@Block_Id)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@StreetName", street.StreetName);
                    cmd.Parameters.AddWithValue("@Block_Id", street.Block_Id);
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

        public Street GetStreetByID(int id)
        {
            Street street = new Street();
            try
            {
                using (conn = new SqlConnection(ConString))
                {
                    string query = "SELECT s.StreetId,s.StreetName,s.Block_Id,b.BlockName From Street s LEFT JOIN" +
                                    " Block b on s.Block_Id = b.BlockId Where s.StreetId = @StreetId";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@StreetId", id);
                    conn.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        // Street.StreetId = (int)rdr["StreetId"];
                        street = new Street
                        {
                            StreetId = (int)rdr["StreetId"],
                            StreetName = (string)rdr["StreetName"],
                            Block_Id = (int)rdr["Block_Id"],
                            Blocks = new Block
                            {
                                BlockId = (int)rdr["Block_Id"],
                                BlockName = (string)rdr["BlockName"]
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
            return street;
        }

        public string GetSectorName(int id)
        {
            string sectorName = "";
            try
            {
                using(conn = new SqlConnection(ConString))
                {
                    string query = "Select sc.SectorName from street s join block b on s.block_id = b.BlockId " +
                        "               join sector sc on b.sector_id = sc.sectorid where streetid = @StreetId";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@StreetId", id);
                    conn.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    if(rdr.Read())
                    {
                        sectorName = (string)rdr["SectorName"];
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
            return sectorName;
        }

        public void UpdateStreet(Street street)
        {
            try
            {
                using (conn = new SqlConnection(ConString))
                {
                    string query = "UPDATE Street SET StreetName = @StreetName,Block_Id = @BlockId WHERE StreetId = @StreetId";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@StreetName", street.StreetName);
                    cmd.Parameters.AddWithValue("@BlockId", street.Block_Id);
                    cmd.Parameters.AddWithValue("@StreetId", street.StreetId);
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

        public void DeleteStreet(int Id)
        {
            try
            {
                using (conn = new SqlConnection(ConString))
                {
                    string query = "DELETE FROM Street WHERE StreetId = @StreetId";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@StreetId", Id);
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
