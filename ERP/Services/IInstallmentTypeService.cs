using ERP.Models;

namespace ERP.Services
{
    public interface IInstallmentTypeService
    {
        List<InstallmentType> GetInstallmentTypes();
        int AddInstallmentType(InstallmentType installmentType);
        InstallmentType GetInstallmentTypeByID(int id);
        void UpdateInstallmentType(InstallmentType installmentType);

        void DeleteInstallmentType(int Id);
    }
}
