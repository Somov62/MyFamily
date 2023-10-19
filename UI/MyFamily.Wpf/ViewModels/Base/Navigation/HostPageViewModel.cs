using System;
using System.Collections.Generic;
namespace MyFamily.Wpf.ViewModels.Base.Navigation
{
    /// <summary>
    /// Класс вьюмодели для контейнера страниц.
    /// </summary>
    public class HostPageViewModel : PageViewModel
    {
        public HostPageViewModel()
        {
            Navigation.ViewChanged += Navigation_ViewChanged;
        }

        /// <summary>
        /// Свойство постраничной навигации
        /// </summary>
        public new NavigationService Navigation { get; } = new NavigationService();

        /// <summary>
        /// Обработчик изменения текущей страницы. Обновляет привязку свойства <see cref="CurrentView"/>
        /// </summary>
        private void Navigation_ViewChanged(PageViewModel _) => OnPropertyChanged(nameof(Navigation.CurrentView));

        /// <summary>
        /// Реализация интерфейса <see cref="INavigationService"/>.
        /// </summary>
        public sealed class NavigationService : INavigationService
        {

            /// <summary>
            /// Список всех страниц. 
            /// </summary>
            private readonly List<PageViewModel> _viewsStack = new();

            /// <summary>
            /// Текущая страница
            /// </summary>
            public PageViewModel CurrentView { get; private set; } = null!;

            /// <summary>
            /// Позволяет перейти на другую страницу. <br/>
            /// Страницы кэшируются, можно перейти на сохраненную по тому же инстансу, что и в первый раз.
            /// </summary>
            /// <param name="viewModel">Вьюмодель страницы</param>
            /// <exception cref="ArgumentNullException"></exception>
            public void Navigate(PageViewModel viewModel)
            {
                // Присвоение с проверкой на null.
                CurrentView = viewModel ?? throw new ArgumentNullException(nameof(viewModel));

                // Если на страницу не переходили раньше, добавляем её в список
                if (!_viewsStack.Contains(viewModel))
                {
                    _viewsStack.Add(viewModel);
                    viewModel.Navigation = this;
                }


                // Уведомляем страницу и подписчиков
                CurrentView.OnAppearing();
                ViewChanged?.Invoke(CurrentView);
            }

            /// <summary>
            /// Позволяет перейти назад на одку страницу.
            /// </summary>
            /// <param name="removeView">True - покидаемая страница будет удалена из кэша</param>
            /// <exception cref="Exception">При попытки покинуть первую страницу</exception>
            public void GoBack(bool removeView = true)
            {
                if (_viewsStack.Count < 2)
                {
                    throw new Exception("You cannot go back when less 2 views in stack");
                }
                int viewIndex = _viewsStack.IndexOf(CurrentView);
                if (viewIndex == 0)
                {
                    throw new Exception("You cannot go back, this view is first in stack");
                }
                if (removeView) _viewsStack.RemoveAt(viewIndex);

                Navigate(_viewsStack[viewIndex - 1]);
            }

            /// <summary>
            /// Событие, возникающее при переходе на страницу
            /// </summary>
            public event INavigationService.ViewChangedHandler? ViewChanged;
        }
    }

    /// <summary>
    /// Сервис постраничной навигации через вьюмодели.
    /// </summary>
    public interface INavigationService
    {
        /// <summary>
        /// Позволяет перейти на другую страницу. <br/>
        /// Страницы кэшируются, можно перейти на сохраненную по тому же инстансу, что и в первый раз.
        /// </summary>
        /// <param name="viewModel">Вьюмодель страницы</param>
        /// <exception cref="ArgumentNullException"></exception>
        void Navigate(PageViewModel viewModel);

        /// <summary>
        /// Позволяет перейти назад на одку страницу.
        /// </summary>
        /// <param name="removeView">True - покидаемая страница будет удалена из кэша</param>
        /// <exception cref="Exception">При попытки покинуть первую страницу</exception>
        void GoBack(bool removeView = true);

        /// <summary>
        /// Обработчик события <see cref="ViewChanged"/>
        /// </summary>
        /// <param name="actualView"></param>
        delegate void ViewChangedHandler(PageViewModel actualView);

        /// <summary>
        /// Событие, возникающее при переходе на страницу
        /// </summary>
        event ViewChangedHandler? ViewChanged;
    }
}
