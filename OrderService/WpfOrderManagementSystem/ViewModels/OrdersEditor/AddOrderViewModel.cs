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

    private Command? _addProductToOrder;
    private Command? _deleteProductFromOrder;

    private OrderItemViewModel _newOrderItemViewModel = null!;
    private ProductItemViewModel? _productToDelete;
    private ProductItemViewModel? _productToAdd;

    private int _storageDays;

    public int StorageDays
    {
        get => _storageDays;
        set
        {
            _storageDays = value;
            RaisePropertyChanged(nameof(StorageDays));
        }
    }

    public OrderItemViewModel NewOrderItemViewModel
    {
        get => _newOrderItemViewModel;
        set
        {
            _newOrderItemViewModel = value;
            RaisePropertyChanged(nameof(NewOrderItemViewModel));
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

    protected async override void Save(object? obj)
    {
        if (ValidateItem() is false)
        {
            return;
        }

        try
        {
            var now = DateTime.Now;
            var addedOrder = await _orderService.AddOrderAsync(new OrderParameters
            {
                OrderDate = now,
                StorageUntil = now.AddDays(_storageDays),
                Status = NewOrderItemViewModel.Status,
                CustomerName = NewOrderItemViewModel.CustomerName,
                PhoneNumber = NewOrderItemViewModel.PhoneNumber
            });

            if (NewOrderItemViewModel.OrderItems.Any())
            {
                await _orderService.ChangeOrderItemsAsync(addedOrder.Id, NewOrderItemViewModel.OrderItems.Select(item => new OrderItemParameters() { ProductId = item.Id, Quantity = item.Quantity }).ToList());
            }

            addedOrder.Items = [];
            foreach (var orderItem in NewOrderItemViewModel.OrderItems)
            {
                addedOrder.Items.Add(new OrderItem()
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

    protected bool CanExecuteAddProductToOrder(object? obj)
    {
        return ProductToAdd != null;
    }

    protected void AddProductToOrder(object? obj)
    {
        NewOrderItemViewModel.OrderItems.Add(new ProductItemViewModel(ProductToAdd!));
    }

    protected bool CanExecuteDeleteProductFromOrder(object? obj)
    {
        return ProductToDelete != null;
    }

    protected void DeleteProductFromOrder(object? obj)
    {
        NewOrderItemViewModel.OrderItems.Remove(ProductToDelete!);
    }
}
