using Avengers.Models;
using CORE_MVC_EXAM.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Avengers.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _dbContext;

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _dbContext = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _dbContext.Avengers.ToListAsync());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public void LoginAction()
        {
            string login = Request.Query["login"].ToString();
            string passwrod = Request.Query["password"].ToString();

            var users = from f in _dbContext.Avengers
                        select f;
            if (!String.IsNullOrEmpty(login))
            {
                users = from f in users where f.HeroName.Equals(login) select f;
            }
            if (users.Any())
            {
                users = from f in users where f.RealName.Equals(passwrod) select f;
                if (users.Any())
                {
                    Response.StatusCode = 200;
                    Response.WriteAsync("Success");
                    return;
                }
                else
                {
                    Response.StatusCode = 500;
                    Response.WriteAsync("Wrong password");
                    return;
                }
            }

            Response.StatusCode = 404;
            Response.WriteAsync("Failure");
            return;
        }
    }
}