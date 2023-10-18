using BudgetAppIntermediate.Entity;

namespace BudgetAppIntermediate.Repositories.Extensions
{
    public static class RepositoryExtensions
    {
        public static void addBatch<T>(this IRepository<T> repository, T[] items)
            where T : class, IEntity
        {
            foreach (var bill in items)
            {
                repository.Add(bill);
            }

            repository.Save();
        }
    }
}
