using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Shop.Management.App.Extensions;
using Shop.Management.App.Message;
using Shop.Management.App.Services;
using Shop.Management.App.Utility;
using Shop.Management.Model;

namespace Shop.Management.App.ViewModel
{
    public class ProductsViewModel
    {
        private readonly DialogService _dialogService = new DialogService();
        private readonly ProductDataService _productDataService;
        private ObservableCollection<Product> _products;
        private Product _selectedProduct;

        public ProductsViewModel()
        {
            _productDataService = new ProductDataService();
            LoadData();
            AddCommand = new RelayCommand(AddProduct, CanAddProduct);
            EditCommand = new RelayCommand(EditProduct, CanEditProduct);
            Messenger.Default.Register<UpdateListMessage>(this, OnUpdateListMessageReceived);
        }

        public Product SelectedProduct
        {
            get { return _selectedProduct; }
            set
            {
                if (_selectedProduct != value)
                {
                    _selectedProduct = value;
                    OnPropertyChanged("SelectedProduct");
                }
            }
        }

        public ICommand EditCommand { get; set; }
        public ICommand AddCommand { get; set; }

        public ObservableCollection<Product> Products
        {
            get { return _products; }
            set
            {
                _products = value;
                OnPropertyChanged("Products");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void EditProduct(object obj)
        {
            Messenger.Default.Send(_selectedProduct);
            _dialogService.ShowProductDialog();
        }

        private bool CanEditProduct(object obj)
        {
            return SelectedProduct != null;
        }

        private bool CanAddProduct(object obj)
        {
            return true;
        }

        private void OnUpdateListMessageReceived(UpdateListMessage obj)
        {
            LoadData();
            _dialogService.CloseProductDialog();
        }

        private void AddProduct(object obj)
        {
            SelectedProduct = new Product();
            Messenger.Default.Send(_selectedProduct);
            _dialogService.ShowProductDialog();
        }

        private void LoadData()
        {
            Products = _productDataService.GetAllProducts().ToObservableCollection();
        }

        private void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}