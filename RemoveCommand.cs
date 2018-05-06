using System;
using System.Windows.Input;

public class RemoveCommand : ICommand
{
    public void Execute(object parameter)
    {
        if (selectedItem != null)
        {
            this.lbHandlers.Remove(selectedItem);
        }

    }

    public bool CanExecute(object parameter)
    {
        return true;
    }


    public event EventHandler CanExecuteChanged;
}
