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
    public class CustomersViewModel : INotifyPropertyChanged
    {
        private readonly CustomerDataService _customerDataService;
        private readonly DialogService _dialogService = new DialogService();
        private ObservableCollection<Customer> _customers;
        private Customer _selectedCustomer;

        public CustomersViewModel()
        {
            _customerDataService = new CustomerDataService();
            AddCommand = new RelayCommand(AddCustomer, CanAddCustomer);
            EditCommand = new RelayCommand(EditCustomer, CanEditCustomer);
            LoadData();
            Messenger.Default.Register<UpdateListMessage>(this, OnUpdateListMessageReceived);
        }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }

        public ObservableCollection<Customer> Customers
        {
            get { return _customers; }
            set
            {
                if (_customers != value)
                {
                    _customers = value;
                    OnPropertyChanged("Customers");
                }
            }
        }

        public Customer SelectedCustomer
        {
            get { return _selectedCustomer; }
            set
            {
                if (_selectedCustomer != value)
                {
                    _selectedCustomer = value;
                    OnPropertyChanged("SelectedCustomer");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private bool CanEditCustomer(object obj)
        {
            return SelectedCustomer != null;
        }

        private void EditCustomer(object obj)
        {
            Messenger.Default.Send(_selectedCustomer);
            _dialogService.ShowCustomerDialog();
        }

        private void AddCustomer(object obj)
        {
            _selectedCustomer = new Customer();
            Messenger.Default.Send(_selectedCustomer);
            _dialogService.ShowCustomerDialog();
        }

        private bool CanAddCustomer(object obj)
        {
            return true;
        }

        private void OnUpdateListMessageReceived(UpdateListMessage obj)
        {
            LoadData();
            _dialogService.CloseCustomerDialog();
        }


        private void LoadData()
        {
            Customers = _customerDataService.GetAllCustomersOrders().ToObservableCollection();
        }

        private void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}