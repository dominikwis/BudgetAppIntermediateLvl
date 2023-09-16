using BudgetAppIntermediate.Data;
using BudgetAppIntermediate.Entity;
using BudgetAppIntermediate.Repositories;
using System.Globalization;
using System.Xml.Linq;

string exit = null;
var billsRepository = new SqlRepository<BillBase>(new BudgetAppIntermediateContext());

Console.WriteLine("***********************************");
Console.WriteLine("Welcome to budget app!");
Console.WriteLine("***********************************");

Console.WriteLine("Write 'o' if you want to add One Time Bills or 'r' if Regular Bills.");
string option = Console.ReadLine();

while (exit == null)
{
    if (option == "o")
    {
        AddOneTimeBills(billsRepository);

        Console.WriteLine("You added everthing correct!");
        Console.WriteLine("Write 'r' if you want to add Regular Bills");
        Console.WriteLine("write 'x' if you want to summarize your budget and exit the app");
        option = Console.ReadLine();

        if (option == "x")
        {
            WriteAllToConsole(billsRepository);
            break;
        }
        else if (option == "r")
        {
            AddRegularBills(billsRepository);

            Console.WriteLine("You added everthing correct!");
            Console.WriteLine("Your summarize result:");

            WriteAllToConsole(billsRepository);
            break;
        }
    }
    else if (option == "r")
    {
        AddRegularBills(billsRepository);

        Console.WriteLine("You added everthing correct!");
        Console.WriteLine("Write 'o' if you want to add One Time Bills");
        Console.WriteLine("write 'x' if you want to summarize your budget and exit the app");
        option = Console.ReadLine();

        if (option == "x")
        {
            WriteAllToConsole(billsRepository);
            break;
        }
        else if (option == "o")
        {
            AddOneTimeBills(billsRepository);

            Console.WriteLine("You added everthing correct!");
            Console.WriteLine("Your summarize result:");

            WriteAllToConsole(billsRepository);
            break;
        }
    }
}

static void AddOneTimeBills(IWriteRepository<OneTimeBills> addOneTimeBills)
{
    addOneTimeBills.Add(new OneTimeBills { Name = "Shopping", Amount = 30, Date = "2023-08-30" });
    addOneTimeBills.Add(new OneTimeBills { Name = "Restaurant", Amount = 120, Date = "2023-08-07" });
    addOneTimeBills.Add(new OneTimeBills { Name = "Computer", Amount = 3000, Date = "2023-08-02" });
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