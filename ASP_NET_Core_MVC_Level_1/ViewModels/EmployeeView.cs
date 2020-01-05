using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace ASP_NET_Core_MVC.ViewModels
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
