using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace ASP_NET_Core_MVC.ViewModels
{
    public class EmployeeViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "Имя")]
        [Required(ErrorMessage = "Имя является обязательным для заполнения!", AllowEmptyStrings = false)]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Длина имени должна быть между 2 и 50 символами!")]
        [RegularExpression(@"(?:[А-ЯЁ][а-яё]+[ ]*[0-9]*)|(?:[A-Z][a-z]+[ ]*[0-9]*)", ErrorMessage = "Используется алфавит разных языков или пробел или цифры в середине слова!")]
        public string Name { get; set; }

        [Display(Name = "Фамилия")]
        [Required(ErrorMessage = "Фамилия является обязательным для заполнения!", AllowEmptyStrings = false)]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Длина фамилии должна быть между 2 и 100 символами!")]
        [RegularExpression(@"(?:[А-ЯЁ][а-яё]+[ ]*[0-9]*)|(?:[A-Z][a-z]+[ ]*[0-9]*)", ErrorMessage = "Используется алфавит разных языков или пробел или цифры в середине слова!")]
        public string SecondName { get; set; }

        [Display(Name = "Отчество")]
        public string Patronymic { get; set; }

        [Display(Name = "Возраст")]
        [Required(ErrorMessage = "Нужно указать возраст!")]
        public int Age { get; set; }
    }
}
