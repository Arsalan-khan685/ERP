using ERP.Models;

namespace ERP.Services
{
    public interface IBankService
    {
        List<Bank> GetBanks();
        int AddBank(Bank bank);
        Bank GetBankByID(int id);
        void UpdateBank(Bank bank);

        void DeleteBank(int Id);
    }
}
