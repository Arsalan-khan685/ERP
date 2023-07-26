using ERP.Models;
using System.Data.SqlClient;

namespace ERP.Services
{
    public class PlotTypeService : IPlotTypeService
    {
        public string ConString { get; set; }
        public IConfiguration _configuration;
        public SqlConnection conn;
        public PlotTypeService(IConfiguration configuration)
        {
            _configuration = configuration;
            ConString = _configuration.GetConnectionString("DBConnection");
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

        public int AddPlotType(PlotType PlotType)
        {
            int res = 0;
            try
            {
                using (conn = new SqlConnection(ConString))
                {
                    conn.Open();
                    string query = "INSERT into PlotType(PlotTypeName) Values(@PlotTypeName)";
                    SqlCommand cmd = new SqlCommand(query,conn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.AddWithValue("@PlotTypeName",PlotType.PlotTypeName);
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

        public PlotType GetPlotTypeByID(int id)
        {
            PlotType plottype = new PlotType();
            try
            {
                using (conn = new SqlConnection(ConString))
                {
                   
                    string query = "Select * from PlotType Where PlotTypeId = @PlotTypeId";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.AddWithValue("@PlotTypeId", id);
                    conn.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        plottype.PlotTypeId = Convert.ToInt32(rdr["PlotTypeId"]);
                        plottype.PlotTypeName = rdr["PlotTypeName"].ToString();
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
            return plottype;
        }

        public void UpdatePlotType(PlotType PlotType)
        {
            int res = 0;
            try
            {
                using (conn = new SqlConnection(ConString))
                {
                    
                    string query = "Update PlotType SET PlotTypeName = @PlotTypeName Where PlotTypeId = @PlotTypeId";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.AddWithValue("@PlotTypeId", PlotType.PlotTypeId);
                    cmd.Parameters.AddWithValue("@PlotTypeName", PlotType.PlotTypeName);
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

        public void DeletePlotType(int Id)
        {
            try
            {
                using(conn = new SqlConnection(ConString))
                {
                    string query = "Delete From PlotType Where PlotTypeId = @PlotTypeId";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.CommandType = System.Data.CommandType.Text;                    
                    cmd.Parameters.AddWithValue("@PlotTypeId", Id);
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
