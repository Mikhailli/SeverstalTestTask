using WpfOrderManagementSystem.Infrastructure;

namespace WpfOrderManagementSystem.ViewModels;

internal abstract class EditorViewModelBase : ViewModelBase
{
    public string Name { get; } = null!;

    public MainViewModel ParentViewModel { get; set; } = null!;

    protected EditorViewModelBase(string name)
    {
        Name = name;
    }

    public abstract void Update();
}
