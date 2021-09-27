using BallerScout.Entities;
using BallerScout.Models;
using BallerScout.Service.ServiceInterfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BallerScout.Controllers
{
    public class SearchController : Controller
    {
        private readonly ISearchService _searchService;
        private readonly IPostService _postService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public SearchController(ISearchService searchService,
            IPostService postService,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _searchService = searchService;
            _postService = postService;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Search()
        {
            SearchModel searchModel = new SearchModel();
            searchModel.AllPosts = _postService.AllPosts();

            return View(searchModel);
        }

        [HttpPost]
        public async Task<IActionResult> Search(SearchModel searchModel)
        {
            var searchString = searchModel.SearchString;
            SearchModel searchResult = new SearchModel();
            searchResult.SearchResult = await _searchService.SearchedUsersResult(searchString);

            return View(searchResult);
        }
    }
}
