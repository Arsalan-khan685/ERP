using ERP.Models;

namespace ERP.Services
{
    public interface IRelationService
    {
        List<Relation> GetRelations();
        int AddRelation(Relation relation);
        Relation GetRelationByID(int id);
        void UpdateRelation(Relation relation);

        void DeleteRelation(int Id);
    }
}
