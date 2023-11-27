using BudgetAppIntermediate.Entity;

public interface IEventRepository<T> where T : IEntity, new()
{
    event EventHandler<BillEventArgs<T>> ItemAdded;
    event EventHandler<BillEventArgs<T>> ItemRemoved;
    event EventHandler<BillEventArgs<T>> AllItemRemoved;
}
