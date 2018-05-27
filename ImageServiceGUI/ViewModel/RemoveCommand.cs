using System;
using System.Windows.Input;
/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public class RemoveCommand<T> : ICommand
{
    public delegate void ExecuteMethod<T>(object obj);
    public delegate bool CanExecuteMethod<T>(object obj);

    private ExecuteMethod<T> executeRemove;
    private CanExecuteMethod<T> canExecuteRemove;

    public RemoveCommand(ExecuteMethod<T> executeRemove, CanExecuteMethod<T> canExecuteRemove)
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
    public void RaiseCanExecuteChange()
    {
        this.CanExecuteChanged?.Invoke(this, new EventArgs());
    }

    public event EventHandler CanExecuteChanged;
}
