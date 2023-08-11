using Microsoft.EntityFrameworkCore;

namespace Budget.Database
{
    public class BudgetDbContext : DbContext
    {


        public BudgetDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
