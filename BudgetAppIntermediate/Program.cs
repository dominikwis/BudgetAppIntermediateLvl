using BudgetAppIntermediate.Data;
using BudgetAppIntermediate.Entity;
using BudgetAppIntermediate.Repositories;
using BudgetAppIntermediate.Repositories.Extensions;

bool exit = false;
string option = null;

var oneTimeBills = new List<OneTimeBills>();

var billsRepository = new FileRepository<OneTimeBills>();
var auditLogger = new AuditLogger(billsRepository);

billsRepository.ItemAdded += BillRepositoryOnItemAdded;
billsRepository.ItemRemoved += BillRepositoryItemRemoved;
billsRepository.AllItemRemoved += BillRepositoryAllItemRemoved;

Console.WriteLine("===========================");
Console.WriteLine("Welcome to The Budget App!");
Console.WriteLine("===========================");

while (true)
{
    Console.WriteLine("<<< MENU >>>");
    Console.WriteLine("[1] Add One Time Bills");
    Console.WriteLine("[2] Add Regular Bills");
    Console.WriteLine("[3] Add Unregular Bills");
    Console.WriteLine("---");
    Console.WriteLine("[4] Display Bills");
    //Console.WriteLine("[5] Display a Specific Category of Bills");
    Console.WriteLine("---");
    Console.WriteLine("[5] Delete Specific Bill by Id");
    Console.WriteLine("[6] Delete All Bill");
    Console.WriteLine("---");
    Console.WriteLine("---");
    Console.WriteLine("[q] if want to exit the app\n");

    option = Console.ReadLine();

    if (option == "q")
    {
        break;
    }

    switch (option)
    {
        case "1":

            while(true)
            {
                OneTimeBills oneTimeBill = new OneTimeBills();

                Console.WriteLine("Give a name of the bill: ");
                oneTimeBill.Name = Console.ReadLine();

                Console.WriteLine("Give the price of the bill: ");
                string priceStr = Console.ReadLine();

                try
                {
                    oneTimeBill.Price = decimal.Parse(priceStr);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Exteption caught: you have to provide a number, not a symbol or text.");
                }

                DateTime currentDate = DateTime.Now;
                oneTimeBill.Date = currentDate.ToString("dd-MM-yyyy");

                oneTimeBills.Add(oneTimeBill);

                while (true)
                {
                    Console.WriteLine("Do you want to add another One Time Bill?");
                    Console.WriteLine("\n[y] to add new bill");
                    Console.WriteLine("[n] if want to come back to the menu\n");
                    option = Console.ReadLine();

                    if (option != "y" && option != "n")
                    {
                        Console.WriteLine("You have to enter correct letter\n");
                    }
                    else
                    {
                        break;
                    }
                }
                
                if (option == "n")
                {
                    billsRepository.addBatch(oneTimeBills);
                    oneTimeBills.Clear();
                    break;
                }
            }

            break;

        case "2":

            Console.WriteLine("\nStill in process, please use only One Time Bills in this version\n");
            
            break;

        case "3":

            Console.WriteLine("\nStill in process, please use only One Time Bills in this version\n");

            break;

        case "4":

            Console.WriteLine();
            try
            {
                IEnumerable<OneTimeBills> allBills = billsRepository.GetAll();

                Console.WriteLine("============================================================");

                foreach (var item in allBills)
                {
                    Console.WriteLine($" {item.Id} Name: {item.Name}, Price: {item.Price} pln, Date: {item.Date}");
                }

                Console.WriteLine("============================================================\n");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception caught: {e.Message}\n");
            }

            break;

        case "5":

            Console.WriteLine();

            try
            {
                IEnumerable<OneTimeBills> allBillsId = billsRepository.GetAll();

                Console.WriteLine("============================================================");
                foreach (var item in allBillsId)
                {
                    Console.WriteLine($"Id: {item.Id} Name: {item.Name}, Price: {item.Price} pln, Date: {item.Date}");
                }
                Console.WriteLine("============================================================");

                Console.WriteLine("\n['id number'] to delete the bill");
                Console.WriteLine("---");
                Console.WriteLine("[c] if want to come back to the menu\n");
                option = Console.ReadLine();

                if (option == "c")
                {
                    break;
                }
                else
                {
                    try
                    {
                        int id = int.Parse(option);
                        billsRepository.RemoveById(id);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine($"Exception caught: you have to provide a number, not a symbol or text.");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception caught: {e.Message}");
            }
            
            Console.WriteLine();

            break;

        case "6":

            Console.WriteLine();
            Console.WriteLine("Are you sure to delete all bills?\n");
            Console.WriteLine("[y] yes\n");
            Console.WriteLine("[n] no\n");

            option = Console.ReadLine();

            if (option == "y")
            {
                try
                {
                    billsRepository.RemoveAll();
                }
                catch (Exception e)
                {
                    Console.WriteLine($"\nException catched: {e.Message}");
                }
            }
            else if (option == "n")
            {
                break;
            }
            
            Console.WriteLine();

            break;

        default:
            Console.WriteLine("Try to use correct sign from MENU list");
            break;
    }
}
    
static void BillRepositoryOnItemAdded(object? sender, BillEventArgs e)
{
    Console.WriteLine($"\nBill added => {e.Item.Name} with action {e.Action} from {sender?.GetType().Name}\n");
}

static void BillRepositoryItemRemoved(object? sender, BillEventArgs e)
{
    Console.WriteLine($"\nBill removed {e.Item.Name} with action {e.Action} from {sender?.GetType().Name}\n");
}

static void BillRepositoryAllItemRemoved(object? sender, BillEventArgs e)
{
    Console.WriteLine($"\n{e.Action}: All bills have been removed!\n");
}