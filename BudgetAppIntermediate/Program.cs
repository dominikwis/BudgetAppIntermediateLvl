using BudgetAppIntermediate.Data;
using BudgetAppIntermediate.Entity;
using BudgetAppIntermediate.Repositories;
using BudgetAppIntermediate.Repositories.Extensions;
using Microsoft.EntityFrameworkCore;

string optionMainMenu = null;
string option = null;

var oneTimeBills = new List<OneTimeBill>();

Console.WriteLine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));

Console.WriteLine("===========================");
Console.WriteLine("Welcome to The Budget App!");
Console.WriteLine("===========================\n");

while (optionMainMenu != "1" && optionMainMenu != "2")
{
    Console.WriteLine("Choose where do you want to save your data: ");
    Console.WriteLine("[1] Save in the database");
    Console.WriteLine("[2] Save in the text file");
    Console.WriteLine("[q] Exit the app");

    optionMainMenu = Console.ReadLine();

    if (optionMainMenu == "q")
    {
        break;
    }

    switch (optionMainMenu)
    {
        case "1":

            var sqliteRepository = new SqliteRepository<OneTimeBill>(new BudgetAppIntermediateSqliteContext());
            var auditLoggerSqlite = new AuditLogger<SqliteRepository<OneTimeBill>>(sqliteRepository);

            sqliteRepository.ItemAdded += BillRepositoryOnItemAdded;
            sqliteRepository.ItemRemoved += BillRepositoryItemRemoved;
            sqliteRepository.AllItemRemoved += BillRepositoryAllItemRemoved;

            while (true)
            {
                Console.WriteLine("<<< MENU >>>");
                Console.WriteLine("[1] Add One Time Bills");
                Console.WriteLine("[2] Add Regular Bills");
                Console.WriteLine("[3] Add Unregular Bills");
                Console.WriteLine("---");
                Console.WriteLine("[4] Display Bills");
                Console.WriteLine("---");
                Console.WriteLine("[5] Delete Specific Bill by Id");
                Console.WriteLine("[6] Delete All Bill");
                Console.WriteLine("---");
                Console.WriteLine("---");
                Console.WriteLine("[q] if want to exit the app\n");

                option = Console.ReadLine();

                if(option == "q")
                {
                    break;
                }

                switch (option)
                {
                    case "1": //ADD ONE TIME BILLS //PRZETESTOWANE
                        while (true)
                        {
                            OneTimeBill oneTimeBill = new OneTimeBill();

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
                                sqliteRepository.addBatch(oneTimeBills);
                                oneTimeBills.Clear();
                                break;
                            }
                        }


                        break;

                    case "2": // ADD REGULAR BILLS

                        Console.WriteLine("\nStill in process, please use only One Time Bills in this version\n");

                        break;

                    case "3": // ADD UNREGULAR BILLS

                        Console.WriteLine("\nStill in process, please use only One Time Bills in this version\n");

                        break;

                    case "4":  //DISPLAY BILLS // PRZETESTOWANE

                        Console.WriteLine();
                        try
                        {
                            IEnumerable<OneTimeBill> allBills = sqliteRepository.GetAll();

                            Console.WriteLine("============================================================");

                            foreach (var item in allBills)
                            {
                                Console.WriteLine($"Id: {item.Id} Name: {item.Name}, Price: {item.Price} pln, Date: {item.Date}");
                            }

                            Console.WriteLine("============================================================\n");
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"Exception caught: {e.Message}\n");
                        }

                        break;

                    case "5": //DELETE SPECIFIC BILLS BY ID // PRZETESTOWANE

                        Console.WriteLine();

                        try
                        {
                            IEnumerable<OneTimeBill> allBillsId = sqliteRepository.GetAll();

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
                                    sqliteRepository.RemoveById(id);
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

                    case "6": //DELETE ALL BILLS // PRZETESTOWANE

                        Console.WriteLine();
                        Console.WriteLine("Are you sure to delete all bills?\n");
                        Console.WriteLine("[y] yes\n");
                        Console.WriteLine("[n] no\n");

                        option = Console.ReadLine();

                        if (option == "y")
                        {
                            try
                            {
                                sqliteRepository.RemoveAll();
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

            break;

        case "2": 

            var fileRepository = new FileRepository<OneTimeBill>();
            var auditLogger = new AuditLogger<FileRepository<OneTimeBill>>(fileRepository);

            fileRepository.ItemAdded += BillRepositoryOnItemAdded;
            fileRepository.ItemRemoved += BillRepositoryItemRemoved;
            fileRepository.AllItemRemoved += BillRepositoryAllItemRemoved;

            while (true)
            {
                Console.WriteLine("<<< MENU >>>");
                Console.WriteLine("[1] Add One Time Bills");
                Console.WriteLine("[2] Add Regular Bills");
                Console.WriteLine("[3] Add Unregular Bills");
                Console.WriteLine("---");
                Console.WriteLine("[4] Display Bills");
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
                    case "1": //ADD ONE TIME BILLS //PRZETESTOWANE

                        while (true)
                        {
                            OneTimeBill oneTimeBill = new OneTimeBill();

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
                                fileRepository.addBatch(oneTimeBills);
                                oneTimeBills.Clear();
                                break;
                            }
                        }

                        break;

                    case "2": // ADD REGULAR BILLS

                        Console.WriteLine("\nStill in process, please use only One Time Bills in this version\n");

                        break;

                    case "3": // ADD UNREGULAR BILLS

                        Console.WriteLine("\nStill in process, please use only One Time Bills in this version\n");

                        break;

                    case "4": //DISPLAY BILLS // PRZETESTOWANE

                        Console.WriteLine();
                        try
                        {
                            IEnumerable<OneTimeBill> allBills = fileRepository.GetAll();

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

                    case "5": //DELETE SPECIFIC BILLS BY ID // PRZETESTOWANE

                        Console.WriteLine();

                        try
                        {
                            IEnumerable<OneTimeBill> allBillsId = fileRepository.GetAll();

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
                                    fileRepository.RemoveById(id);
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

                    case "6": //DELETE ALL BILLS // PRZETESTOWANE

                        Console.WriteLine();
                        Console.WriteLine("Are you sure to delete all bills?\n");
                        Console.WriteLine("[y] yes\n");
                        Console.WriteLine("[n] no\n");
                        option = Console.ReadLine();
                        if (option == "y")
                        {
                            try
                            {
                                fileRepository.RemoveAll();
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

            break;

        case "q":

            optionMainMenu = "q";

            break;

        default:
            Console.WriteLine("Try to use correct sign from suggest options");
            break;
    }
}

static void BillRepositoryOnItemAdded(object? sender, BillEventArgs<OneTimeBill> e)
{
    Console.WriteLine($"\nBill added => {e.Item.Name} with action {e.Action} from {sender?.GetType().Name}\n");
}
static void BillRepositoryItemRemoved(object? sender, BillEventArgs<OneTimeBill> e)
    {
        Console.WriteLine($"\nBill removed {e.Item.Name} with action {e.Action} from {sender?.GetType().Name}\n");
    }
static void BillRepositoryAllItemRemoved(object? sender, BillEventArgs<OneTimeBill> e)
{
    Console.WriteLine($"\n{e.Action}: All bills have been removed!\n");
}