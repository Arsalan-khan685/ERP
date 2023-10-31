using ERP.Models;
using System.Data.SqlClient;

namespace ERP.Services
{
    public class RelationService:IRelationService
    {
        public string ConString { get; set; }
        public IConfiguration _configuration;
        public SqlConnection conn;
        public RelationService(IConfiguration configuration)
        {
            _configuration = configuration;
            ConString = _configuration.GetConnectionString("DBConnection");
        }

        public List<Relation> GetRelations()
        {
            List<Relation> relationsList = new List<Relation>();
            try
            {
                using (conn = new SqlConnection(ConString))
                {

                    string query = "Select * from Relation";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    conn.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        Relation Relation = new Relation();
                        Relation.RelationId = Convert.ToInt32(rdr["RelationId"]);
                        Relation.RelationName = rdr["RelationName"].ToString();

                        relationsList.Add(Relation);
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
            return relationsList;
        }

        public int AddRelation(Relation relation)
        {
            int res = 0;
            try
            {
                using (conn = new SqlConnection(ConString))
                {
                    conn.Open();
                    string query = "INSERT into Relation(RelationName) Values (@RelationName)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.AddWithValue("@RelationName", relation.RelationName);
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

        public Relation GetRelationByID(int id)
        {
            Relation relation = new Relation();
            try
            {
                using (conn = new SqlConnection(ConString))
                {

                    string query = "Select * from Relation Where RelationId = @RelationId";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.AddWithValue("@RelationId", id);
                    conn.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        relation.RelationId = Convert.ToInt32(rdr["RelationId"]);
                        relation.RelationName = rdr["RelationName"].ToString();
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
            return relation;
        }

        public void UpdateRelation(Relation relation)
        {
            int res = 0;
            try
            {
                using (conn = new SqlConnection(ConString))
                {

                    string query = "Update Relation SET RelationName = @RelationName Where RelationId = @RelationId";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.AddWithValue("@RelationId", relation.RelationId);
                    cmd.Parameters.AddWithValue("@RelationName", relation.RelationName);
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

        public void DeleteRelation(int Id)
        {
            try
            {
                using (conn = new SqlConnection(ConString))
                {
                    string query = "Delete From Relation Where RelationId = @RelationId";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.AddWithValue("@RelationId", Id);
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
