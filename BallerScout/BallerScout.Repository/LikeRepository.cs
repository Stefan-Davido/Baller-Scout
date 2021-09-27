using BallerScout.Data;
using BallerScout.Entities;
using BallerScout.Repository.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BallerScout.Repository
{
    public class LikeRepository : ILikeRepository
    {
        private readonly DataContext _dataContext;

        public LikeRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void AddLike(Like like)
        {
            _dataContext.Like.Add(like);
            _dataContext.SaveChanges();
        }

        public void DeleteLike(int Id)
        {
            var like = GetLikeById(Id);
            _dataContext.Like.Remove(like);
            _dataContext.SaveChanges();
        }

        public IEnumerable<Like> GetAllLikes()
        {
            var result = _dataContext.Like.AsEnumerable();
            return result;
        }

        public Like GetLikeById(int Id)
        {
            var result = _dataContext.Like.Where(x => x.LikeId == Id).FirstOrDefault();
            return result;
        }

        public void UpdateLike(Like like)
        {
            _dataContext.Like.Update(like);
            _dataContext.SaveChanges();
        }
    }
}
