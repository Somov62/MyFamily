namespace MyFamily.Wpf.ViewModels.Base.Navigation
{
    /// <summary>
    /// Класс вьюмодели для страницы
    /// </summary>
    public class PageViewModel : BaseViewModel
    {
        /// <summary>
        /// Навигационное свойство.
        /// </summary>
        public virtual INavigationService? Navigation { get; set; }
    }
}
