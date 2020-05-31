using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CardPortfolio.Models;

namespace CardPortfolio.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBlogPostData _blogPostData;
        public HomeController(ILogger<HomeController> logger, IBlogPostData blogPostData)
        {
            _logger = logger;
            _blogPostData = blogPostData;
        }

        public IActionResult Index()
        {
            ViewBag.ThreeRecentPosts = _blogPostData.GetThreeRecentPosts();
            return View();
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
        public IActionResult Contact()
        {
            return View();
        }
    }
}
