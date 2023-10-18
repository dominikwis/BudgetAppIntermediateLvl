using BudgetAppIntermediate.Data;
using BudgetAppIntermediate.Entity;
using BudgetAppIntermediate.Repositories;
using BudgetAppIntermediate.Repositories.Extensions;

string exit = null;
string option = null;
var billsRepository = new SqlRepository<BillBase>(new BudgetAppIntermediateContext());

billsRepository.ItemAdded += BillRepositoryOnItemAdded;

Console.WriteLine("***************************");
Console.WriteLine("Welcome to The Budget App!");
Console.WriteLine("***************************");

while (exit == null)
{
    Console.WriteLine("<<< MENU >>>");
    Console.WriteLine("[1] Add One Time Bills");
    Console.WriteLine("[2] Add Regular Bills");
    Console.WriteLine("[3] Add Unregular Bills");
    Console.WriteLine("---");
    Console.WriteLine("[4] Delete Special Bill");
    Console.WriteLine("---");
    Console.WriteLine("[5] Display Bills");
    Console.WriteLine("[6] Display a Specific Bill");
    Console.WriteLine("---");
    Console.WriteLine("---");
    Console.WriteLine("[q] if want to exit the app");

    option = Console.ReadLine();

    switch (option)
    {
        case "1":
            Console.WriteLine("[q] if want to exit the app");

            break;

        case "2":
            Console.WriteLine("[q] if want to exit the app");
            break;

        case "3":
            Console.WriteLine("[q] if want to exit the app");
            break;
            Console.WriteLine("[q] if want to exit the app");
        case "4":
            Console.WriteLine("[q] if want to exit the app");
            break;
            Console.WriteLine("[q] if want to exit the app");
        case "5":
            Console.WriteLine("[q] if want to exit the app");
            break;
            Console.WriteLine("[q] if want to exit the app");
        case "6":
            Console.WriteLine("[q] if want to exit the app");
            break;

        default:
            Console.WriteLine("Try to use correct sign from MENU list");
            break;
    }

    
    // **************************************************************************

    Console.WriteLine("Write 'o' if you want to add One Time Bills or 'r' if Regular Bills.");
    option = Console.ReadLine();

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
            //AddRegularBills(billsRepository);

            Console.WriteLine("You added everthing correct!");
            Console.WriteLine("Your summarize result:");

            WriteAllToConsole(billsRepository);
            break;
        }
    }
    else if (option == "r")
    {
        //AddRegularBills(billsRepository);

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
    else
    {
        Console.WriteLine("Wrong letter! Please repeat this action.");
    }

    //*******************************************************************************

}

static void BillRepositoryOnItemAdded(object? sender, BillBase b)
{
    Console.WriteLine($"bill added => {b.Name} from {sender?.GetType().Name}");
}

static void AddOneTimeBills(IRepository<BillBase> oneTimeBillsRepository)
{
    var oneTimeBills = new[]
    {
        new OneTimeBills { Name = "Shopping", Amount = 30, Date = "2023-08-30" },
        new OneTimeBills { Name = "Restaurant", Amount = 120, Date = "2023-08-07" },
        new OneTimeBills { Name = "Computer", Amount = 3000, Date = "2023-08-02" }
    };

    oneTimeBillsRepository.addBatch(oneTimeBills);
}

static void AddRegularBills(IRepository<BillBase> regularBillsRepository)
{
    var regularBills = new[]
    {
        new RegularBills { Name = "OC/AC", Amount = 30, FixedFrequency = 360 },
        new RegularBills { Name = "Petrol", Amount = 120, FixedFrequency = 14 },
        new RegularBills { Name = "Savings", Amount = 3000, FixedFrequency = 10 }
    };

    regularBillsRepository.addBatch(regularBills);
}

static void WriteAllToConsole(IReadRepository<IEntity> repository)
{
    var allBills = repository.GetAll();
    foreach (var bill in allBills)
    {
        Console.WriteLine(bill);
    }
}