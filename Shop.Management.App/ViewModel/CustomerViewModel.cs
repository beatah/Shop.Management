using System.ComponentModel;
using System.Windows.Input;
using Shop.Management.App.Message;
using Shop.Management.App.Services;
using Shop.Management.App.Utility;
using Shop.Management.Model;

namespace Shop.Management.App.ViewModel
{
    public class CustomerViewModel
    {
        private readonly CustomerDataService _customerDataService;
        private Customer _selectedCustomer;

        public CustomerViewModel()
        {
            _customerDataService = new CustomerDataService();
            Messenger.Default.Register<Customer>(this, OnCustomerReceived);
            SaveCommand = new RelayCommand(SaveCustomer, CanSaveCustomer);
        }

        public ICommand SaveCommand { get; set; }

        public Customer SelectedCustomer
        {
            get { return _selectedCustomer; }
            set
            {
                if (value != _selectedCustomer)
                {
                    _selectedCustomer = value;
                    OnPropertyChanged("SelectedCustomer");
                }
            }
        }

        private bool CanSaveCustomer(object obj)
        {
            return true;
        }

        private void SaveCustomer(object obj)
        {
            _customerDataService.SaveCustomer(_selectedCustomer);
            Messenger.Default.Send(new UpdateListMessage());
        }

        private void OnCustomerReceived(Customer customer)
        {
            SelectedCustomer = customer;
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