using BallerScout.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BallerScout.Service.ServiceInterfaces
{
    public interface ILikeService
    {
        void AddLike(Like like);
        void DeleteLike(int Id);
        void UpdateLike(Like like);
        Like GetLikeById(int Id);
        IEnumerable<Like> GetAllLikes();
        Like LikeCheck(int postId, string userLikedId);
        void DeleteLikeByPostAndUserLikedId(int postId, string userLikedId);

        Task<IEnumerable<ApplicationUser>> GetLikesByPostId(int id);
    }
}
