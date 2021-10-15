using BallerScout.Data;
using BallerScout.Entities;
using BallerScout.Service.ServiceInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using System.Threading.Tasks;

namespace BallerScout.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly DataContext _dataContext;
        private readonly IFollowingService _followingService;
        private readonly IPostService _postService;
        private readonly IUploadImageService _uploadImageService;
        private readonly ISkillsService _skillsService;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            DataContext dataContext,
            IFollowingService followingService,
            IPostService postService,
            IUploadImageService uploadImageService,
            ISkillsService skillsService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _dataContext = dataContext;
            _followingService = followingService;
            _postService = postService;
            _skillsService = skillsService;
            _uploadImageService = uploadImageService;
        }
        public string ImagePath { get; set; }
        public string Username { get; set; }
        public string Position { get; set; }
        public Skills SkillsList { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }
        public int NumberOfFollowings { get; set; }
        public int NumberOfFollowers { get; set; }
        public int NumberOfPosts { get; set; }
        

        public class InputModel
        {
            public string FirstName { get; set; }
            public string InputImagePath { get; set; }
            public string About { get; set; }
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);

            //Here To Fix UserName(Dont need to be shown)
            Username = user.FirstName;
            NumberOfFollowings = _followingService.NumberOfFollowings(user.Id);
            NumberOfFollowers = _followingService.NumberOfFollowers(user.Id);
            NumberOfPosts = _postService.GetListOfPostsByUserId(user.Id).Count();
            Position = user.Position;
            SkillsList = _skillsService.GetSkillsByUserId(user.Id);

            ImagePath = user.ImgURL;

            Input = new InputModel
            {
                FirstName = user.FirstName,
                InputImagePath = user.ImgURL,
                About = user.About
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(IFormFile file)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            user.FirstName = Input.FirstName;
            user.About = Input.About;
            if(file != null)
            {
                _uploadImageService.UploadProfileImage(file);
                user.ImgURL = file.FileName;
            }

            var allUserPosts = _postService.GetListOfPostsByUserId(user.Id);
            foreach(var post in allUserPosts)
            {
                post.UserProfilePhoto = user.ImgURL;
                _dataContext.Post.Update(post);

            }

            _dataContext.Users.Update(user);
            _dataContext.SaveChanges();

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
