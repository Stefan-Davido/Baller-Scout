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
    public class LikeService : ILikeService
    {
        private readonly ILikeRepository _likeRepository;
        private readonly IPostRepository _postRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _singInManager;

        public LikeService(ILikeRepository likeRepository,
            IPostRepository postRepository,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> singInManager)
        {
            _likeRepository = likeRepository;
            _postRepository = postRepository;
            _userManager = userManager;
            _singInManager = singInManager;
        }

        public void AddLike(Like like)
        {
            _likeRepository.AddLike(like);
        }

        public void DeleteLike(int Id)
        {
            _likeRepository.DeleteLike(Id);
        }

        public void DeleteLikeByPostAndUserLikedId(int postId, string userLikedId)
        {
            var allLikes = GetAllLikes();
            var like = allLikes.Where(x => x.PostId == postId && x.UserLikedId == userLikedId).FirstOrDefault();

            DeleteLike(like.LikeId);
        }

        public void UpdateLike(Like like)
        {
            _likeRepository.UpdateLike(like);
        }

        public IEnumerable<Like> GetAllLikes()
        {
            var likes = _likeRepository.GetAllLikes();
            return likes;
        }

        public Like GetLikeById(int Id)
        {
            var like = _likeRepository.GetLikeById(Id);
            return like;
        }

        public async Task<IEnumerable<ApplicationUser>> GetLikesByPostId(int id)
        {
            var likes = _likeRepository.GetAllLikes();
            var result = from l in likes select l;
            result = result.Where(x => x.PostId == id).ToList();
            List<ApplicationUser> LikedUsers = new List<ApplicationUser>();

            foreach (var user in result)
            {
                var userProfile = await _userManager.FindByIdAsync(user.UserLikedId);
                LikedUsers.Add(userProfile);
            }

            return LikedUsers.AsEnumerable();
        }

        public Like LikeCheck(int postId, string userLikedId)
        {
            var allLikes = GetAllLikes();
            var like = allLikes.Where(x => x.PostId == postId && x.UserLikedId == userLikedId).FirstOrDefault();

            return like;
        }
    }
}
