using ERP.Models;
using System.Configuration;
using System.Data.SqlClient;

namespace ERP.Services
{
    public class PaymentPlanService:IPaymentPlanService
    {
        public string ConString { get; set; }
        public IConfiguration _configuration;
        public SqlConnection conn;
        public PaymentPlanService(IConfiguration configuration) 
        {
            _configuration = configuration;
            ConString = _configuration.GetConnectionString("DBConnection");
        }
        public List<PaymentPlan> GetAllPaymentPlans()
        {
            List<PaymentPlan> _listofPaymentplans = new List<PaymentPlan>();
            try
            {
                using (conn = new SqlConnection(ConString))
                {
                    conn.Open();
                    string query = "Select pp.*,ps.Plot_Size,pt.PlotTypeName,It.Installment_Type from PaymentPlan pp " +
                                    " inner join PlotSize ps on pp.PlotSize_ID=ps.PlotSizeId " +
                                    " inner join PlotType pt on pp.PlotType_ID=pt.PlotTypeId " +
                                    " inner join InstallmentType It on pp.InstallmentType_ID = It.InstallmentTypeId";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.CommandType = System.Data.CommandType.Text;

                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        PaymentPlan plan = new PaymentPlan
                        {
                            PaymentPlanID = Convert.ToInt32(rdr["PaymentPlanId"]),
                            InstallmentDuration = rdr["InstallmentDuration"].ToString(),
                            TotalPrice = (decimal)rdr["TotalPrice"],
                            DownPayment = (decimal)rdr["DownPayment"],
                            OnConfirmation = Convert.ToInt32(rdr["OnConfirmation"]),
                            AllocationFee = Convert.ToInt32(rdr["AllocationFee"]),
                            FormFee = Convert.ToInt32(rdr["FormFee"]),
                            Description = rdr["Description"].ToString(),
                            Balance = (decimal)rdr["Balance"],
                            InstallmentType = new InstallmentType
                            {
                                InstallmentTypeId = (int)rdr["InstallmentType_ID"],
                                Installment_Type = rdr["Installment_Type"].ToString()
                            },
                            InstallmentType_ID = (int)rdr["InstallmentType_ID"],
                            OneInstallmentAmount = (decimal)rdr["OneInstallmentAmount"],
                            NoOfInstallment = (int)rdr["NoOfInstallment"],
                            TotalInstallmentAmount = (decimal)rdr["TotalInstallmentAmount"],
                            PlotType = new PlotType
                            {
                                PlotTypeId = (int)rdr["PlotType_ID"],
                                PlotTypeName = rdr["PlotTypeName"].ToString()
                            },
                            PlotSize = new PlotSize
                            {
                                PlotSizeId = (int)rdr["PlotSize_ID"],
                                Plot_Size = rdr["Plot_Size"].ToString()
                            }
                        };
                    _listofPaymentplans.Add(plan);
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
            return _listofPaymentplans;
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

        public void AddPaymentPlan(PaymentPlan _plan)
        {
            try
            {
                using(conn = new SqlConnection(ConString))
                {
                    string query = "Insert INTO PaymentPlan(PlotType_ID,PlotSize_ID,InstallmentDuration,TotalPrice,DownPayment," +
                                        "OnConfirmation,AllocationFee,FormFee,Description,Balance,InstallmentType_ID," +
                                        "OneInstallmentAmount,NoOfInstallment,TotalInstallmentAmount) " +
                                    " Values (@PlotType,@PlotSize,@InstallmentDuration,@TotalPrice,@DownPayment," +
                                    "@OnConfirmation,@AllocationFee,@FormFee,@Description,@Balance,@InstallmentType," +
                                    "@OneInstallmentAmount,@NoOfInstallment,@TotalInstallmentAmount)";
                    SqlCommand cmd = new SqlCommand(query,conn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.AddWithValue("@PlotType",_plan.PlotType_ID);
                    cmd.Parameters.AddWithValue("@PlotSize",_plan.PlotSize_ID);
                    cmd.Parameters.AddWithValue("@InstallmentDuration",_plan.InstallmentDuration);
                    cmd.Parameters.AddWithValue("@TotalPrice",_plan.TotalPrice);
                    cmd.Parameters.AddWithValue("@DownPayment", _plan.DownPayment);
                    cmd.Parameters.AddWithValue("@OnConfirmation", _plan.OnConfirmation);
                    cmd.Parameters.AddWithValue("@AllocationFee", _plan.AllocationFee);
                    cmd.Parameters.AddWithValue("@FormFee", _plan.FormFee);
                    cmd.Parameters.AddWithValue("@Description", _plan.Description);
                    cmd.Parameters.AddWithValue("@Balance", _plan.Balance);
                    cmd.Parameters.AddWithValue("@InstallmentType", _plan.InstallmentType_ID);
                    cmd.Parameters.AddWithValue("@OneInstallmentAmount", _plan.OneInstallmentAmount);
                    cmd.Parameters.AddWithValue("@NoOfInstallment", _plan.NoOfInstallment);
                    cmd.Parameters.AddWithValue("@TotalInstallmentAmount", _plan.TotalInstallmentAmount);
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

        public PaymentPlan GetPaymentPlanById(int _paymentplanId)
        {
            PaymentPlan _paymentplan = new PaymentPlan();
            try
            {
                using (conn = new SqlConnection(ConString))
                {
                    string query = "Select * from PaymentPlan Where PaymentPlanID = @paymentplanId ";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.AddWithValue("@paymentplanId",_paymentplanId);
                    conn.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while(rdr.Read())
                    {
                        _paymentplan.PaymentPlanID = (int)rdr["PaymentPlanID"];
                        _paymentplan.PlotType_ID = (int)rdr["PlotType_ID"];
                        _paymentplan.PlotSize_ID = (int)rdr["PlotSize_ID"];
                        _paymentplan.InstallmentDuration = (string?)rdr["InstallmentDuration"];
                        _paymentplan.TotalPrice = (decimal)rdr["TotalPrice"];
                        _paymentplan.DownPayment = (decimal)rdr["DownPayment"];
                        _paymentplan.OnConfirmation = Convert.ToInt32(rdr["OnConfirmation"]);
                        _paymentplan.AllocationFee = Convert.ToInt32(rdr["AllocationFee"]);
                        _paymentplan.FormFee = Convert.ToInt32(rdr["FormFee"]);
                        _paymentplan.Description = (string?)rdr["Description"];
                        _paymentplan.Balance = (decimal)rdr["Balance"];
                        _paymentplan.InstallmentType_ID = (int)rdr["InstallmentType_ID"];
                        _paymentplan.OneInstallmentAmount = (decimal)rdr["OneInstallmentAmount"];
                        _paymentplan.NoOfInstallment = (int)rdr["NoOfInstallment"];
                        _paymentplan.TotalInstallmentAmount = (decimal)rdr["TotalInstallmentAmount"];
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
            return _paymentplan;
        }

        public void UpdatePaymentPlan(PaymentPlan _paymentPlan)
        {
            try
            {
                using(conn = new SqlConnection(ConString))
                {
                    string query = "Update PaymentPlan Set PlotType_ID=@PlotType_ID,PlotSize_ID=@PlotSize_ID," +
                        " InstallmentDuration=@InstallmentDuration,TotalPrice=@TotalPrice,DownPayment=@DownPayment," +
                        " OnConfirmation=@OnConfirmation,AllocationFee=@AllocationFee,FormFee=@FormFee," +
                        " Description=@Description,Balance=@Balance,InstallmentType_ID=@InstallmentType_ID," +
                        " OneInstallmentAmount=@OneInstallmentAmount,NoOfInstallment=@NoOfInstallment," +
                        " TotalInstallmentAmount=@TotalInstallmentAmount " +
                        " Where PaymentPlanID=@PaymentPlanID";
                    SqlCommand cmd = new SqlCommand(query,conn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.AddWithValue("@PaymentPlanID",_paymentPlan.PaymentPlanID);
                    cmd.Parameters.AddWithValue("@PlotType_ID",_paymentPlan.PlotType_ID);;
                    cmd.Parameters.AddWithValue("@PlotSize_ID", _paymentPlan.PlotSize_ID);
                    cmd.Parameters.AddWithValue("@InstallmentDuration", _paymentPlan.InstallmentDuration);
                    cmd.Parameters.AddWithValue("@TotalPrice", _paymentPlan.TotalPrice);
                    cmd.Parameters.AddWithValue("@DownPayment", _paymentPlan.DownPayment);
                    cmd.Parameters.AddWithValue("@OnConfirmation", _paymentPlan.OnConfirmation);
                    cmd.Parameters.AddWithValue("@AllocationFee", _paymentPlan.AllocationFee);
                    cmd.Parameters.AddWithValue("@FormFee", _paymentPlan.FormFee);
                    cmd.Parameters.AddWithValue("@Description", _paymentPlan.Description);
                    cmd.Parameters.AddWithValue("@Balance", _paymentPlan.Balance);
                    cmd.Parameters.AddWithValue("@InstallmentType_ID", _paymentPlan.InstallmentType_ID);
                    cmd.Parameters.AddWithValue("@OneInstallmentAmount", _paymentPlan.OneInstallmentAmount);
                    cmd.Parameters.AddWithValue("@NoOfInstallment", _paymentPlan.NoOfInstallment);
                    cmd.Parameters.AddWithValue("@TotalInstallmentAmount", _paymentPlan.TotalInstallmentAmount);
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

        public void DeletePaymentPlan(int id)
        {
            try
            {
                using (conn = new SqlConnection(ConString))
                {
                    string query = "Delete from PaymentPlan Where PaymentPlanID = @PaymentPlanId";
                    SqlCommand cmd = new SqlCommand(query,conn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.AddWithValue("@PaymentPlanId",id);
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
