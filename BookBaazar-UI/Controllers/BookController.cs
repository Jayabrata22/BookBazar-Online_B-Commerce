using Microsoft.AspNetCore.Mvc;

namespace BookBaazar_UI.Controllers
{
    public class BookController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public BookController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(string username, string password, string fullName)
        {
            // Here you would typically call a service to handle the registration logic
            // For example: _userService.Register(new RegisterDTO { Username = username, Password = password, FullName = fullName });
            // Redirect to a success page or return a view
            return RedirectToAction("Index");
        }
    }
}
