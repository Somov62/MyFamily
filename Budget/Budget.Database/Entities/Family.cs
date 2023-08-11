using Budget.Database.Entities.Base;

namespace Budget.Database.Entities
{
    public class Family : NamedEntity
    {
        public IList<User> Relatives { get; set; } = new List<User>();

        public IList<TransactionCategoryGroup> Categories { get; set; } = new List<TransactionCategoryGroup>();

    }
}
