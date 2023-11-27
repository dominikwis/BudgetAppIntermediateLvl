using BudgetAppIntermediate.Entity;
using Microsoft.EntityFrameworkCore;

public class BudgetAppIntermediateSqliteContext : DbContext
{
    public DbSet<OneTimeBills> oneTimeBills { get; set; }

    public string DbPath { get; }

    public BudgetAppIntermediateSqliteContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Join(path, "budget.db");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite($"Data Source={DbPath}");
}
