﻿namespace BudgetAppIntermediate.Entity
{
    public class RegularBill : BillBase
    {
        public int FixedFrequency { get; set; }

        public override string ToString() => $"Id: {Id}, Name of Bill: {Name}, Executed: {Price} pln, Frequency: {FixedFrequency}";
    }
}
