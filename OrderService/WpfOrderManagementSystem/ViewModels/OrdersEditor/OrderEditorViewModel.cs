using System.Collections.ObjectModel;
using WpfOrderManagementSystem.Infrastructure;
using WpfOrderManagementSystem.Models;
using WpfOrderManagementSystem.Services.Interfaces;

namespace WpfOrderManagementSystem.ViewModels.OrdersEditor;

internal class OrderEditorViewModel : DataBaseEditorViewModelBase
{
    private readonly IOrderServiceForInformation _orderServiceForInformation;
    private readonly AddOrderViewModel _addOrderViewModel;
    private readonly EditOrderViewModel _editOrderViewModel;
    private ObservableCollection<OrderItemViewModel> _orderItems = null!;
    private OrderItemViewModel? _selectedOrderItem;
    private EditorPanelViewModelBase<Order> _editorPanelViewModel = null!;
    private bool _isDeletePanelVisible;

    public ObservableCollection<OrderItemViewModel> OrderItems
    {
        get => _orderItems;
        set
        {
            if (_orderItems != value)
            {
                _orderItems = value;
                RaisePropertyChanged(nameof(OrderItems));
            }
        }
    }

    public OrderItemViewModel? SelectedOrderItem
    {
        get => _selectedOrderItem;
        set
        {
            if (_selectedOrderItem != value)
            {
                _selectedOrderItem = value;
                RaisePropertyChanged(nameof(SelectedOrderItem));
                RaiseCommandCanExecuteChanged(ShowEditItemPanelCmd);
                RaiseCommandCanExecuteChanged(ShowDeleteItemPanelCmd);
            }
        }
    }

    public EditorPanelViewModelBase<Order> EditorPanelViewModel
    {
        get => _editorPanelViewModel;
        set
        {
            if (_editorPanelViewModel != value)
            {
                _editorPanelViewModel = value;
                RaisePropertyChanged(nameof(EditorPanelViewModel));
            }
        }
    }

    public DeleteOrderViewModel DeleteOrderViewModel { get; set; }

    public bool IsDeletePanelVisible
    {
        get => _isDeletePanelVisible;
        set
        {
            if (_isDeletePanelVisible != value)
            {
                _isDeletePanelVisible = value;
                RaisePropertyChanged(nameof(IsDeletePanelVisible));
            }
        }
    }

    public OrderEditorViewModel(IOrderServiceForInformation orderServiceForInformation, 
        DeleteOrderViewModel deleteOrderViewModel, AddOrderViewModel addOrderViewModel, EditOrderViewModel editOrderViewModel) : base("Заказы")
    {
        _orderServiceForInformation = orderServiceForInformation;

        _addOrderViewModel = addOrderViewModel;
        _addOrderViewModel.ParentViewModel = this;
        _addOrderViewModel.EditorPanelClosed += AddOrderViewModelOnEditorPanelClosed;

        _editOrderViewModel = editOrderViewModel;
        _editOrderViewModel.EditorPanelClosed += EditOrderViewModelOnEditorPanelClosed;

        DeleteOrderViewModel = deleteOrderViewModel;
        DeleteOrderViewModel.EditorPanelClosed += DeleteOrderViewModelOnEditorPanelClosed;
    }

    public override async void Update()
    {
        var orders = await _orderServiceForInformation.GetAllAsync();
        OrderItems = new ObservableCollection<OrderItemViewModel>(orders.Select(order => new OrderItemViewModel(order)));
    }

    protected override void RefreshData(object? obj)
    {
        Update();
    }

    protected override void ShowAddItemPanel(object? obj)
    {
        _addOrderViewModel.Init();
        EditorPanelViewModel = _addOrderViewModel;
        base.ShowAddItemPanel(obj);
    }

    protected override void ShowDeleteItemPanel(object? obj)
    {
        DeleteOrderViewModel.Init(SelectedOrderItem!);
        IsDeletePanelVisible = true;
    }

    protected override bool CanExecuteShowDeleteItemPanelCmd(object? obj)
    {
        return SelectedOrderItem is not null;
    }

    protected override void ShowEditItemPanel(object? obj)
    {
        _editOrderViewModel.Init(SelectedOrderItem!);
        EditorPanelViewModel = _editOrderViewModel;
        base.ShowEditItemPanel(obj);
    }

    protected override bool CanExecuteShowEditItemPanelCmd(object? obj)
    {
        return SelectedOrderItem is not null;
    }

    protected override bool CanExecuteEditorCommands(object? obj)
    {
        return IsEditorPanelVisible is false && IsDeletePanelVisible is false;
    }

    private void AddOrderViewModelOnEditorPanelClosed(object? sender, EditorPanelClosedEventArgs<Order> editorPanelClosedEventArgs)
    {
        if (editorPanelClosedEventArgs.ResultType == EditorPanelResult.Success)
        {
            OrderItems.Add(new OrderItemViewModel(editorPanelClosedEventArgs.EditedModel));
        }

        IsEditorPanelVisible = false;
    }

    private void EditOrderViewModelOnEditorPanelClosed(object? sender, EditorPanelClosedEventArgs<Order> editorPanelClosedEventArgs)
    {
        if (editorPanelClosedEventArgs.ResultType == EditorPanelResult.Success)
        {
            var orderToReplace = OrderItems.FirstOrDefault(order => order.Id == editorPanelClosedEventArgs.EditedModel.Id);
            if (orderToReplace is not null)
            {
                var index = OrderItems.IndexOf(orderToReplace);
                OrderItems.RemoveAt(index);
                OrderItems.Insert(index, new OrderItemViewModel(editorPanelClosedEventArgs.EditedModel));
            }
        }

        IsEditorPanelVisible = false;
    }

    private void DeleteOrderViewModelOnEditorPanelClosed(object? sender, EditorPanelClosedEventArgs<Order> editorPanelClosedEventArgs)
    {
        if (editorPanelClosedEventArgs.ResultType == EditorPanelResult.Success)
        {
            OrderItems.Remove(DeleteOrderViewModel.DeletedOrderItem);
        }

        IsDeletePanelVisible = false;
    }
}
