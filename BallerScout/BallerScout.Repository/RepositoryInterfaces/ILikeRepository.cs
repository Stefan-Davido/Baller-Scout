using BallerScout.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BallerScout.Repository.RepositoryInterfaces
{
    public interface ILikeRepository
    {
        void AddLike(Like like);
        void UpdateLike(Like like);
        void DeleteLike(int Id);
        Like GetLikeById(int Id);
        IEnumerable<Like> GetAllLikes();
    }
}
