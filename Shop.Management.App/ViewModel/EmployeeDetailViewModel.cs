using System.ComponentModel;
using System.Windows.Input;
using Shop.Management.App.Message;
using Shop.Management.App.Services;
using Shop.Management.App.Utility;
using Shop.Management.Model;

namespace Shop.Management.App.ViewModel
{
    public class EmployeeDetailViewModel : INotifyPropertyChanged
    {
        private readonly EmployeeDataService _employeeDataService;
        private Employee _selectedEmployee;

        public EmployeeDetailViewModel()
        {
            Messenger.Default.Register<Employee>(this, OnEmployeeReceived);

            SaveCommand = new RelayCommand(SaveEmployee, CanSaveEmployee);
            DeleteCommand = new RelayCommand(DeleteEmployee, CanDeleteEmployee);

            _employeeDataService = new EmployeeDataService();
        }

        public ICommand SaveCommand { get; set; }
        public ICommand DeleteCommand { get; set; }


        public Employee SelectedEmployee
        {
            get { return _selectedEmployee; }
            set
            {
                if (value != _selectedEmployee)
                {
                    _selectedEmployee = value;
                    OnPropertyChanged("SelectedEmployee");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private bool CanDeleteEmployee(object obj)
        {
            return true;
        }

        private void DeleteEmployee(object obj)
        {
            _employeeDataService.DeleteEmployee(_selectedEmployee);
            Messenger.Default.Send(new UpdateListMessage());
        }

        private bool CanSaveEmployee(object obj)
        {
            return true;
        }

        private void SaveEmployee(object obj)
        {
            _employeeDataService.SaveEmployee(_selectedEmployee);
            Messenger.Default.Send(new UpdateListMessage());
        }

        private void OnEmployeeReceived(Employee employee)
        {
            SelectedEmployee = employee;
        }
    }
}