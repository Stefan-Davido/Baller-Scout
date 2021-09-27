using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using BallerScout.Data;
using BallerScout.Entities;
using BallerScout.Service.ServiceInterfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BallerScout.Areas.Identity.Pages.Account.Manage
{
    public class _PostsModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly DataContext _dataContext;
        private readonly IPostService _postService;

        public _PostsModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            DataContext dataContext,
            IPostService postService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _dataContext = dataContext;
            _postService = postService;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }
        //public string ProfileImage{ get; set; } 

        [BindProperty]
        public InputModel Input { get; set; }

        //PROPERTIES
        public string UserName { get; set; }
        public string UserProfilePhoto { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public int Likes { get; set; }
        public DateTime DatePosted { get; set; }

        public class InputModel
        {
            public string UserName { get; set; }
            public string UserProfilePhoto { get; set; }
            public string Image { get; set; }
            public string Description { get; set; }
            public int Likes{ get; set; }
            public DateTime DatePosted{ get; set; }
        }

        private IEnumerable<Post> LoadAsync(Post post)
        {
            //var userName = await _userManager.GetUserNameAsync(user);
            //var post = user.Post;

            //Username = user.FirstName;
            //user.ImgURL = GetProfilePhoto(user.Id).ToString();

            //UserName = user.FirstName + " " + user.LastName;
            //Image = post.PhotoUrl;
            //Description = post.Description;
            //Likes = 30;
            //DatePosted = DateTime.Now;
            //UserProfilePhoto = user.ImgURL;

            var allPosts = _postService.AllPosts();

            return allPosts;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            var post = user.Post;
            //if (user == null)
            //{
            //    return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            //}

            LoadAsync(post);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            var Post = user.Post;
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                LoadAsync(Post);
                return Page();
            }

            Post post = new Post
            {
                PhotoUrl = Input.Image,
                Description = Input.Description,
                UserName = Input.UserName,
                DatePosted = Input.DatePosted
            };

            _dataContext.Post.Update(post);
            _dataContext.SaveChanges();

            //await _signInManager.RefreshSignInAsync(post);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }

        public async Task<string> GetProfilePhoto(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var img = user.ImgURL;
            return img;
        }

        public class IndexPhoto : ControllerBase
        {
            private readonly UserManager<ApplicationUser> _userManager;

            public IndexPhoto(UserManager<ApplicationUser> userManager)
            {
                _userManager = userManager;
            }

            // Get Profile Photo


            // Upload Photo
            public IActionResult UploadPhoto()
            {
                try
                {
                    var file = Request.Form.Files[0];
                    var folderName = Path.Combine("wwwroot", "ProfilePhotos");
                    var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                    if (file.Length > 0)
                    {
                        var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                        var fullPath = Path.Combine(pathToSave, fileName);
                        var dbPath = fileName;

                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }

                        return Ok(new { dbPath });
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(500, "Internal server Error" + ex);
                }
            }
        }

    }
}
