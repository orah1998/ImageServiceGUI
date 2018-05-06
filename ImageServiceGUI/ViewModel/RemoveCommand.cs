using System;
using System.Windows.Input;

public class RemoveCommand<T> : ICommand
{
    public delegate void ExcuteMethod<T>(object obj);
    public delegate bool CanExecuteMethod<T>(object obj);

    private ExcuteMethod<T> executeRemove;
    private CanExecuteMethod<T> canExecuteRemove;

    public RemoveCommand(ExcuteMethod<T> executeRemove, CanExecuteMethod<T> canExecuteRemove)
    {
        this.executeRemove = executeRemove;
        this.canExecuteRemove = canExecuteRemove;
    }

    public void Execute(object parameter)
    {
        this.executeRemove(parameter);

    }

    public bool CanExecute(object parameter)
    {
        return this.canExecuteRemove(parameter);

    }


    public event EventHandler CanExecuteChanged;
}
