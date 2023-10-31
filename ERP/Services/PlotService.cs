using ERP.Models;
using ERP.Models.ViewModels;
using System.Data;
using System.Data.SqlClient;

namespace ERP.Services
{
    public class PlotService:IPlotService
    {
        public string ConString { get; set; }
        public int PlotSiz_Id { get; private set; }

        public IConfiguration _configuration;
        public SqlConnection conn;
        public PlotService(IConfiguration configuration) 
        {
            _configuration = configuration;
            ConString = _configuration.GetConnectionString("DBConnection");
        }

        public List<PlotViewModel> GetPlots()
        {
            List<PlotViewModel> _listofPlots = new List<PlotViewModel>();
            try
            {
                using(conn = new SqlConnection(ConString))
                {
                    string query = "SELECT p.PlotId,p.PlotNo,pt.PlotTypeId,pt.PlotTypeName,ps.PlotSizeId,ps.Plot_Size, " +
                                     "s.StreetId, s.StreetName, b.BlockId, b.BlockName, sec.SectorId, sec.SectorName " +
                                     "FROM Plot p " +
                                     "INNER JOIN PlotType pt ON p.PlotType_Id = pt.PlotTypeId " +
                                     "INNER JOIN PlotSize ps ON p.PlotSize_Id = ps.PlotSizeId " +
                                     "INNER JOIN Street s ON p.Street_Id = s.StreetId " +
                                     "INNER JOIN Block b ON s.Block_Id = b.BlockId " +
                                     "INNER JOIN Sector sec ON b.Sector_Id = sec.SectorId";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    conn.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while(rdr.Read())
                    {
                        PlotViewModel viewModel = new PlotViewModel
                        {
                            PlotId = Convert.ToInt32(rdr["PlotId"]),
                            PlotNo = rdr["PlotNo"].ToString(),
                            PlotTypeId = Convert.ToInt32(rdr["PlotTypeId"]),
                            PlotTypeName = rdr["PlotTypeName"].ToString(),
                            PlotSizeId = Convert.ToInt32(rdr["PlotSizeId"]),
                            Plot_Size = rdr["Plot_Size"].ToString(),
                            StreetId = Convert.ToInt32(rdr["StreetId"]),
                            StreetName = rdr["StreetName"].ToString(),
                            BlockId = Convert.ToInt32(rdr["BlockId"]),
                            BlockName = rdr["BlockName"].ToString(),
                            SectorId = Convert.ToInt32(rdr["SectorId"]),
                            SectorName = rdr["SectorName"].ToString()
                        };

                        _listofPlots.Add(viewModel);
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
            return _listofPlots;
        }
        public List<Sector> GetSectors()
        {
            List<Sector> sectorsList = new List<Sector>();
            try
            {
                using (conn = new SqlConnection(ConString))
                {
                    
                    string query = "Select * from Sector";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    conn.Open();
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
        public List<Street> GetStreets()
        {
            List<Street> streetsList = new List<Street>();
            try
            {
                using (conn = new SqlConnection(ConString))
                {
                    conn.Open();
                    string query = "Select * from Street";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.CommandType = System.Data.CommandType.Text;

                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        Street street = new Street();
                        street.StreetId = Convert.ToInt32(rdr["StreetId"]);
                        street.StreetName = rdr["StreetName"].ToString();

                        streetsList.Add(street);
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
            return streetsList;
        }
        public List<PlotSize> GetPlotSizes()
        {
            List<PlotSize> plotSizesList = new List<PlotSize>();
            try
            {
                using (conn = new SqlConnection(ConString))
                {

                    string query = "Select * from PlotSize";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    conn.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        PlotSize plotSize = new PlotSize();
                        plotSize.PlotSizeId = Convert.ToInt32(rdr["PlotSizeId"]);
                        plotSize.Plot_Size = rdr["Plot_Size"].ToString();

                        plotSizesList.Add(plotSize);
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
            return plotSizesList;
        }
        public List<PlotType> GetPlotTypes()
        {
            List<PlotType> PlotTypesList = new List<PlotType>();
            try
            {
                using (conn = new SqlConnection(ConString))
                {

                    string query = "Select * from PlotType";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    conn.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        PlotType PlotType = new PlotType();
                        PlotType.PlotTypeId = Convert.ToInt32(rdr["PlotTypeId"]);
                        PlotType.PlotTypeName = rdr["PlotTypeName"].ToString();

                        PlotTypesList.Add(PlotType);
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
            return PlotTypesList;
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
        public List<Street> GetStreetsWithBlock(int blockId)
        {
            List<Street> streetslist = new List<Street>();
            try
            {
                using (conn = new SqlConnection(ConString))
                {
                    string query = "Select StreetId,StreetName From Street Where Block_Id = @Block_Id";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Block_Id", blockId);
                    conn.Open();

                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {

                        Street street = new Street
                        {
                            StreetId = (int)rdr["StreetId"],
                            StreetName = (string?)rdr["StreetName"]
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
        public void AddPlot(PlotViewModel plot)
        {
            try
            {
                using(conn = new SqlConnection(ConString))
                {
                    string query = "INSERT INTO PLOT(PlotNo,PlotType_Id,PlotSize_Id,Street_Id) " +
                                        " Values (@PlotNo,@PlotType_Id,@PlotSize_Id,@Street_Id)";
                    SqlCommand cmd = new SqlCommand(query,conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@PlotNo",plot.PlotNo);
                    cmd.Parameters.AddWithValue("@PlotType_Id", plot.PlotTypeId);
                    cmd.Parameters.AddWithValue("@PlotSize_Id", plot.PlotSizeId);
                    cmd.Parameters.AddWithValue("@Street_Id", plot.StreetId);
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
        public string GetSectorName(int id)
        {
            string sectorName = "";
            try
            {
                using(conn = new SqlConnection(ConString))
                {
                    string query = "select SectorName from sector s inner join block b on s.sectorid=b.sector_id " +
                                    " inner join street st on b.blockid=st.block_id " +
                                    " inner join plot p on st.streetid=p.street_id where p.plotid=@PlotID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@PlotID", id);
                    conn.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    if (rdr.Read())
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
        public string GetBlockName(int id)
        {
            string blockName = "";
            try
            {
                using (conn = new SqlConnection(ConString))
                {
                    string query = "select blockName from block b inner join street st on b.blockid=st.block_id " +
                                    " inner join plot p on st.streetid=p.street_id where p.plotid=@PlotID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@PlotID", id);
                    conn.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        blockName = (string)rdr["blockName"];
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
            return blockName;
        }
        public Block GetBlockNameandId(int id)
        {
            Block block = new Block();
            try
            {
                using (conn = new SqlConnection(ConString))
                {
                    string query = "select blockid,blockName from block b inner join street st on b.blockid=st.block_id " +
                                    " inner join plot p on st.streetid=p.street_id where p.plotid=@PlotID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@PlotID", id);
                    conn.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        block.BlockId = (int)rdr["BlockId"];
                        block.BlockName = (string)rdr["blockName"];
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
        public PlotViewModel GetPlotById(int id)
        {
            PlotViewModel plot = new PlotViewModel();
            try
            {
                using(conn = new SqlConnection(ConString))
                {
                    string query = "select p.plotid,p.plotno,p.plottype_id,p.plotsize_id,p.street_id,ps.Plot_Size," +
                                    "pt.PlotTypeName,s.StreetName from plot p inner join plotsize ps " +
                                    "on p.plotsize_id=ps.plotsizeid join plottype pt on p.plottype_id=pt.plottypeid " +
                                    "join street s on p.street_id = s.streetid where plotid = @PlotID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@PlotID", id);
                    conn.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        plot.PlotId = (int)rdr["PlotId"];
                        plot.PlotNo = (string)rdr["PlotNo"];
                        plot.PlotSizeId = (int)rdr["PlotSize_Id"];
                        plot.Plot_Size = (string)rdr["Plot_Size"];
                        plot.PlotTypeId = (int)rdr["PlotType_Id"];
                        plot.PlotTypeName = (string)rdr["PlotTypeName"];
                        plot.StreetId = (int)rdr["Street_Id"];
                        plot.StreetName = (string)rdr["StreetName"];
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
            return plot;
        }

        public void UpdatePlot(Plot plot)
        {
            try
            {
                using(conn = new SqlConnection(ConString))
                {
                    string query = "Update Plot SET PlotNo=@PlotNo,PlotType_Id=@Plottype,PlotSize_Id=@Plotsize," +
                                    " Street_Id=@StreetId Where PlotId=@PlotId ";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@PlotId", plot.PlotId);
                    cmd.Parameters.AddWithValue("@PlotNo", plot.PlotNo);
                    cmd.Parameters.AddWithValue("@Plottype", plot.PlotType_Id);
                    cmd.Parameters.AddWithValue("@Plotsize", plot.PlotSize_Id);
                    cmd.Parameters.AddWithValue("@StreetId", plot.Street_Id);
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
        public void DeletePlot(int id)
        {
            try
            {
                using (conn = new SqlConnection(ConString))
                {
                    string query = "Delete from plot where PlotId=@PlotId ";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@PlotId",id);
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
