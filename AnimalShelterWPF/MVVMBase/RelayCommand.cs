using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace GUI.MVVMBase
{
    public class RelayCommand<T> : ICommand
    {
        // private fields
        readonly Action<T> _execute = null;
        readonly Predicate<T> _canExecute = null;

        public RelayCommand(Action<T> execute) : this(execute, null) { }

        public RelayCommand(Action<T> execute, Predicate<T> canExecute)
        {
            this._execute = execute ?? throw new ArgumentNullException("execute");
            this._canExecute = canExecute;
        }

        // CommandManager.RequerySuggested event is raised whenever the CommandManager thinks that something has changed
        // and that will affect the ability of commands to execute. For example this might be a change of focus.
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute?.Invoke((T)parameter) ?? true;
        }

        public void Execute(object parameter)
        {
            _execute((T)parameter);
        }
    }
}
