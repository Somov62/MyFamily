using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyFamily.XF.Database
{
    internal class TransactionsDbContext
    {
    }

    public class Transaction
    {
        [PrimaryKey, Column(nameof(Guid))]
        public Guid Guid { get; set; }


        [Column(nameof(Name)), NotNull()]
        public string Name { get; set; }

        [Column(nameof(Description))]
        public string Description { get; set; }

        /// <summary>
        /// Дата и время транзакции.
        /// </summary>
        [Column(nameof(Date))]
        public DateTime Date { get; set; }

        /// <summary>
        /// Сумма транзакции.
        /// </summary>
        [Column(nameof(Amount))]
        public decimal Amount { get; set; }

        /// <summary>
        /// Категория.
        /// </summary>
        [ManyToOne()]
        public TransactionCategory Category { get; set; }
    }

    public enum TransactionType
    {
        Expense,
        Profit
    }


    public class TransactionCategoryGroup
    {
        [PrimaryKey, Column(nameof(Guid))]
        public Guid Guid { get; set; }


        [Column(nameof(Name)), NotNull()]
        public string Name { get; set; }
        
        [Column(nameof(Type)), NotNull()]
        public TransactionType Type { get; set; }
        
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public IList<TransactionCategory> TransactionCategories { get; set; } = new List<TransactionCategory>();
    }

    public class TransactionCategory
    {

        [PrimaryKey, Column(nameof(Guid))]
        public Guid Guid { get; set; }

        [Column(nameof(Name)), NotNull()]
        public string Name { get; set; }

        [ManyToOne()]
        public TransactionCategoryGroup TransactionCategoryGroup { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public IList<Transaction> Transactions { get; set; } = new List<Transaction>();
    }

}
