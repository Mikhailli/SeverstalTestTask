using System.Collections.ObjectModel;
using WpfOrderManagementSystem.Services.Interfaces;

namespace WpfOrderManagementSystem.ViewModels.ProductsEditor;

internal class ProductEditorViewModel : DataBaseEditorViewModelBase
{
    private readonly IProductServiceForInformation _productService;
    private ObservableCollection<ProductItemViewModel> _productItems = null!;

    public ObservableCollection<ProductItemViewModel> ProductItems
    {
        get => _productItems;
        set
        {
            if (_productItems != value)
            {
                _productItems = value;
                RaisePropertyChanged(nameof(ProductItems));
            }
        }
    }

    public ProductEditorViewModel(IProductServiceForInformation productService) : base("Товары")
    {
        _productService = productService;
    }

    public override async void Update()
    {
        var products = await _productService.GetAllAsync();
        ProductItems = new ObservableCollection<ProductItemViewModel>(products.Select(product => new ProductItemViewModel(product)));
    }

    protected override void RefreshData(object? obj)
    {
        Update();
    }
}
