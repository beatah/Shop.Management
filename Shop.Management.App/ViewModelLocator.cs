using Shop.Management.App.ViewModel;

namespace Shop.Management.App
{
    public class ViewModelLocator
    {
        public static EmployeeOverviewViewModel EmployeeOverviewViewModel { get; } = new EmployeeOverviewViewModel();
        public static EmployeeDetailViewModel EmployeeDetailViewModel { get; } = new EmployeeDetailViewModel();
        public static MainWindowViewModel MainWindowViewModel { get; } = new MainWindowViewModel();
        public static CustomersViewModel CustomersViewModel { get; } = new CustomersViewModel();
        public static CustomerViewModel CustomerViewModel { get; } = new CustomerViewModel();
        public static HomeViewModel HomeViewModel { get; } = new HomeViewModel();
        public static ProductsViewModel ProductsViewModel { get; } = new ProductsViewModel();
        public static ProductViewModel ProductViewModel { get; } = new ProductViewModel();
        public static OrdersViewModel OrdersViewModel { get; } = new OrdersViewModel();

    }
}