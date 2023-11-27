using BudgetAppIntermediate.Entity;
using BudgetAppIntermediate.Repositories;
using Microsoft.EntityFrameworkCore;

public class SqliteRepository<T> : IRepository<T>, IEventRepository<T>
    where T : class, IEntity, new()
{
    private readonly DbContext _dbContext;
    private readonly DbSet<T> _dbSet;

    private List<T> _items;

    public SqliteRepository(DbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<T>();
    }

    public event EventHandler<BillEventArgs<T>> ItemAdded;
    public event EventHandler<BillEventArgs<T>> ItemRemoved;
    public event EventHandler<BillEventArgs<T>> AllItemRemoved;

    public IEnumerable<T> GetAll()
    {
        return _dbSet.ToList();
    }

    public T? GetById(int id)
    {
        return _dbSet.Find(id);
    }

    public void Add(T item)
    {
        _dbSet.Add(item);
        this.ItemAdded?.Invoke(this, new BillEventArgs<T>("BillAdded", item));
    }

    public void RemoveById(int id)
    {
        var itemToRemove = _dbSet.Find(id);
        if (itemToRemove != null)
        {
            _dbSet.Remove(itemToRemove);
            _dbContext.SaveChanges();
            this.ItemRemoved?.Invoke(this, new BillEventArgs<T>("BillRemoved", itemToRemove));
        }
        else
        {
            throw new Exception("Failure: there is no such an item to remove.");
        }
    }

    public void RemoveAll()
    {
        var allBills = this.GetAll();

        if(allBills.Any())
        {
            _dbSet.RemoveRange(allBills);
            _dbContext.SaveChanges();
            this.AllItemRemoved?.Invoke(this, new BillEventArgs<T>("AllBillsDeleted", null));
        }
        else
        {
            throw new Exception("Failure: the list of the bills is already empty");
        }
    }

    public void Save()
    {
        _dbContext.SaveChanges();
    }
}

