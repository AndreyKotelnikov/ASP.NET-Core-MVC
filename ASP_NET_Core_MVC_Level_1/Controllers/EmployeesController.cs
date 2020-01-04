using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASP_NET_Core_MVC_Level_1.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ASP_NET_Core_MVC_Level_1.Controllers
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
    }
}