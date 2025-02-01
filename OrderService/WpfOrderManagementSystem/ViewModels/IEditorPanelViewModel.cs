using WpfOrderManagementSystem.Infrastructure;

namespace WpfOrderManagementSystem.ViewModels;

internal interface IEditorPanelViewModel
{
    public bool HasErrors { get; }

    public string? ErrorMessage { get; }

    public Command SaveCmd { get; }

    public Command CloseEditorPanelCmd { get; }
}
