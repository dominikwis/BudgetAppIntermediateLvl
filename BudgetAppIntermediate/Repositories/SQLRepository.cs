//using BudgetAppIntermediate.Entity;
//using Microsoft.EntityFrameworkCore;

////public delegate void ItemAdded<in T>(T item);

//namespace BudgetAppIntermediate.Repositories
//{
//    public class SqlRepository<T> : IRepository<T>
//        where T : class , IEntity, new()
//    {
//        private readonly DbContext _dbContext;
//        private readonly DbSet<T> _dbSet;
//        //private readonly Action<T>? _itemAddedCallback;

//        public SqlRepository(DbContext dbContext/*, Action<T>? itemCallback = null*/)
//        {
//            _dbContext = dbContext;
//            _dbSet = _dbContext.Set<T>();
//            //_itemAddedCallback = itemCallback;
//        }

//        public event EventHandler<T>? ItemAdded;

//        public IEnumerable<T> GetAll()
//        {
//            return _dbSet.ToList();
//        }

//        public T? GetById(int id)
//        {
//            return _dbSet.Find(id);
//        }

//        public void Add(T item)
//        {
//            _dbSet.Add(item);
//            //_itemAddedCallback?.Invoke(item);
//            ItemAdded.Invoke(this, item);
//        }

//        public void Remove(T item)
//        {
//            _dbSet.Remove(item);
//        }

//        public void Save()
//        {
//            _dbContext.SaveChanges();
//        }
//    }
//}
