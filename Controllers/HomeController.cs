using Microsoft.AspNetCore.Mvc;
using pi4_PixieVault_DemiBruls.Models;
using System.Diagnostics;

namespace pi4_PixieVault_DemiBruls.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(
            ILogger<HomeController> logger
            //IUserService userService
            )
        {
            _logger = logger;
            //var userService = new UserService();
            //userService.CreateUser();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Demi()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
