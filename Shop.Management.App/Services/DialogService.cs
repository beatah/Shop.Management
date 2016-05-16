using System.Windows;
using Shop.Management.App.View;

namespace Shop.Management.App.Services
{
    public class DialogService
    {
        private Window _customerView;
        private Window _employeeDetailView;
        private Window _loginView;
        private Window _productView;
        private Window _mainWindowView;

        public void ShowLogin()
        {
            _loginView = new LoginView();
            _loginView.ShowDialog();
        }

        public void CloseLoginDialog()
        {
            if (_loginView != null)
                _loginView.Close();
        }

        public void ShowDetailEmployeeDialog()
        {
            _employeeDetailView = new EmployeeDetailView();
            _employeeDetailView.ShowDialog();
        }

        public void CloseDetailEmployeeDialog()
        {
            if (_employeeDetailView != null)
                _employeeDetailView.Close();
        }

        public void ShowProductDialog()
        {
            _productView = new ProductView();
            _productView.ShowDialog();
        }

        public void CloseProductDialog()
        {
            if (_productView != null)
                _productView.Close();
        }

        public void ShowCustomerDialog()
        {
            _customerView = new CustomerView();
            _customerView.ShowDialog();
        }

        public void CloseCustomerDialog()
        {
            if (_customerView != null)
                _customerView.Close();
        }

        public void ShowMainWindow()
        {
            _mainWindowView=new MainWindow();
            _mainWindowView.Show();
        }
    }
}