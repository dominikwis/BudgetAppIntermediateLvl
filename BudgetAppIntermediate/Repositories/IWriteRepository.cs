using BudgetAppIntermediate.Entity;

namespace BudgetAppIntermediate.Repositories
{
    public interface IWriteRepository<in T> where T : class, IEntity
    {
        void Add(T item);

        void RemoveById(int id);
        void Save();
    }
}
