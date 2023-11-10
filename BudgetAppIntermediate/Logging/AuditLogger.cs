using BudgetAppIntermediate.Entity;
using BudgetAppIntermediate.Repositories;

public class AuditLogger
{
    private static string _logFilePath = "audits.txt";

    public AuditLogger(FileRepository<OneTimeBills> fileRepository)
    {
        fileRepository.ItemRemoved += LogEvent;
        fileRepository.ItemAdded += LogEvent;
        fileRepository.AllItemRemoved += LogEventNonParameters;
    }

    private void LogEvent(object? sender, BillEventArgs e)
    {
        WriteLog($"{e.Action}-{e.Item.Name}");
    }

    private void LogEventNonParameters(object sender, EventArgs e)
    {
        WriteLog($"AllBillsDeleted");
    }

    private void WriteLog(string logEntry)
    {
        try
        {
            string timestamp = DateTime.Now.ToString("yyy-MM-ddTHH:mm:ss.fffffff");
            string logMessage = $"[{timestamp}]-{logEntry}";
            string fullPath = Path.GetFullPath(_logFilePath);

            Console.WriteLine($"Writing to log file at: {fullPath}");

            using (var writer = new StreamWriter(fullPath, true))
            {
                writer.WriteLine(logMessage);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Failed to write to log file: {e.Message}");
        }
        
    }

}
