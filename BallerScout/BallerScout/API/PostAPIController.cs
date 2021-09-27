using BallerScout.Controllers;
using BallerScout.Entities;
using BallerScout.Repository.RepositoryInterfaces;
using BallerScout.Service.ServiceInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BallerScout.API
{
    [Route("api/PostAPI")]
    [ApiController]
    public class PostAPIController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly PostController _postController;
        private readonly ISearchService _searchService;

        public PostAPIController(IPostService postService,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            PostController postController,
            ISearchService searchService)
        {
            _postService = postService;
            _signInManager = signInManager;
            _userManager = userManager;
            _postController = postController;
            _searchService = searchService;
        }

        [Route("MyPosts/{Id}")]
        [HttpGet]
        public IEnumerable<Post> GetAllMyPosts(string Id)
        {
            var posts = _postService.GetListOfPostsByUserId(Id);
            return posts;
        }

        [HttpGet("GetPostById/{id}")]
        public IEnumerable<Post> GetMyPostsAPI(string Id)
        {
            var myPosts = _postService.GetListOfPostsByUserId(Id);
            return myPosts;
        }

        [HttpGet("GetAllPosts")]
        public IEnumerable<Post> GetPosts()
        {
            var myPosts = _postService.AllPosts();
            return myPosts;
        }
        
        [HttpGet("GetAllUsers")]
        public IEnumerable<ApplicationUser> GetAllUsers()
        {
            var myPosts = _searchService.AllUsers();
            return myPosts;
        }     

        [HttpGet("Search")]
        public IEnumerable<ApplicationUser> SearchedUsers()
        {
            var result =  _searchService.SearchedUsersResultAPI("d");
            return result;
        }
    }
}
