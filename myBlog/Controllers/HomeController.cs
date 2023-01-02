using Microsoft.AspNetCore.Mvc;
using myBlog.Models;
using myBlog.Models.Comments;
using myBlog.Models.ViewModels;
using myBlog.Repository.IRepository;
using System.Diagnostics;

namespace myBlog.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRepository _repo;

        public HomeController(ILogger<HomeController> logger, 
            IRepository repo)
        {
            _logger = logger;
            _repo = repo;
        }
        public IActionResult Index(int pageNumber, string category, string search, string orderBy)
        {
            if (pageNumber < 1)
                return RedirectToAction("Index", new { pageNumber = 1, category });
            var posts = _repo.GetAllPosts(pageNumber, category, search, orderBy);
            return View(posts);
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
    }
}