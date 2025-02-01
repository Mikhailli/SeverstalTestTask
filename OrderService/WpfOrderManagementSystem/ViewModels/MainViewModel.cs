using System.Collections.ObjectModel;
using WpfOrderManagementSystem.Infrastructure;
using WpfOrderManagementSystem.Models.Settings;
using WpfOrderManagementSystem.ViewModels.ProductsEditor;

namespace WpfOrderManagementSystem.ViewModels;

internal class MainViewModel : ViewModelBase
{
    private EditorViewModelBase? _selectedEditorViewModel;

    public ObservableCollection<EditorViewModelBase> EditorViewModels { get; }

    public EditorViewModelBase? SelectedEditorViewModel
    {
        get => _selectedEditorViewModel;
        set
        {
            if (_selectedEditorViewModel != value)
            {
                _selectedEditorViewModel = value;
                if (SelectedEditorViewModel is not null)
                {
                    _selectedEditorViewModel!.Update();
                }
                RaisePropertyChanged(nameof(SelectedEditorViewModel));
            }
        }
    }

    public MainViewModel(AppSettings appSettings, ProductEditorViewModel productsEditorViewModel)
    {
        EditorViewModels = new ObservableCollection<EditorViewModelBase>
        {
            productsEditorViewModel,
        };

        foreach (var editor in EditorViewModels)
        {
            editor.ParentViewModel = this;
        }

        SelectedEditorViewModel = EditorViewModels.FirstOrDefault();
    }
}
