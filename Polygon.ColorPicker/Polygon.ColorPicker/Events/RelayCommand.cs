using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Polygon.ColorPicker.Events
{
    /// <inheritdoc />
    /// <summary>
    /// Generic relay command
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RelayCommand<T> : ICommand
    {
        private readonly Predicate<T> _canExecute;
        private readonly Action<T> _execute;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="execute"></param>
        public RelayCommand(Action<T> execute)
            : this(execute, null) =>
            _execute = execute;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="execute"></param>
        /// <param name="canExecute"></param>
        public RelayCommand(Action<T> execute, Predicate<T> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        /// <summary>
        /// Verifies if the command can be executed
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter) => _canExecute == null || _canExecute((T)parameter);

        /// <summary>
        /// Executes the command
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            _execute((T)parameter);
        }

        /// <summary>
        /// Handles the execution event
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }

    /// <summary>
    /// RelayCommand class
    /// </summary>
    public class RelayCommand : ICommand
    {
        private readonly Predicate<object> _canExecute;
        private readonly Action<object> _execute;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="execute"></param>
        public RelayCommand(Action<object> execute)
            : this(execute, null) =>
            _execute = execute;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="execute"></param>
        /// <param name="canExecute"></param>
        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        /// <summary>
        /// Verifies if the command can be executed
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter) => _canExecute == null || _canExecute(parameter);

        /// <summary>
        /// Executes the command
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        // Ensures WPF commanding infrastructure asks all RelayCommand objects whether their
        // associated views should be enabled whenever a command is invoked 
        /// <summary>
        /// Handles the execution event
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
                CanExecuteChangedInternal += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
                CanExecuteChangedInternal -= value;
            }
        }

        private event EventHandler CanExecuteChangedInternal;

        /// <summary>
        /// Executes the CanExecuteChangedInternal EventHandler
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChangedInternal.Raise(this);
        }
    }
}
