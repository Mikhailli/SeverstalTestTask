using System.Collections.ObjectModel;
using System.Text;
using WpfOrderManagementSystem.Infrastructure;
using WpfOrderManagementSystem.Models;
using WpfOrderManagementSystem.Models.ApiRequestModels;
using WpfOrderManagementSystem.Services.Interfaces;
using WpfOrderManagementSystem.ViewModels.ProductsEditor;

namespace WpfOrderManagementSystem.ViewModels.OrdersEditor;

internal class AddOrderViewModel(IServiceForOrderManagement orderService, IProductServiceForInformation productService) : EditorPanelViewModelBase<Order>
{
    private readonly IServiceForOrderManagement _orderService = orderService;
    private readonly IProductServiceForInformation _productService = productService;
    private OrderItemViewModel _newOrderItemViewModel = null!;
    private ObservableCollection<ProductItemViewModel> _productItems = null!;

    public OrderItemViewModel NewOrderItemViewModel
    {
        get => _newOrderItemViewModel;
        set
        {
            _newOrderItemViewModel = value;
            RaisePropertyChanged(nameof(NewOrderItemViewModel));
        }
    }

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

    public DataBaseEditorViewModelBase ParentViewModel { get; set; } = null!;

    public async void Init()
    {
        NewOrderItemViewModel = new OrderItemViewModel();
        ProductItems = new ObservableCollection<ProductItemViewModel>();
        var products = await _productService.GetAllAsync();
        foreach (var product in products)
        {
            ProductItems.Add(new ProductItemViewModel(product));
        }
    }

    protected async override void Save(object? obj)
    {
        if (ValidateItem() is false)
        {
            return;
        }

        try
        {
            var addedOrder = await _orderService.AddOrderAsync(new OrderParameters
            {
                OrderDate = NewOrderItemViewModel.OrderDate,
                StorageUntil = NewOrderItemViewModel.StorageUntil,
                Status = NewOrderItemViewModel.Status,
                CustomerName = NewOrderItemViewModel.CustomerName,
                PhoneNumber = NewOrderItemViewModel.PhoneNumber
            });
            NewOrderItemViewModel = new OrderItemViewModel(addedOrder);

            ClosePanel(EditorPanelResult.Success, addedOrder);
            ErrorMessage = null;
            HasErrors = false;
        }
        catch (Exception ex)
        {
            ShowErrorMessage(ex.Message);
        }
    }

    private bool ValidateItem()
    {
        var validationResult = NewOrderItemViewModel.Validate();
        if (validationResult is not null)
        {
            var errorMessageBuilder = new StringBuilder();
            foreach (var result in validationResult)
            {
                errorMessageBuilder.AppendLine(result.ErrorMessage);
            }

            ShowErrorMessage(errorMessageBuilder.ToString().TrimEnd('\n'));
            return false;
        }

        return true;
    }
}
