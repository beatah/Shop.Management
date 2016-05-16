using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Shop.Management.App.Message;
using Shop.Management.App.Services;
using Shop.Management.App.Utility;
using Shop.Management.Model;

namespace Shop.Management.App.ViewModel
{
    public class LoginViewModel
    {
        private readonly EmployeeDataService _employeeDataService = new EmployeeDataService();
        private readonly List<Employee> _employees;
        private readonly DialogService _dialogService;
        private string _password;
        private string _username;

        public Action CloseAction { get; set; }

        public LoginViewModel()
        {
            _employees = _employeeDataService.GetAll();
            LoginCommand = new RelayCommand(Login, CanLogin);
            _dialogService=new DialogService();
            
        }

        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged("Username");
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged("Password");
            }
        }


        public ICommand LoginCommand { get; set; }

        private void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void Login(object obj)
        {
            var accounts = new List<Employee>(_employees)
                .Where(e => e.Username == _username)
                .Where(e => e.Password == Password);

            if (accounts.Count() == 1)
            {
                App.LoggedInEmployee = accounts.FirstOrDefault();
                _dialogService.ShowMainWindow();
                CloseAction();
            }
            else
            {
                MessageBox.Show("You have entered an invalid username or password!");
            }
        }

        private bool CanLogin(object obj)
        {
            return !string.IsNullOrEmpty(_username) && !string.IsNullOrEmpty(_password);
        }
    }
}