using ERP.Models;
using System.Data.SqlClient;

namespace ERP.Services
{
    public class BankService : IBankService
    {
        public string ConString { get; set; }
        public IConfiguration _configuration;
        public SqlConnection conn;
        public BankService(IConfiguration configuration)
        {
            _configuration = configuration;
            ConString = _configuration.GetConnectionString("DBConnection");
        }

        public List<Bank> GetBanks()
        {
            List<Bank> banksList = new List<Bank>();
            try
            {
                using (conn = new SqlConnection(ConString))
                {

                    string query = "Select * from Bank";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    conn.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        Bank Bank = new Bank();
                        Bank.BankId = Convert.ToInt32(rdr["BankId"]);
                        Bank.BankName = rdr["BankName"].ToString();

                        banksList.Add(Bank);
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
            return banksList;
        }

        public int AddBank(Bank bank)
        {
            int res = 0;
            try
            {
                using (conn = new SqlConnection(ConString))
                {
                    conn.Open();
                    string query = "INSERT into Bank(BankName) Values (@BankName)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.AddWithValue("@BankName", bank.BankName);
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

        public Bank GetBankByID(int id)
        {
            Bank Bank = new Bank();
            try
            {
                using (conn = new SqlConnection(ConString))
                {

                    string query = "Select * from Bank Where BankId = @BankId";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.AddWithValue("@BankId", id);
                    conn.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        Bank.BankId = Convert.ToInt32(rdr["BankId"]);
                        Bank.BankName = rdr["BankName"].ToString();
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
            return Bank;
        }

        public void UpdateBank(Bank bank)
        {
            int res = 0;
            try
            {
                using (conn = new SqlConnection(ConString))
                {

                    string query = "Update Bank SET BankName = @BankName Where BankId = @BankId";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.AddWithValue("@BankId", bank.BankId);
                    cmd.Parameters.AddWithValue("@BankName", bank.BankName);
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

        public void DeleteBank(int Id)
        {
            try
            {
                using (conn = new SqlConnection(ConString))
                {
                    string query = "Delete From Bank Where BankId = @BankId";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.AddWithValue("@BankId", Id);
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
