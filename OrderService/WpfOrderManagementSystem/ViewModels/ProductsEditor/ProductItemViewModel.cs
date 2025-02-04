using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using WpfOrderManagementSystem.Infrastructure;
using WpfOrderManagementSystem.Models;

namespace WpfOrderManagementSystem.ViewModels.ProductsEditor;

internal class ProductItemViewModel : ViewModelBase
{
    private string _name = null!;
    private string _article = null!;
    private string? _description;
    private decimal _price;
    private int _stockQuantity;
    private int _quanitity;
    private bool _isSelected;

    public int Id { get; }

    [Required]
    public string Name
    {
        get => _name;
        set
        {
            if (_name != value)
            {
                _name = value;
                RaisePropertyChanged(nameof(Name));
            }
        }
    }

    [Required]
    public string Article
    {
        get => _article;
        set
        {
            if (_article != value)
            {
                _article = value;
                RaisePropertyChanged(nameof(Article));
            }
        }
    }

    public string? Description
    {
        get => _description;
        set
        {
            if (_description != value)
            {
                _description = value;
                RaisePropertyChanged(nameof(Description));
            }
        }
    }

    [Required]
    public decimal Price
    {
        get => _price;
        set
        {
            if (_price != value)
            {
                _price = value;
                RaisePropertyChanged(nameof(Price));
            }
        }
    }

    [Required]
    public int StockQuantity
    {
        get => _stockQuantity;
        set
        {
            if (_stockQuantity != value)
            {
                _stockQuantity = value;
                RaisePropertyChanged(nameof(StockQuantity));
            }
        }
    }

    public int Quantity
    {
        get => _quanitity;
        set
        {
            if (_quanitity != value)
            {
                _quanitity = value;
                RaisePropertyChanged(nameof(Quantity));
            }
        }
    }

    public bool IsSelected
    {
        get => _isSelected;
        set
        {
            if (_isSelected != value)
            {
                _isSelected = value;
                RaisePropertyChanged(nameof(IsSelected));
            }
        }
    }

    public ProductItemViewModel()
    {

    }

    public ProductItemViewModel(Product product, int quantity = 1)
    {
        Id = product.Id;
        Name = product.Name;
        Article = product.Article;
        Description = product.Description;
        Price = product.Price;
        StockQuantity = product.StockQuantity;
        IsSelected = false;
        Quantity = quantity;
    }

    public ProductItemViewModel(ProductItemViewModel productItemViewModel, int quantity = 1)
    {
        Id = productItemViewModel.Id;
        Name = productItemViewModel.Name;
        Article = productItemViewModel.Article;
        Description = productItemViewModel.Description;
        Price = productItemViewModel.Price;
        StockQuantity = productItemViewModel.StockQuantity;
        IsSelected = false;
        Quantity = quantity;
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
