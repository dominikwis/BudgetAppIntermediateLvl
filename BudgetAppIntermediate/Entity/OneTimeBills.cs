using Newtonsoft.Json;

namespace BudgetAppIntermediate.Entity
{
    public  class OneTimeBills : BillBase
    {
        //public DateTime Date { get; set; }
        //public string DateString { get; set; }

        public override string ToString() => $"Id: {Id}, Name of Bill: {Name}, Executed: {Price} pln, Date: {Date}";
    }
}
