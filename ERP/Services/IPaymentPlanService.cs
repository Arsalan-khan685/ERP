using ERP.Models;

namespace ERP.Services
{
    public interface IPaymentPlanService
    {
        List<PaymentPlan> GetAllPaymentPlans();
        List<PlotType> GetPlotTypes();
        List<PlotSize> GetPlotSizes();
        List<InstallmentType> GetInstallmentTypes();
        void AddPaymentPlan(PaymentPlan _plan);
        PaymentPlan GetPaymentPlanById(int _paymentplanId);
        void UpdatePaymentPlan(PaymentPlan _paymentPlan);
        void DeletePaymentPlan(int id);
    }
}
