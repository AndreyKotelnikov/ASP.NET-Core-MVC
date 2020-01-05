using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASP_NET_Core_MVC.ViewModels;

namespace ASP_NET_Core_MVC.Infrastructure.Interfaces
{
    public interface IEmployeesData
    {
        IEnumerable<EmployeeView> GetAll();

        EmployeeView GetById(int id);

        void Add(EmployeeView employee);

        void Edit(int id, EmployeeView employee);

        bool Delete(int id);

        bool SaveChanges();
    }
}
