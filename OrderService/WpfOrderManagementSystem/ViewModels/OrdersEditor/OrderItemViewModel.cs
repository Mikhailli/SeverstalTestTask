using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using WpfOrderManagementSystem.Infrastructure;
using WpfOrderManagementSystem.Models;
using WpfOrderManagementSystem.ViewModels.ProductsEditor;

namespace WpfOrderManagementSystem.ViewModels.OrdersEditor;

internal class OrderItemViewModel : ViewModelBase
{
    private DateTime _orderDate;
    private DateTime _storageUntil;
    private string _status = null!;
    private string? _customerName;
    private string? _phoneNumber;

    public int Id { get; }

    [Required]
    public DateTime OrderDate
    {
        get => _orderDate;
        set
        {
            if (_orderDate != value)
            {
                _orderDate = value;
                RaisePropertyChanged(nameof(OrderDate));
            }
        }
    }

    [Required]
    public DateTime StorageUntil
    {
        get => _storageUntil;
        set
        {
            if (_storageUntil != value)
            {
                _storageUntil = value;
                RaisePropertyChanged(nameof(StorageUntil));
            }
        }
    }

    [Required]
    public string Status
    {
        get => _status;
        set
        {
            if (_status != value)
            {
                _status = value;
                RaisePropertyChanged(nameof(Status));
            }
        }
    }

    [Required]
    public string? CustomerName
    {
        get => _customerName;
        set
        {
            if (_customerName != value)
            {
                _customerName = value;
                RaisePropertyChanged(nameof(CustomerName));
            }
        }
    }

    [Required]
    public string? PhoneNumber
    {
        get => _phoneNumber;
        set
        {
            if (_phoneNumber != value)
            {
                _phoneNumber = value;
                RaisePropertyChanged(nameof(PhoneNumber));
            }
        }
    }

    public ObservableCollection<ProductItemViewModel> OrderItems { get; set; }

    public OrderItemViewModel()
    {
        OrderItems = [];
    }

    public OrderItemViewModel(Order order)
    {
        Id = order.Id;
        OrderDate = order.OrderDate;
        StorageUntil = order.StorageUntil;
        Status = order.Status;
        CustomerName = order.CustomerName;
        PhoneNumber = order.PhoneNumber;
        OrderItems = [];
        foreach (var item in order.Items)
        {
            OrderItems.Add(new ProductItemViewModel(item.Product, item.Quantity));
        }
    }

    public OrderItemViewModel(OrderItemViewModel orderItemViewModel)
    {
        Id = orderItemViewModel.Id;
        OrderDate = orderItemViewModel.OrderDate;
        StorageUntil = orderItemViewModel.StorageUntil;
        Status = orderItemViewModel.Status;
        CustomerName = orderItemViewModel.CustomerName;
        PhoneNumber = orderItemViewModel.PhoneNumber;
        OrderItems = new ObservableCollection<ProductItemViewModel>(orderItemViewModel.OrderItems.Select(orderItem => new ProductItemViewModel(orderItem, orderItem.Quantity)));
    }

    public ICollection<ValidationResult>? Validate()
    {
        var context = new ValidationContext(this);
        var results = new List<ValidationResult>();

        if (Validator.TryValidateObject(this, context, results, true) is false)
        {
            return results;
        }

        return null;
    }
}
