using System.ComponentModel;
using System.Windows.Input;
using Shop.Management.App.Message;
using Shop.Management.App.Services;
using Shop.Management.App.Utility;
using Shop.Management.Model;

namespace Shop.Management.App.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly DialogService _dialogService;

        private object _selectedViewModel;

        public MainWindowViewModel()
        {
            NavigateCommand = new RelayCommand(Navigate, CanNavigate);
            _dialogService = new DialogService();
            Messenger.Default.Register<LoginMessage>(this, OnLoginMessageReceived);
            _dialogService.ShowLogin();
        }

        public string LoggedInEmployee
        {
            get { return App.LoggedInEmployee != null ? App.LoggedInEmployee.FullName : ""; }
        }

        public object SelectedViewModel
        {
            get { return _selectedViewModel; }
            set
            {
                _selectedViewModel = value;
                OnPropertyChanged("SelectedViewModel");
            }
        }

        public ICommand NavigateCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OnLoginMessageReceived(LoginMessage obj)
        {
            _dialogService.CloseLoginDialog();
        }

        private bool CanNavigate(object obj)
        {
            return true;
        }

        private void Navigate(object parameter)
        {
            switch (parameter.ToString())
            {
                case "EmployeeOverviewViewModel":
                    SelectedViewModel = new EmployeeOverviewViewModel();
                    break;
                case "CustomersViewModel":
                    SelectedViewModel = new CustomersViewModel();
                    break;
                case "HomeViewModel":
                    SelectedViewModel = new HomeViewModel();
                    break;
                case "ProductsViewModel":
                    SelectedViewModel = new ProductsViewModel();
                    break;
                case "OrdersViewModel":
                    SelectedViewModel = new OrdersViewModel();
                    break;
            }
        }
    }
}