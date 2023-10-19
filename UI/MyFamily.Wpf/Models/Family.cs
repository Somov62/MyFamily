namespace MyFamily.Wpf.Models
{
    /// <summary>
    /// Класс представления семьи.
    /// </summary>
    internal class Family
    {
        /// <summary>
        /// Уникальный код семьи.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Имя семьи.
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// Пароль семьи.
        /// </summary>
        public string Password { get; set; } = null!;
    }
}
