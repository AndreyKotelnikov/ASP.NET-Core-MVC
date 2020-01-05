using System.Collections.Generic;
using System.Linq;
using ASP_NET_Core_MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ASP_NET_Core_MVC.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEnumerable<EmployeeView> _employeeViews = Enumerable.Range(1, 10)
        .Select(e => new EmployeeView
        {
            Id = e,
            Name = $"Name {e}",
            SecondName = $"SecondName {e}"
        });

        public IActionResult Index() => View(_employeeViews);

        public IActionResult Details(int? id)
        {
            if (id is null)
            {
                return BadRequest();
            }

            var employee = _employeeViews.FirstOrDefault(e => e.Id == id);
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

            var sellectEmployees = _employeeViews;
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

        [HttpPost]
        public IActionResult Edit(int id, [FromBody] EmployeeView employee)
        {
            return RedirectToAction(nameof(Index));
        }
    }
}