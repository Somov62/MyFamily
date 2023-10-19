using Budget.Database.Entities.Base;

namespace Budget.Database.Entities
{
    /// <summary>
    /// Класс сущности семья
    /// </summary>
    public class Family : NamedEntity
    {
        public IList<User> Relatives { get; set; } = new List<User>();

        public IList<TransactionCategoryGroup> Categories { get; set; } = new List<TransactionCategoryGroup>();
        public string Password { get; set; }
    }
}
