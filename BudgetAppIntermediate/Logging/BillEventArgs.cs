using BudgetAppIntermediate.Entity;

public class BillEventArgs : EventArgs
{
    public string Action { get; set; }
    public BillBase Item { get; set; }

    public BillEventArgs(string action, BillBase item)
    {
        Action = action;
        Item = item;
    }
}
