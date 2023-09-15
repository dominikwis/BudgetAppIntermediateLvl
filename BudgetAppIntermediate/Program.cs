using BudgetAppIntermediate.Data;
using BudgetAppIntermediate.Entity;
using BudgetAppIntermediate.Repositories;
using System.Globalization;
using System.Xml.Linq;

const string dateFormat = "yyyy-MM-dd";
DateTime convertedDate;
string exit = null;
var billsRepository = new SqlRepository<BillBase>(new BudgetAppIntermediateContext());

AddOneTimeBills(billsRepository);
AddRegularBills(billsRepository);
WriteAllToConsole(billsRepository);

Console.WriteLine("***********************************");
Console.WriteLine("Welcome to budget app!");
Console.WriteLine("***********************************");

Console.WriteLine("Write 'o' if you want to add One Time Bills or 'r' if Regular Bills. You can change it whenever you want.");
string option = Console.ReadLine();

while (exit == null)
{
    if (option == "o")
    {
        Console.WriteLine("Name: ");
        string name = Console.ReadLine();

        Console.WriteLine("Amount: ");
        string amount = Console.ReadLine();

        if (!decimal.TryParse(amount, out decimal convertedAmount))
        {
            Console.WriteLine("Invalid number format");
        }

        Console.WriteLine("Date (with correct format 'yyyy-MM-dd'): ");
        string date = Console.ReadLine();

        if (!DateTime.TryParseExact(date, dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out convertedDate))
        {
            Console.WriteLine("Invalid date format. Please use 'yyyy-MM-dd' for example: 2023-03-30.");
        }

        // Trzeba stworzyć nowy obiekt z tymi zebranymi danymi i przypisać go do jakiejś zmiennej, następnie zastosować bezpośrednio metode billRepository i użyć metody Add();

        Console.WriteLine("You added everthing correct!");
        Console.WriteLine("Write 'o' if you want to add One Time Bills.");
        Console.WriteLine("Write 'r' if you want to add Regular Bills");
        Console.WriteLine("write 'x' if you want to summarize your budget and exit the app");

        if (option == "x")
        {
            exit = "x";
        }
        else
        {
            option = Console.ReadLine();
        }
    }
    else if (option == "r")
    {
        Console.WriteLine("you already clicked 'r' option");
    }
}

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