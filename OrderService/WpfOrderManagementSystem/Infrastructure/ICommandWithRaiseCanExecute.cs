using System.Windows.Input;

namespace WpfOrderManagementSystem.Infrastructure;

internal interface ICommandWithRaiseCanExecute : ICommand
{
    void RaiseCanExecuteChanged();
}
