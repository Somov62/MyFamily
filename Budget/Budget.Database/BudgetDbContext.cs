using Budget.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Budget.Database
{
    /// <summary>
    /// Контекст базы данных бюджета
    /// </summary>
    public class BudgetDbContext : DbContext
    {
        public BudgetDbContext(DbContextOptions options) : base(options) { }

        #region Tables properties
        /// <summary>
        /// Таблица с группами категорий расходов/доходов
        /// </summary>
        public DbSet<TransactionCategoryGroup> TransactionCategoryGroups { get; set; }

        /// <summary>
        /// Таблица с категориями расходов/доходов
        /// </summary>
        public DbSet<TransactionCategory> TransactionCategories { get; set; }

        /// <summary>
        /// Таблица с транзакциями
        /// </summary>
        public DbSet<Transaction> Transactions { get; set; }

        /// <summary>
        /// Таблица семей
        /// </summary>
        public DbSet<Family> Families { get; set; }

        /// <summary>
        /// Таблица пользователей
        /// </summary>
        public DbSet<User> Users { get; set; } 
        #endregion

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //Конфигурация таблицы семья
            builder.Entity<Family>()
                .HasMany(p => p.Relatives)
                .WithOne(p => p.Family)
                .HasForeignKey(p => p.FamilyId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Family>()
                .HasMany(p => p.Categories)
                .WithOne(p => p.Family)
                .HasForeignKey(p => p.FamilyId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Family>().Navigation(p => p.Relatives).AutoInclude();
            builder.Entity<Family>().Navigation(p => p.Categories).AutoInclude();

            //Конфигурация таблицы пользователей
            builder.Entity<User>().HasIndex(nameof(User.Name)).IsUnique();

            builder.Entity<User>()
                .HasMany(p => p.Transactions)
                .WithOne(p => p.Actor)
                .HasForeignKey(p => p.ActorId)
                .OnDelete(DeleteBehavior.Cascade);

            //Конфигурация таблицы групп категорий транзакций
            builder.Entity<TransactionCategoryGroup>().HasIndex(nameof(TransactionCategoryGroup.Name)).IsUnique();

            builder.Entity<TransactionCategoryGroup>()
                .HasMany(p => p.Categories)
                .WithOne(p => p.Group)
                .HasForeignKey(p => p.GroupId)
                .OnDelete(DeleteBehavior.Cascade);

            //Конфигурация таблицы категорий транзакций
            builder.Entity<TransactionCategory>().HasIndex(nameof(TransactionCategory.Name)).IsUnique();

            builder.Entity<TransactionCategory>()
                .HasMany(p => p.Transactions)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
