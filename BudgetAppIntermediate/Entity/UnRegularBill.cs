namespace BudgetAppIntermediate.Entity
{
    public class UnRegularBill : RegularBill
    {
        public int Frequency { get; set; }

        public override string ToString() => $"Id: {Id}, Name of Bill: {Name}, Bill size: {Price}, Frequency: {Frequency}";
    }
}
