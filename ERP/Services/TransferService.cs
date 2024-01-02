using ERP.Models;
using ERP.Models.ViewModels;
using System.Data;
using System.Data.SqlClient;

namespace ERP.Services
{
    public class TransferService:ITransferService
    {
        public string ConString { get; set; }
        public IConfiguration _configuration;
        public SqlConnection conn;
        public TransferService(IConfiguration configuration)
        {
            _configuration = configuration;
            ConString = _configuration.GetConnectionString("DBConnection");
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
                            BlockId = Convert.ToInt32(rdr["BlockId"]),
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
                            StreetId = Convert.ToInt32(rdr["StreetId"]),
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
        public List<Plot> GetPlotsWithStreet(int streetId)
        {
            List<Plot> plotslist = new List<Plot>();
            try
            {
                using (conn = new SqlConnection(ConString))
                {
                    string query = "Select plotId,plotNo From Plot Where street_Id = @street_Id";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@street_Id", streetId);
                    conn.Open();

                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {

                        Plot plot = new Plot
                        {
                            PlotId = Convert.ToInt32(rdr["plotId"]),
                            PlotNo = (string)rdr["plotNo"]
                        };

                        plotslist.Add(plot);
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
            return plotslist;
        }
        public List<dynamic> GetPlotDetails(int plotId)
        {
            List<dynamic> plotslist = new List<dynamic>();
            try
            {
                using (conn = new SqlConnection(ConString))
                {
                    string query = "Select p.plotType_ID,p.plotSize_ID,pt.PlotTypeName,ps.Plot_Size From Plot p" +
                                    " inner join PlotType pt on p.PlotType_ID = pt.PlotTypeID" +
                                    " inner join PlotSize ps on p.PlotSize_ID = ps.PlotSizeID" +
                                    " Where PlotId = @PlotId";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@PlotId", plotId);
                    conn.Open();

                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        var plotDetails = new
                        {

                            //PlotId = rdr.GetInt32(rdr.GetOrdinal("PlotId")),
                            //PlotNo = rdr.GetString(rdr.GetOrdinal("PlotNo")),
                            // PlotType_Id = rdr.GetInt32(rdr.GetOrdinal("PlotType_Id")),
                            PlotType = rdr["PlotTypeName"].ToString(),  
                            PlotSize = rdr["Plot_Size"].ToString()      
                    };
                        plotslist.Add(plotDetails);
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
            return plotslist;
        }
        public void AddTransferInformation(TransferViewModel model)
        {
            try 
            {
                using(conn = new SqlConnection(ConString))
                {
                    SqlCommand cmd = new SqlCommand("TransferEntry", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Plot_ID", model.Clients?.Plot_ID);
                    cmd.Parameters.AddWithValue("@Name", (object)model.Clients?.Name ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@GuardianName", model.Clients?.GuardianName);
                    cmd.Parameters.AddWithValue("@CNIC", model.Clients?.CNIC);
                    cmd.Parameters.AddWithValue("@PassportNo", model.Clients?.PassportNo);
                    cmd.Parameters.AddWithValue("@MailingAddress", model.Clients?.MailingAddress);
                    cmd.Parameters.AddWithValue("@PermanentAddress", model.Clients?.PermanentAddress);
                    cmd.Parameters.AddWithValue("@PhoneNo", model.Clients?.PhoneNo);
                    cmd.Parameters.AddWithValue("@Email", model.Clients?.Email);
                    cmd.Parameters.AddWithValue("@NomineeName", model.Nominees?.NomineeName);
                    cmd.Parameters.AddWithValue("@NomineeGuardian", model.Nominees?.NomineeGuardian);
                    cmd.Parameters.AddWithValue("@NomineeCNIC", model.Nominees?.NomineeCNIC);
                    cmd.Parameters.AddWithValue("@Relation_ID", model.Nominees?.Relation_ID);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch 
            {

            }
            finally
            {
                conn.Close();
            }
        }
    }
}
