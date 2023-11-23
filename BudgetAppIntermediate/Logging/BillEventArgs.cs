using BudgetAppIntermediate.Entity;

public class BillEventArgs<T> : EventArgs
    where T : IEntity
{
    public string Action { get; set; }
    public T Item { get; set; }

    public BillEventArgs(string action, T item)
    {
        Action = action;
        Item = item;
    }
}
