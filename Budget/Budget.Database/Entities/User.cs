using Budget.Database.Entities.Base;

namespace Budget.Database.Entities
{
    /// <summary>
    /// Сущность пользователя - члена семьи.
    /// </summary>
    public class User : NamedEntity
    {
        /// <summary>
        /// Уникальный логин пользователя.
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Зашированный md5 пароль пользователя.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Свойство связи с сущностью Семья.
        /// </summary>
        public Family Family { get; set; }

        /// <summary>
        /// Внешний ключ на таблицу Семья.
        /// </summary>
        public int FamilyId { get; set; }

        /// <summary>
        /// Транзакции пользователя
        /// </summary>
        public IList<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
