using Budget.Database.Entities.Base;

namespace Budget.Database.Entities
{
    public enum TransactionType
    {
        Expense,
        Profit
    }


    public class TransactionCategoryGroup : NamedEntity
    {
        public TransactionType TransactionType { get; set; } 
        public IList<TransactionCategory> Categories { get; set; } = new List<TransactionCategory>();

        public Family Family { get; set; }

        public int FamilyId { get; set; }
    }

    public class TransactionCategory : NamedEntity 
    {
        public TransactionCategoryGroup Group { get; set; }

        public int GroupId { get; set; }

        public IList<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
