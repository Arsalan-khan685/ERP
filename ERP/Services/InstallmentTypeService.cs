using ERP.Models;
using System.Data.SqlClient;

namespace ERP.Services
{
    public class InstallmentTypeService:IInstallmentTypeService
    {
        public string ConString { get; set; }
        public IConfiguration _configuration;
        public SqlConnection conn;
        public InstallmentTypeService(IConfiguration configuration)
        {
            _configuration = configuration;
            ConString = _configuration.GetConnectionString("DBConnection");
        }

        public List<InstallmentType> GetInstallmentTypes()
        {
            List<InstallmentType> installmentTypesList = new List<InstallmentType>();
            try
            {
                using (conn = new SqlConnection(ConString))
                {

                    string query = "Select * from InstallmentType";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    conn.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        InstallmentType installmentType = new InstallmentType();
                        installmentType.InstallmentTypeId = Convert.ToInt32(rdr["InstallmentTypeId"]);
                        installmentType.Installment_Type = rdr["Installment_Type"].ToString();

                        installmentTypesList.Add(installmentType);
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
            return installmentTypesList;
        }

        public int AddInstallmentType(InstallmentType installmentType)
        {
            int res = 0;
            try
            {
                using (conn = new SqlConnection(ConString))
                {
                    conn.Open();
                    string query = "INSERT into InstallmentType(Installment_Type) Values (@Installment_Type)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.AddWithValue("@Installment_Type", installmentType.Installment_Type);
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
            return res;
        }

        public InstallmentType GetInstallmentTypeByID(int id)
        {
            InstallmentType installmentType = new InstallmentType();
            try
            {
                using (conn = new SqlConnection(ConString))
                {

                    string query = "Select * from InstallmentType Where InstallmentTypeId = @InstallmentTypeId";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.AddWithValue("@InstallmentTypeId", id);
                    conn.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        installmentType.InstallmentTypeId = Convert.ToInt32(rdr["InstallmentTypeId"]);
                        installmentType.Installment_Type = rdr["Installment_Type"].ToString();
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
            return installmentType;
        }

        public void UpdateInstallmentType(InstallmentType installmentType)
        {
            int res = 0;
            try
            {
                using (conn = new SqlConnection(ConString))
                {

                    string query = "Update InstallmentType SET Installment_Type = @Installment_Type Where InstallmentTypeId = @InstallmentTypeId";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.AddWithValue("@InstallmentTypeId", installmentType.InstallmentTypeId);
                    cmd.Parameters.AddWithValue("@Installment_Type", installmentType.Installment_Type);
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

        public void DeleteInstallmentType(int Id)
        {
            try
            {
                using (conn = new SqlConnection(ConString))
                {
                    string query = "Delete From InstallmentType Where InstallmentTypeId = @InstallmentTypeId";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.AddWithValue("@InstallmentTypeId", Id);
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
