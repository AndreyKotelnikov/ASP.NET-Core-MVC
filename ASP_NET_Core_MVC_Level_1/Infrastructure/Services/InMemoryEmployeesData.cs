using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ASP_NET_Core_MVC.Infrastructure.Interfaces;
using ASP_NET_Core_MVC.ViewModels;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ASP_NET_Core_MVC.Infrastructure.Services
{
    public class InMemoryEmployeesData : IEmployeesData
    {
        private readonly List<EmployeeViewModel> _employeeViews = Enumerable.Range(1, 10)
            .Select(e => new EmployeeViewModel
            {
                Id = e,
                Name = $"Name {e}",
                SecondName = $"Secondname {e}",
                Patronymic = $"Patronymic {e}",
                Age = e % 30 + 20
            })
            .ToList();

        public IEnumerable<EmployeeViewModel> GetAll() => _employeeViews;

        public EmployeeViewModel GetById(int id) => _employeeViews.SingleOrDefault(e => e.Id == id);

        public void Add(EmployeeViewModel employee)
        {
            if (employee == null) throw new ArgumentNullException(nameof(employee));

            employee.Id = _employeeViews.Any() 
                ? _employeeViews.Max(e => e.Id) + 1 
                : 1;

            _employeeViews.Add(employee);
        }

        public void Edit(int id, EmployeeViewModel employee)
        {
            if (employee == null) throw new ArgumentNullException(nameof(employee));

            var dbEmployee = GetById(id);
            if (dbEmployee is null)
            {
                return;
            }

            dbEmployee.Name = employee.Name;
            dbEmployee.SecondName = employee.SecondName;
            dbEmployee.Patronymic = employee.Patronymic;
            dbEmployee.Age = employee.Age;
        }

        public bool Delete(int id)
        {
            var dbEmployee = GetById(id);
            if (dbEmployee is null)
            {
                return false;
            }

            return _employeeViews.Remove(dbEmployee);
        }

        public bool SaveChanges() => true;
    }
}
