using System;
using System.Collections.Generic;
using System.Linq;
using ASP_NET_Core_MVC.Infrastructure.Interfaces;
using ASP_NET_Core_MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ASP_NET_Core_MVC.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployeesData _employeesData;

        public EmployeesController(IEmployeesData employeesData)
        {
            _employeesData = employeesData;
        }

        public IActionResult Index() => View(_employeesData.GetAll());

        public IActionResult Details(int? id)
        {
            if (id is null)
            {
                return BadRequest();
            }

            var employee = _employeesData.GetById((int)id);
            if (employee is null)
            {
                return NotFound();
            }

            return View(employee);
        }

        public IActionResult DetailsSearchByNames(string name, string secondName)
        {
            if (name is null && secondName is null)
            {
                return BadRequest();
            }

            var sellectEmployees = _employeesData.GetAll();
            if (!string.IsNullOrWhiteSpace(name))
            {
                sellectEmployees = sellectEmployees.Where(e => e.Name == name);
            }
            if (!string.IsNullOrWhiteSpace(secondName))
            {
                sellectEmployees = sellectEmployees.Where(e => e.SecondName == secondName);
            }

            var employee = sellectEmployees.FirstOrDefault();

            if (employee is null)
            {
                return NotFound();
            }

            return View(nameof(Details), employee);
        }

        public IActionResult EditOrCreate(int? id)
        {
            if (id is null)
            {
                return View(new EmployeeView());
            }

            if (id <= 0)
            {
                return BadRequest();
            }

            var employee = _employeesData.GetById((int)id);
            if (employee is null)
            {
                return NotFound();
            }

            return View(employee);
        }

        [HttpPost]
        public IActionResult EditOrCreate(EmployeeView employee)
        {
            if (employee == null) throw new ArgumentNullException(nameof(employee));

            if (!ModelState.IsValid)
            {
                return View(employee);
            }

            var id = employee.Id;
            if (id == 0)
            {
                _employeesData.Add(employee);
            }
            else
            {
                _employeesData.Edit(id, employee);
            }
            
            _employeesData.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}