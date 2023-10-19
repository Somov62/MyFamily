using System;
using System.Windows.Input;

namespace MyFamily.Wpf.MVVM
{
    /// <summary>
    /// Реализация интерфейса <see cref="ICommand"/> для ассинхронной команды
    /// </summary>
    internal class AsyncCommand : RelayCommand
    {
        /// <summary>
        /// Состояние исполнения
        /// </summary>
        private bool _isBusy = false;

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public AsyncCommand(Action execute, Func<bool>? canExecute = null) : base(execute, canExecute) { }

        /// <summary>
        /// Событие завершения асинхронной операции
        /// </summary>
        internal void AsyncFinalize()
        {
            if (!_isBusy)
                throw new InvalidOperationException("invalid state");
            _isBusy = false;
            DoCanExecuteChanged();
        }

        /// <summary>
        /// Проверка на возможность исполнения команды
        /// </summary>
        public override bool CanExecute(object? parameter) 
            => base.CanExecute(parameter) && !_isBusy;

        /// <summary>
        /// Обработка команды
        /// </summary>
        public override void Execute(object? parameter)
        {
            if (_isBusy)
                throw new InvalidOperationException("invalid state");
            // Начало обработки
            _isBusy = true;
            DoCanExecuteChanged();
            // Обработка
            base.Execute(parameter);
        }
    }
}