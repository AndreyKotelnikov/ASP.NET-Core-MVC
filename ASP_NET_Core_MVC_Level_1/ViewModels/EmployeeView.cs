using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ASP_NET_Core_MVC_Level_1.ViewModels
{
    public class EmployeeView
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "Имя")]
        public string Name { get; set; }

        [Display(Name = "Фамилия", Description = "Фамилия у этого сотрудника")]
        public string SecondName { get; set; }
    }
}
