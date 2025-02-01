using System.Collections.ObjectModel;
using System.Text;
using WpfOrderManagementSystem.Infrastructure;
using WpfOrderManagementSystem.Models;
using WpfOrderManagementSystem.Services.Interfaces;
using WpfOrderManagementSystem.ViewModels.ProductsEditor;

namespace WpfOrderManagementSystem.ViewModels.OrdersEditor;

internal class EditOrderViewModel(IServiceForOrderManagement orderService, 
    IProductServiceForInformation productServiceForInformation, IOrderServiceForInformation orderServiceForInformation) : EditorPanelViewModelBase<Order>
{
    private readonly IServiceForOrderManagement _orderService = orderService;
    private readonly IProductServiceForInformation _productServiceForInformation = productServiceForInformation;
    private readonly IOrderServiceForInformation _orderServiceForInformation = orderServiceForInformation;

    private ProductItemViewModel? _selectedProduct;
    private OrderItemViewModel _editedOrderItemViewModel = null!;
    private ObservableCollection<ProductItemViewModel> _productItems = null!;

    public OrderItemViewModel EditedOrderItemViewModel
    {
        get => _editedOrderItemViewModel;
        set
        {
            _editedOrderItemViewModel = value;
            RaisePropertyChanged(nameof(EditedOrderItemViewModel));
        }
    }

    public ProductItemViewModel? SelectedProduct
    {
        get => _selectedProduct;
        set
        {
            if (_selectedProduct != value)
            {
                _selectedProduct = value;
                RaisePropertyChanged(nameof(SelectedProduct));
            }
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

    public async void Init(OrderItemViewModel orderItemViewModel)
    {
        EditedOrderItemViewModel = new OrderItemViewModel(orderItemViewModel);
        ProductItems = [];
        var products = await _productServiceForInformation.GetAllAsync();
        foreach (var product in products)
        {
            ProductItems.Add(new ProductItemViewModel(product));
        }
    }

    protected override async void Save(object? obj)
    {
        if (ValidateItem() is false)
        {
            return;
        }

        try
        {
            var orderToUpdate = await _orderServiceForInformation.GetAsync(EditedOrderItemViewModel.Id);
            if (orderToUpdate.Status != EditedOrderItemViewModel.Status)
            {
                await _orderService.ChangeStatusAsync(EditedOrderItemViewModel.Id, EditedOrderItemViewModel.Status);
            }
            
            if (orderToUpdate.Items.Equals(EditedOrderItemViewModel.Items) is false)
            {
                await _orderService.ChangeOrderItemsAsync(orderToUpdate.Id, EditedOrderItemViewModel.Items.Select(item => (item.Product.Id, item.Quantity)).ToList());
            }

            var updatedOrder = await _orderServiceForInformation.GetAsync(EditedOrderItemViewModel.Id);
            ClosePanel(EditorPanelResult.Success, updatedOrder);
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
        var validationResult = EditedOrderItemViewModel.Validate();
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
