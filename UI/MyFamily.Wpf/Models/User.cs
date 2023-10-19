namespace MyFamily.Wpf.Models
{
    //Класс представления пользователя
    internal class User
    {
        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Зашифрованный md5 пароль пользователя
        /// </summary>
        public string? Password { get; set; }
    }
}
