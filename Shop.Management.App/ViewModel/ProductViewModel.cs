using System.ComponentModel;
using System.Windows.Input;
using Shop.Management.App.Message;
using Shop.Management.App.Services;
using Shop.Management.App.Utility;
using Shop.Management.Model;

namespace Shop.Management.App.ViewModel
{
    public class ProductViewModel
    {
        private readonly ProductDataService _productDataService;
        private Product _selectedProduct;

        public ProductViewModel()
        {
            Messenger.Default.Register<Product>(this, OnProductReceived);
            SaveCommand = new RelayCommand(SaveProduct, CanSaveProduct);
            _productDataService = new ProductDataService();
        }

        public ICommand SaveCommand { get; set; }

        public Product SelectedProduct
        {
            get { return _selectedProduct; }
            set
            {
                if (value != _selectedProduct)
                {
                    _selectedProduct = value;
                    OnPropertyChanged("SelectedProduct");
                }
            }
        }

        private void SaveProduct(object obj)
        {
            _productDataService.SaveProduct(_selectedProduct);
            Messenger.Default.Send(new UpdateListMessage());
        }

        private bool CanSaveProduct(object obj)
        {
            return true;
        }

        private void OnProductReceived(Product product)
        {
            SelectedProduct = product;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}