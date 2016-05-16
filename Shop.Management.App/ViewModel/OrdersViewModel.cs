using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using Shop.Management.App.Extensions;
using Shop.Management.App.Message;
using Shop.Management.App.Services;
using Shop.Management.App.Utility;
using Shop.Management.Model;

namespace Shop.Management.App.ViewModel
{
    public class OrdersViewModel : INotifyPropertyChanged
    {
        private readonly CustomerDataService _customerDataService;
        private readonly OrderDataService _orderDataService;
        private readonly ProductDataService _productDataService;
        private ObservableCollection<Customer> _customers;
        private ObservableCollection<OrderItem> _orderItems;

        private ObservableCollection<Order> _orders;
        private ObservableCollection<Product> _products;


        private Order _selectedOrder;


        private OrderItem _selectedOrderItem;

        public OrdersViewModel()
        {
            ClearOrder();
            _orderDataService = new OrderDataService();
            _customerDataService = new CustomerDataService();
            _productDataService = new ProductDataService();
            AddItemCommand = new RelayCommand(AddItem, CanAddItem);
            AddOrderCommand = new RelayCommand(AddOrder, CanAddOrder);
            LoadData();
            Messenger.Default.Register<UpdateListMessage>(this, OnUpdateListMessageReceived);
        }

        public ICommand AddItemCommand { get; set; }
        public ICommand AddOrderCommand { get; set; }

        public ObservableCollection<Order> Orders
        {
            get { return _orders; }
            set
            {
                if (_orders != value)
                {
                    _orders = value;
                    OnPropertyChanged("Orders");
                }
            }
        }


        public ObservableCollection<Customer> Customers
        {
            get { return _customers; }
            set
            {
                _customers = value;
                OnPropertyChanged("Customers");
            }
        }

        public ObservableCollection<OrderItem> OrderItems
        {
            get { return _orderItems; }
            set
            {
                _orderItems = value;
                OnPropertyChanged("OrderItems");
            }
        }

        public ObservableCollection<Product> Products
        {
            get { return _products; }
            set
            {
                _products = value;
                OnPropertyChanged("Products");
            }
        }

        public OrderItem SelectedOrderItem
        {
            get { return _selectedOrderItem; }
            set
            {
                if (_selectedOrderItem != value)
                {
                    _selectedOrderItem = value;
                    OnPropertyChanged("SelectedOrderItem");
                }
            }
        }

        public Order SelectedOrder
        {
            get { return _selectedOrder; }
            set
            {
                if (_selectedOrder != value)
                {
                    _selectedOrder = value;
                    OnPropertyChanged("SelectedOrder");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void LoadData()
        {
            Orders = _orderDataService.GetAllOrders().ToObservableCollection();
            Customers = _customerDataService.GetAll().ToObservableCollection();
            Products = _productDataService.GetAllProducts().ToObservableCollection();
        }

        private void AddOrder(object obj)
        {
            _orderDataService.SaveOrder(_selectedOrder);
            Messenger.Default.Send(new UpdateListMessage());
            ClearOrder();
        }

        private void ClearOrder()
        {
            _selectedOrder = new Order();
            _selectedOrder.Employee = App.LoggedInEmployee;
            _selectedOrderItem = new OrderItem();
            _orderItems = new ObservableCollection<OrderItem>();
            OnPropertyChanged("OrderItems");
        }

        private void AddItem(object obj)
        {
            if (_selectedOrderItem.Product.InStock >= _selectedOrderItem.Quantity)
            {
                _selectedOrder.AddItem(_selectedOrderItem.Product, _selectedOrderItem.Quantity);
                _orderItems.Add(_selectedOrderItem);
                _selectedOrderItem = new OrderItem();
                OnPropertyChanged("SelectedOrderItem");
            }
            else
            {
                MessageBox.Show("Product amount in stock LOW");
            }
        }

        private bool CanAddOrder(object obj)
        {
            return _selectedOrder.Items.Count > 0 && _selectedOrder.Customer != null;
        }

        private bool CanAddItem(object obj)
        {
            return true;
        }

        private void OnUpdateListMessageReceived(UpdateListMessage obj)
        {
            LoadData();
        }

        private void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}