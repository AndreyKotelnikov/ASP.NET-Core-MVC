using System.IO;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

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

        public IActionResult TestAction()
        {
            //return View();
            //return new ViewResult();

            //return Json(new {Name = "Andrey", Id = 1}, new JsonSerializerOptions(){ MaxDepth = 1});
            //return new JsonResult(new {Name = "Andrey", Id = 1});

            //return Redirect("http://www.yandex.ru");
            //return new RedirectResult("http://www.yandex.ru");

            return RedirectToAction("Index", "Home");
            //return new RedirectToActionResult("Index", "Home", null);

            //return Content("Hello!", "application/text", Encoding.UTF8);
            //return new ContentResult{ Content = "Hello!", StatusCode = 200, ContentType = "application/text"};

            //return File(Encoding.UTF8.GetBytes("Hello world!"), "application/text", "HelloWorld.txt");
            //return new FileContentResult(Encoding.UTF8.GetBytes("Hello world!"), new MediaTypeHeaderValue("application/text", 5));
            //return new FileStreamResult(new MemoryStream(Encoding.UTF8.GetBytes("Hello world!")), "application/text");

            //return StatusCode(200);
            //return new StatusCodeResult(200);
            //return NoContent();
            //return new NoContentResult();
        }
    }
}