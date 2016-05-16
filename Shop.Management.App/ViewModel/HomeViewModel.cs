using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using NHibernate;
using NHibernate.Linq;
using Shop.Management.App.Extensions;
using Shop.Management.App.Message;
using Shop.Management.App.Services;
using Shop.Management.App.Utility;
using Shop.Management.DAL;
using Shop.Management.Model;

namespace Shop.Management.App.ViewModel
{
   public class HomeViewModel : INotifyPropertyChanged
   {
        private readonly CustomerDataService _customerDataService;
        private readonly EmployeeDataService _employeeDataService;
        private readonly ProductDataService _productDataService;
        public ICommand LoadSampleDataCommand { get; set; }

        public HomeViewModel()
        {
            _customerDataService = new CustomerDataService();
            _employeeDataService = new EmployeeDataService();
            _productDataService =new ProductDataService();
            LoadSampleDataCommand = new RelayCommand(LoadSampleData, CanLoadSampleData);
        }

        private bool CanLoadSampleData(object obj)
       {
           return true;
       }

       private void LoadSampleData(object obj)
       {
            Employee e = new Employee()
            {
                Email = "r.weber@mail.com",
                Username = "test",
                Password = "test",
                FirstName = "Rose",
                HireDate = new DateTime(2014, 11, 05),
                LastName = "Weber",
                Phone = "123456789"
            };
            Customer c = new Customer()
            {
                City = "Arlington",
                Country = "United States",
                Email = "HarveyMNorvell@mail.com",
                FirstName = "Harvey",
                LastName = "Norvell",
                Phone = "123456787",
                PostalCode = 76010,
                Street = "2586 Oliver Street"
            };
            Product p = new Product()
            {
                Name = "SanDisk Extreme 64GB MicroSDXC",
                Price = 26.99M,
                InStock = 200
            };

            Product p2 = new Product()
            {
                Name = "Amazon Fire TV",
                Price = 99.99M,
                InStock = 100
            };
            Product p3 = new Product()
            {
                Name = "Logitech Bluetooth Audio Adapter",
                Price = 27.49M,
                InStock = 50
            };
            _customerDataService.SaveCustomer(c);
            _productDataService.SaveProduct(p);
            _productDataService.SaveProduct(p2);
            _productDataService.SaveProduct(p3);
            _employeeDataService.SaveEmployee(e);
            Messenger.Default.Send(new UpdateListMessage());
        }

        private void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
