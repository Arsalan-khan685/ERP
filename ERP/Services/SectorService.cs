using ERP.Models;
using System.Data.SqlClient;

namespace ERP.Services
{
    public class SectorService : ISectorService
    {
        public string ConString { get; set; }
        public IConfiguration _configuration;
        public SqlConnection conn;
        public SectorService(IConfiguration configuration)
        {
            _configuration = configuration;
            ConString = _configuration.GetConnectionString("DBConnection");
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

        public string AddSector(Sector sector)
        {
            int res = 0;
            try
            {
                using (conn = new SqlConnection(ConString))
                {
                    conn.Open();
                    string query = "INSERT into Sector(SectorName) Values(@SectorName)";
                    SqlCommand cmd = new SqlCommand(query,conn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.AddWithValue("@SectorName",sector.SectorName);
                    res = cmd.ExecuteNonQuery(); 
                    if(res > 0)
                    {
                        return null;
                    }
                    else 
                    {
                        return "Error Inserting Sector";
                    }
                }
            }
            catch (Exception ex)
            {
                return "Sector Insertion Failed";
            }
            finally 
            {
                conn.Close();
            }        
        }

        public Sector GetSectorByID(int id)
        {
            Sector sec = new Sector();
            try
            {
                using (conn = new SqlConnection(ConString))
                {
                    conn.Open();
                    string query = "Select * from Sector Where SectorId = @SectorId";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.AddWithValue("@SectorId", id);

                    SqlDataReader rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        sec.SectorId = Convert.ToInt32(rdr["SectorId"]);
                        sec.SectorName = rdr["SectorName"].ToString();
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
            return sec;
        }

        public string UpdateSector(Sector sector)
        {
            int res = 0;
            try
            {
                using (conn = new SqlConnection(ConString))
                {
                    conn.Open();
                    string query = "Update Sector SET SectorName = @SectorName Where SectorId = @SectorId";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.AddWithValue("@SectorId", sector.SectorId);
                    cmd.Parameters.AddWithValue("@SectorName", sector.SectorName);
                    res = cmd.ExecuteNonQuery();
                    if (res > 0)
                    {
                        return null;
                    }
                    else
                    {
                        return "Error Inserting Sector";
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                conn.Close();
            }           
        }

        public void DeleteSector(int Id)
        {
            try
            {
                using(conn = new SqlConnection(ConString))
                {
                    string query = "Delete From Sector Where SectorId = @SectorId";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.CommandType = System.Data.CommandType.Text;                    
                    cmd.Parameters.AddWithValue("@SectorId", Id);
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
