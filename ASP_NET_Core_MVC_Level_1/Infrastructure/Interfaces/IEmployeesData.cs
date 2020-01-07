using System.Collections.Generic;
using ASP_NET_Core_MVC.ViewModels;

namespace ASP_NET_Core_MVC.Infrastructure.Interfaces
{
    public interface IEmployeesData
    {
        IEnumerable<EmployeeViewModel> GetAll();

        EmployeeViewModel GetById(int id);

        void Add(EmployeeViewModel employee);

        void Edit(int id, EmployeeViewModel employee);

        bool Delete(int id);

        bool SaveChanges();
    }
}
