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
    public class EmployeeOverviewViewModel : INotifyPropertyChanged
    {
        private readonly DialogService _dialogService = new DialogService();
        private readonly EmployeeDataService _employeeDataService;
        private ObservableCollection<Employee> _employees;

        private Employee _selectedEmployee;


        public EmployeeOverviewViewModel()
        {
            _employeeDataService = new EmployeeDataService();
            LoadData();
            EditCommand = new RelayCommand(EditEmployee, CanEditEmployee);
            AddCommand = new RelayCommand(AddEmployee, CanAddEmployee);
            DeleteCommand = new RelayCommand(DeleteEmployee, CanDeleteEmployee);

            Messenger.Default.Register<UpdateListMessage>(this, OnUpdateListMessageReceived);
        }

        public ObservableCollection<Employee> Employees
        {
            get { return _employees; }
            set
            {
                if (_employees != value)
                {
                    _employees = value;
                    OnPropertyChanged("Employees");
                }
            }
        }

        public Employee SelectedEmployee
        {
            get { return _selectedEmployee; }
            set
            {
                if (_selectedEmployee != value)
                {
                    _selectedEmployee = value;
                    OnPropertyChanged("SelectedEmployee");
                }
            }
        }


        public ICommand EditCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnUpdateListMessageReceived(UpdateListMessage obj)
        {
            LoadData();
            _dialogService.CloseDetailEmployeeDialog();
        }

        private void LoadData()
        {
            Employees = _employeeDataService.GetAllEmployees().ToObservableCollection();
        }

        private void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private bool CanEditEmployee(object obj)
        {
            if (SelectedEmployee != null)
                return true;
            return false;
        }

        private void EditEmployee(object obj)
        {
            Messenger.Default.Send(_selectedEmployee);
            _dialogService.ShowDetailEmployeeDialog();
        }

        private bool CanAddEmployee(object obj)
        {
            return true;
        }

        private void AddEmployee(object obj)
        {
            SelectedEmployee = new Employee();
            Messenger.Default.Send(_selectedEmployee);
            _dialogService.ShowDetailEmployeeDialog();
        }

        private bool CanDeleteEmployee(object obj)
        {
            if (SelectedEmployee != null)
                return true;
            return false;
        }

        private void DeleteEmployee(object obj)
        {
            _employeeDataService.DeleteEmployee(_selectedEmployee);
            LoadData();
        }
    }
}