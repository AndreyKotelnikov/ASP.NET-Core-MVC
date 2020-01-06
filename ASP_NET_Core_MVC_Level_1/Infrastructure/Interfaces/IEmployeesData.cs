using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
