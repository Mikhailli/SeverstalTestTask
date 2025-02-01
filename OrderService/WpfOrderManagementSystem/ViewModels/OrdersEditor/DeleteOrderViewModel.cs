using WpfOrderManagementSystem.Infrastructure;
using WpfOrderManagementSystem.Models;
using WpfOrderManagementSystem.Services.Interfaces;

namespace WpfOrderManagementSystem.ViewModels.OrdersEditor;

internal class DeleteOrderViewModel(IServiceForOrderManagement orderService) : EditorPanelViewModelBase<Order>
{
    private readonly IServiceForOrderManagement _orderService = orderService;
    private string _message = null!;

    public OrderItemViewModel DeletedOrderItem { get; set; } = null!;

    public string Message
    {
        get => _message;
        set
        {
            _message = value;
            RaisePropertyChanged(nameof(Message));
        }
    }

    public void Init(OrderItemViewModel orderItemViewModel)
    {
        DeletedOrderItem = orderItemViewModel;
        Message = $"Удалить заказ {DeletedOrderItem.Id}?";
    }

    protected override async void Save(object? obj)
    {
        try
        {
            await _orderService.DeleteOrderAsync(DeletedOrderItem.Id);
            ClosePanel(EditorPanelResult.Success, null);
        }
        catch (Exception ex)
        {
            ShowErrorMessage(ex.Message);
        }
    }
}
