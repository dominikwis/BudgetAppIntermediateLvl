using BudgetAppIntermediate.Entity;

namespace BudgetAppIntermediate.Repositories
{
    public interface IWriteRepository<in T> where T : class, IEntity
    {
        void Add(T item);
        //void Remove(T item);
        void RemoveAll();
        void RemoveById(int id);
        void Save();
    }
}
