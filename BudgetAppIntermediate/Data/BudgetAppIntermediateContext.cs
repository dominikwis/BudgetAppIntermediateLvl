using BudgetAppIntermediate.Entity;
using Microsoft.EntityFrameworkCore;

namespace BudgetAppIntermediate.Data
{
    public class BudgetAppIntermediateContext : DbContext
    {
        public DbSet<BillBase> billBase => Set<BillBase>();

        //public DbSet<OneTimeBill> oneTimeBills => Set<OneTimeBill>();

        //public DbSet<RegularBills> regularBills => Set<RegularBills>();

        //We have to give the name of database
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseInMemoryDatabase("StorageAppDb");
        }
    }
}
