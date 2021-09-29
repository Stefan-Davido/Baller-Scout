using AutoMapper;
using BallerScout.Entities;
using BallerScout.Mapping;
using BallerScout.Models;
using BallerScout.Service.ServiceInterfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BallerScout.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPostService _postService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ISearchService _searchService;

        public HomeController(IPostService postService,
           UserManager<ApplicationUser> userManager,
           SignInManager<ApplicationUser> signInManager,
           ISearchService searchService)
        {
            _postService = postService;
            _signInManager = signInManager;
            _userManager = userManager;
            _searchService = searchService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var singInUser = await _userManager.GetUserAsync(User);
            if(singInUser != null)
            {
                var singInUserId = await _userManager.GetUserIdAsync(singInUser);
                var posts = await _postService.GetPostsByUsersIFollow(singInUserId);

                if(posts.Count() > 0)
                {
                    return View(posts);
                }
                else
                {
                    return RedirectToAction("ZeroFollowers", "User");
                }          
            }
            else
            {
                var posts = _postService.AllPosts();
                return View(posts);
            }
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
