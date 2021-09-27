using BallerScout.Data;
using BallerScout.Entities;
using BallerScout.Repository.RepositoryInterfaces;
using BallerScout.Service.ServiceInterfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BallerScout.Service
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IFollowingService _followingService;
        private readonly ILikeRepository _likeRepository;
        private readonly ISavedPostService _savedPostService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly DataContext _dataContext;

        public PostService(IPostRepository postRepository,
            ILikeRepository likeRepository,
            IFollowingService followingService,
            ISavedPostService savedPostService,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            DataContext dataContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _postRepository = postRepository;
            _dataContext = dataContext;
            _followingService = followingService;
            _likeRepository = likeRepository;
            _savedPostService = savedPostService;
        }

        public void AddPost(Post post)
        {
            _postRepository.AddPost(post);
        }

        public IEnumerable<Post> AllPosts()
        {
            var result = _postRepository.GetAllPosts();
            result = result.OrderBy(x => x.DatePosted.TimeOfDay).Reverse();
            return result;
        }

        public void DeletePost(int Id)
        {
            _postRepository.DeletePost(Id);
        }

        public Post GetPostById(int Id)
        {
            var result = _postRepository.GetPostById(Id);
            return result;
        }

        public void UpdatePost(Post post)
        {
            _postRepository.UpdatePost(post);
        }

        public IEnumerable<Post> GetListOfPostsByUserId(string Id)
        {
            var myPosts = from p in AllPosts() select p;
            myPosts = myPosts.Where(s => s.UserId == Id).OrderByDescending(x => x.DatePosted.TimeOfDay).Reverse().AsEnumerable();

            return myPosts;
        }

        public async Task<IEnumerable<Post>> GetPostsByUsersIFollow(string Id)
        {
            var usersIFollow = _followingService.GetFollowingListByUserId(Id);
            List<Post> usersIFollowPosts = new List<Post>();

            foreach(var user in usersIFollow)
            {
                var userProfile = await _userManager.FindByIdAsync(user.UserIFollowId);
                var userPosts = GetListOfPostsByUserId(userProfile.Id).ToList();
                foreach (var post in userPosts)
                {
                    usersIFollowPosts.Add(post);
                }
            }

            var result = usersIFollowPosts.OrderBy(x => x.DatePosted.Day).Reverse();
            return result.AsEnumerable();
        }

        public IEnumerable<Post> GetPostByUserLikedId(string Id)
        {
            var likes = _likeRepository.GetAllLikes();
            var result = from l in likes select l;
            result = result.Where(x => x.UserLikedId == Id).AsEnumerable();
            List<Post> userLikedPost = new List<Post>();

            foreach (var post in result)
            {
                var postLiked = GetPostById(post.PostId);
                userLikedPost.Add(postLiked);
            }

            return userLikedPost.AsEnumerable().Reverse();
        }

        public IEnumerable<Post> GetAllUserSavedPost(string Id)
        {
            var userSavedPost = _savedPostService.AllSavedPostByUserId(Id);
            List<Post> allSavedPost = new List<Post>();
            foreach(var savedPost in userSavedPost)
            {
                var post = _postRepository.GetPostById(savedPost.PostId);
                allSavedPost.Add(post);
            }

            allSavedPost.OrderBy(x => x.DatePosted.Date).Reverse();

            return allSavedPost.AsEnumerable();
        }
    }
}
