using BudgetAppIntermediate.Entity;

namespace BudgetAppIntermediate.Repositories
{
    public interface IRepository<T> : IReadRepository<T>, IWriteRepository<T>
        where T : class, IEntity
    {
    }
}
