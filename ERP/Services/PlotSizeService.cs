using ERP.Models;
using System.Data.SqlClient;

namespace ERP.Services
{
    public class PlotSizeService : IPlotSizeService
    {
        public string ConString { get; set; }
        public IConfiguration _configuration;
        public SqlConnection conn;
        public PlotSizeService(IConfiguration configuration)
        {
            _configuration = configuration;
            ConString = _configuration.GetConnectionString("DBConnection");
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

        public int AddPlotSize(PlotSize plotSize)
        {
            int res = 0;
            try
            {
                using (conn = new SqlConnection(ConString))
                {
                    conn.Open();
                    string query = "INSERT into PlotSize(Plot_Size) Values(@Plot_Size)";
                    SqlCommand cmd = new SqlCommand(query,conn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.AddWithValue("@Plot_Size",plotSize.Plot_Size);
                    res = cmd.ExecuteNonQuery(); 
                    if(res > 0)
                    {
                   //     return res;
                    }
                    else 
                    {

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
            return res;
        }

        public PlotSize GetPlotSizeByID(int id)
        {
            PlotSize plotSize = new PlotSize();
            try
            {
                using (conn = new SqlConnection(ConString))
                {
                   
                    string query = "Select * from PlotSize Where PlotSizeId = @PlotSizeId";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.AddWithValue("@PlotSizeId", id);
                    conn.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        plotSize.PlotSizeId = Convert.ToInt32(rdr["PlotSizeId"]);
                        plotSize.Plot_Size = rdr["Plot_Size"].ToString();
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
            return plotSize;
        }

        public void UpdatePlotSize(PlotSize plotSize)
        {
            int res = 0;
            try
            {
                using (conn = new SqlConnection(ConString))
                {
                    
                    string query = "Update PlotSize SET Plot_Size = @Plot_Size Where PlotSizeId = @PlotSizeId";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.AddWithValue("@PlotSizeId", plotSize.PlotSizeId);
                    cmd.Parameters.AddWithValue("@Plot_Size", plotSize.Plot_Size);
                    conn.Open();
                    res = cmd.ExecuteNonQuery();
                    if (res > 0)
                    {
                        //     return res;
                    }
                    else
                    {

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
        }

        public void DeletePlotSize(int Id)
        {
            try
            {
                using(conn = new SqlConnection(ConString))
                {
                    string query = "Delete From PlotSize Where PlotSizeId = @PlotSizeId";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.CommandType = System.Data.CommandType.Text;                    
                    cmd.Parameters.AddWithValue("@PlotSizeId", Id);
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
