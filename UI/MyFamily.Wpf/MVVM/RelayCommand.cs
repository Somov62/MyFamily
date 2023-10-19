using System;
using System.Windows.Input;

namespace MyFamily.Wpf.MVVM
{
    /// <summary>
    /// Реализация интерфейса <see cref="ICommand"/> по умолчанию
    /// </summary>
    internal class RelayCommand : ICommand
    {
        /// <summary>
        /// Действие команды
        /// </summary>
        private readonly Action _execute;

        /// <summary>
        /// Действие, проверяющие возможность совершения действия команды
        /// </summary>
        private readonly Func<bool>? _canExecute;

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        /// <param name="execute">Действие команды</param>
        /// <param name="canExecute">Действие, проверяющие возможность совершения действия команды</param>
        public RelayCommand(Action execute, Func<bool>? canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        /// <summary>
        /// Событие изменения возможности выполнения команды
        /// </summary>
        public event EventHandler? CanExecuteChanged;

        /// <summary>
        /// Вызов события изменения возможности выполнения команды
        /// </summary>
        public void DoCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);

        /// <summary>
        /// Проверка на возможность исполнения команды.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public virtual bool CanExecute(object? parameter) 
            => _canExecute == null || _canExecute();

        /// <summary>
        /// Обработка команды
        /// </summary>
        public virtual void Execute(object? parameter) => _execute();
    }
}
