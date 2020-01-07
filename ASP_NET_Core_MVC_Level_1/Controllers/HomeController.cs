using Microsoft.AspNetCore.Mvc;

namespace ASP_NET_Core_MVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();

        public IActionResult Blog() => View();

        public IActionResult BlogSingle() => View();

        public IActionResult Cart() => View();

        public IActionResult Checkout() => View();

        public IActionResult ContactUs() => View();

        public IActionResult Login() => View();

        public IActionResult ProductDetails() => View();

        public IActionResult Error404() => View();
    }
}