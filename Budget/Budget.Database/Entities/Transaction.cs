using Budget.Database.Entities.Base;

namespace Budget.Database.Entities
{
    /// <summary>
    /// Сущность транзакция.
    /// </summary>
    public class Transaction : Entity
    {
        /// <summary>
        /// Создатель транзакции.
        /// </summary>
        public User Actor { get; set; }

        /// <summary>
        /// Внешний ключ на таблицу пользователей.
        /// </summary>
        public int ActorId { get; set; }

        /// <summary>
        /// Дата и время транзакции.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Сумма транзакции.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Категория.
        /// </summary>
        public TransactionCategory Category { get; set; }

        /// <summary>
        /// Внешний ключ на таблицу категорий.
        /// </summary>
        public int CategoryId { get; set; }

    }
}
