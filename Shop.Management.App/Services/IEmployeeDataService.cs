using System.Collections.Generic;
using Shop.Management.Model;

namespace Shop.Management.App.Services
{
    internal interface IEmployeeDataService
    {
        void SaveEmployee(Employee employee);
        void DeleteEmployee(Employee employee);
        List<Employee> GetAllEmployees();
    }
}