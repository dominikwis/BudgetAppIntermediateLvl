namespace BudgetAppIntermediate.Entity
{
    public class UnRegularBills : RegularBills
    {
        public int Frequency { get; set; }

        public override string ToString() => $"Id: {Id}, Name of Bill: {Name}, Bill size: {Price}, Frequency: {Frequency}";
    }
}
