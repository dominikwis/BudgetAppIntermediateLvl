namespace BudgetAppIntermediate.Entity
{
    public class RegularBills : BillBase
    {
        public int FixedFrequency { get; set; }

        public override string ToString() => $"Id: {Id}, Name of Bill: {Name}, Executed: {Amount} pln, Frequency: {FixedFrequency}";
    }
}
