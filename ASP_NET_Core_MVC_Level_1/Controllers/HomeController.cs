using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASP_NET_Core_MVC_Level_1.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ASP_NET_Core_MVC_Level_1.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;

        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ReadConfig()
        {
            return Content(_configuration["CustomData"]);
        }

        public IActionResult GetEmployees()
        {
            ViewBag.SomeData = "Привет от ViewBag";
            ViewData["test"] = "Hello from ViewData";
            
            var emplist = Enumerable.Range(1, 10)
                .Select(e => new EmployeeView
            {
                Id = e, 
                Name = $"Name {e}", 
                SecondName = $"SecondName {e}"
            });

            return View(emplist);
        }
    }
}