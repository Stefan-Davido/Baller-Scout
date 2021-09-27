using BallerScout.Entities;
using BallerScout.Service.ServiceInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BallerScout.API.Controllers
{
    [Route("api/post")]
    [ApiController]
    public class PostAPIController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostAPIController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet("AllPosts")]
        public IEnumerable<Post> AllPosts()
        {
            var posts = _postService.AllPosts();
            return posts;
        }
    }
}
