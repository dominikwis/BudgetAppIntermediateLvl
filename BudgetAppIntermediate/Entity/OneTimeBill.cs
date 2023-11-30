namespace BudgetAppIntermediate.Entity
{
    public  class OneTimeBill : BillBase
    {
        public string Date { get; set; }

        public override string ToString() => $"Id: {Id}, Name of Bill: {Name}, Executed: {Price} pln";
    }
}
