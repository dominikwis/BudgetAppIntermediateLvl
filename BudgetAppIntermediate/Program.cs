using BudgetAppIntermediate.Data;
using BudgetAppIntermediate.Entity;
using BudgetAppIntermediate.Repositories;
using System.Xml.Linq;

var billsRepository = new SqlRepository<BillBase>(new BudgetAppIntermediateContext());

AddOneTimeBills(billsRepository);
AddRegularBills(billsRepository);
WriteAllToConsole(billsRepository);

static void AddOneTimeBills(IWriteRepository<OneTimeBills> addOneTimeBills)
{
    addOneTimeBills.Add(new OneTimeBills { Name = "Shopping", Amount = 30, Date = "25.08.2023" });
    addOneTimeBills.Add(new OneTimeBills { Name = "Restaurant", Amount = 120, Date = "07.08.2023" });
    addOneTimeBills.Add(new OneTimeBills { Name = "Computer", Amount = 3000, Date = "02.08.2023" });
    addOneTimeBills.Save();
}

static void AddRegularBills(IWriteRepository<RegularBills> addRegularBills)
{
    addRegularBills.Add(new RegularBills { Name = "OC/AC", Amount = 30, FixedFrequency = 360 });
    addRegularBills.Add(new RegularBills { Name = "Petrol", Amount = 120, FixedFrequency = 14 });
    addRegularBills.Add(new RegularBills { Name = "Savings", Amount = 3000, FixedFrequency = 10 });
    addRegularBills.Save();
}

static void WriteAllToConsole(IReadRepository<IEntity> repository)
{
    var allBills = repository.GetAll();
    foreach (var bill in allBills)
    {
        Console.WriteLine(bill);
    }
}