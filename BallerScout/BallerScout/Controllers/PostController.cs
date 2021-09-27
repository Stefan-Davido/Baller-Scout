using BallerScout.Data;
using BallerScout.Entities;
using BallerScout.Models;
using BallerScout.Service.ServiceInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace BallerScout.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService _postService;
        private readonly ILikeService _likeService;
        private readonly ISavedPostService _savedPostService;
        private readonly IUploadImageService _uploadImageService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly DataContext _dataContext;

        public PostController(IPostService postService,
            ILikeService likeService,
            ISavedPostService savedPostService,
            IUploadImageService uploadImageService,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            DataContext dataContext)
        {
            _postService = postService;
            _likeService = likeService;
            _savedPostService = savedPostService;
            _uploadImageService = uploadImageService;
            _signInManager = signInManager;
            _userManager = userManager;
            _dataContext = dataContext;
        }

        public IActionResult AllMyPosts()
        {
            var userId = _userManager.GetUserId(User);
            var allPosts = _postService.GetListOfPostsByUserId(userId).OrderByDescending(x => x.DatePosted).Reverse();
            return View(allPosts);
        }

        [HttpGet]
        public IActionResult AddPost()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddPost(PostModel postModel, IFormFile file)
        {
            if (file != null)
            {
                var user = await _userManager.GetUserAsync(User);
                _uploadImageService.UploadPostImage(file);
                var DT = DateTime.Now;
                var date = new DateTime(DT.Year, DT.Month, DT.Day);

                var post = new Post();
                post.Description = postModel.Description;
                post.DatePosted = date;
                post.PhotoUrl = postModel.PhotoUrl;
                post.NumberOfLikes = 0;
                post.UserId = user.Id;
                post.UserName = user.UserName;
                post.UserProfilePhoto = user.ImgURL;
                post.PhotoUrl = file.FileName;
                post.DatePostedString = post.DatePosted.ToShortDateString();
              
                _postService.AddPost(post);
                user.NumberOfPosts += 1;
                await _userManager.UpdateAsync(user);

                return RedirectToAction(nameof(AllMyPosts));
            }
            else
            {
                return View();
            }
            
        }

        [HttpGet]
        public IActionResult MyPostEdit(int id)
        {
            var post = _postService.GetPostById(id);
            PostModel postModel = new PostModel();
            postModel.PostId = post.PostId;
            postModel.UserId = post.UserId;
            postModel.UserName = post.UserName;
            postModel.UserProfilePhoto = post.UserProfilePhoto;
            postModel.PhotoUrl = post.PhotoUrl;
            postModel.NumberOfLikes = post.NumberOfLikes;
            postModel.Description = post.Description;
            postModel.DatePosted = post.DatePosted;

            return View(postModel);
        }

        [HttpPost]
        public IActionResult MyPostEdit(PostModel postModel)
        {
            var post = _postService.GetPostById(postModel.PostId);

            post.PostId = postModel.PostId;
            post.UserId = postModel.UserId;
            post.UserName = postModel.UserName;
            post.UserProfilePhoto = postModel.UserProfilePhoto;
            post.PhotoUrl = postModel.PhotoUrl;
            post.NumberOfLikes = postModel.NumberOfLikes;
            post.Description = postModel.Description;
            post.DatePosted = postModel.DatePosted;
            post.DatePostedString = post.DatePosted.ToShortDateString();

            _postService.UpdatePost(post);
            return RedirectToAction(nameof(AllMyPosts));
        }
        
        [HttpGet]
        public IActionResult MyPostDetails(int id)
        {
            var post = _postService.GetPostById(id);
            return View(post);
        }

        [HttpGet]
        //[Authorize(Roles = "admin")]
        public IActionResult MyPostDelete(int id)
        {
            var post = _postService.GetPostById(id);
            return View(post);
        }

        [HttpPost, ActionName("MyPostDelete")]
        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteComfirmed(int id)
        {
            var post = _postService.GetPostById(id);
            _postService.DeletePost(post.PostId);

            var user = await _userManager.FindByIdAsync(post.UserId);
            user.NumberOfPosts -= 1;
            await _userManager.UpdateAsync(user);

            return RedirectToAction(nameof(AllMyPosts));
        }

        // *** Like 
        public async Task Like(int postId)
        {
            var post = _postService.GetPostById(postId);
            var userPostId = post.UserId;
            var usersPost = await _userManager.FindByIdAsync(userPostId);
            var signInUserId= _userManager.GetUserId(User);
            var signInUser = await _userManager.FindByIdAsync(signInUserId);

            var likeCheck = _likeService.LikeCheck(post.PostId, signInUser.Id);

            if (likeCheck == null)
            {
                Like like = new Like();
                like.PostId = postId;
                like.UserPostId = userPostId;
                like.UserName = usersPost.UserName;
                like.UserLikedId = signInUser.Id;
                like.UserLikedName = signInUser.UserName;
                _likeService.AddLike(like);

                var Like = 1;
                post.NumberOfLikes += Like;
                _postService.UpdatePost(post);
            }
            else
            {
                var Like = 1;
                post.NumberOfLikes -= Like;
                _postService.UpdatePost(post);
                _likeService.DeleteLikeByPostAndUserLikedId(postId, signInUser.Id);
            }
        }

        public async Task<IActionResult> PostLikesList(int postId)
        {
            var likesList = await _likeService.GetLikesByPostId(postId);

            return View(likesList);
        }

        // *** Save Post
        public async Task SavePost(int postId)
        {
            var post = _postService.GetPostById(postId);
            var signInUser = await _userManager.GetUserAsync(User);
            var userPost = await _userManager.FindByIdAsync(post.UserId);
            var savePostCheck = _savedPostService.SavedCheck(signInUser.Id, postId);

            if(savePostCheck == true)
            {
                var savedPost = _savedPostService.GetSavedPostByUserIdAndPostId(signInUser.Id, postId);

                //signInUser.NumberOfSavedPosts -= 1;
                signInUser.NumberOfSavedPosts = _savedPostService.NumberOfSavedPosts(signInUser.Id);
                await _userManager.UpdateAsync(userPost);
                await _dataContext.SaveChangesAsync();
                _savedPostService.UnsavePost(savedPost.SavingId);
            }
            else
            {
                SavedPost savedPost = new SavedPost()
                {
                    PostId = post.PostId,
                    UserPostId = userPost.Id,
                    UserPostName = userPost.FirstName,
                    UserSaveId = signInUser.Id,
                    UserSaveName = signInUser.FirstName,
                    DateSaved = DateTime.Now
                };

                //signInUser.NumberOfSavedPosts = signInUser.NumberOfSavedPosts += 1;
                signInUser.NumberOfSavedPosts = _savedPostService.NumberOfSavedPosts(signInUser.Id);
                await _userManager.UpdateAsync(userPost);
                await _dataContext.SaveChangesAsync();
                _savedPostService.SavePost(savedPost);
            }     
        }

        public IActionResult UserSavedPosts(string Id)
        {
            var allSavedPosts = _postService.GetAllUserSavedPost(Id).OrderByDescending(x => x.DatePosted);
            return View(allSavedPosts);
        }
    }
}
