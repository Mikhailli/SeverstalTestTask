using System.Collections.ObjectModel;
using System.Text;
using WpfOrderManagementSystem.Infrastructure;
using WpfOrderManagementSystem.Models;
using WpfOrderManagementSystem.Models.ApiRequestModels;
using WpfOrderManagementSystem.Services.Interfaces;
using WpfOrderManagementSystem.ViewModels.ProductsEditor;

namespace WpfOrderManagementSystem.ViewModels.OrdersEditor;

internal class EditOrderViewModel(IServiceForOrderManagement orderService, 
    IProductServiceForInformation productServiceForInformation, IOrderServiceForInformation orderServiceForInformation) : EditorPanelViewModelBase<Order>
{
    private readonly IServiceForOrderManagement _orderService = orderService;
    private readonly IProductServiceForInformation _productServiceForInformation = productServiceForInformation;
    private readonly IOrderServiceForInformation _orderServiceForInformation = orderServiceForInformation;

    private Command? _addProductToOrder;
    private Command? _deleteProductFromOrder;

    private OrderItemViewModel _editedOrderItemViewModel = null!;
    private ProductItemViewModel? _productToDelete;
    private ProductItemViewModel? _productToAdd;

    public OrderItemViewModel EditedOrderItemViewModel
    {
        get => _editedOrderItemViewModel;
        set
        {
            _editedOrderItemViewModel = value;
            RaisePropertyChanged(nameof(EditedOrderItemViewModel));
        }
    }

    public ProductItemViewModel? ProductToDelete
    {
        get => _productToDelete;
        set
        {
            if (_productToDelete != value)
            {
                _productToDelete = value;
                RaisePropertyChanged(nameof(ProductToDelete));
                RaiseCommandCanExecuteChanged(DeleteProductFromOrderCmd);
            }
        }
    }

    public ProductItemViewModel? ProductToAdd
    {
        get => _productToAdd;
        set
        {
            if (_productToAdd != value)
            {
                _productToAdd = value;
                RaisePropertyChanged(nameof(ProductToAdd));
                RaiseCommandCanExecuteChanged(AddProductToOrderCmd);
            }
        }
    }

    public ObservableCollection<ProductItemViewModel> ProductItems { get; set; } = [];

    public async void Init(OrderItemViewModel orderItemViewModel)
    {
        ProductItems = [];
        var products = await _productServiceForInformation.GetAllAsync();
        foreach (var product in products)
        {
            ProductItems.Add(new ProductItemViewModel(product));
        }
        EditedOrderItemViewModel = new OrderItemViewModel(orderItemViewModel);
    }

    public Command AddProductToOrderCmd
    {
        get
        {
            if (_addProductToOrder is null)
                _addProductToOrder = new Command(AddProductToOrder, CanExecuteAddProductToOrder);
            return _addProductToOrder;
        }
    }

    public Command DeleteProductFromOrderCmd
    {
        get
        {
            if (_deleteProductFromOrder is null)
                _deleteProductFromOrder = new Command(DeleteProductFromOrder, CanExecuteDeleteProductFromOrder);
            return _deleteProductFromOrder;
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
            
            if (orderToUpdate.Items.Equals(EditedOrderItemViewModel.OrderItems) is false)
            {
                await _orderService.ChangeOrderItemsAsync(orderToUpdate.Id, EditedOrderItemViewModel.OrderItems.Select(item => new OrderItemParameters() { ProductId = item.Id, Quantity = item.Quantity}).ToList());
            }

            var updatedOrder = await _orderServiceForInformation.GetAsync(EditedOrderItemViewModel.Id);
            updatedOrder.Items.Clear();
            foreach (var orderItem in EditedOrderItemViewModel.OrderItems)
            {
                updatedOrder.Items.Add(new OrderItem() 
                { 
                    Product = new Product()
                    {
                        Id = orderItem.Id,
                        Article = orderItem.Article,
                        Name = orderItem.Name,
                        Description = orderItem.Description,
                        Price = orderItem.Price,
                        StockQuantity = orderItem.StockQuantity
                    },
                    Quantity = orderItem.Quantity
                });
            }
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

    protected bool CanExecuteAddProductToOrder(object? obj)
    {
        return ProductToAdd != null;
    }

    protected void AddProductToOrder(object? obj)
    {
        EditedOrderItemViewModel.OrderItems.Add(new ProductItemViewModel(ProductToAdd!));
    }

    protected bool CanExecuteDeleteProductFromOrder(object? obj)
    {
        return ProductToDelete != null;
    }

    protected void DeleteProductFromOrder(object? obj)
    {
        EditedOrderItemViewModel.OrderItems.Remove(ProductToDelete!);
    }
}
