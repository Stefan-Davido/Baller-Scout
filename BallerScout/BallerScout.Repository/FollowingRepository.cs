using BallerScout.Data;
using BallerScout.Entities;
using BallerScout.Repository.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BallerScout.Repository
{
    public class FollowingRepository : IFollowingRepository
    {
        private readonly DataContext _dataContext;

        public FollowingRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void AddFollowingList(Following following)
        {
            _dataContext.Add(following);
            _dataContext.SaveChanges();
        }

        public void DeleteFollowingList(int Id)
        {
            var following = GetFollowingListById(Id);
            _dataContext.Remove(following);
            _dataContext.SaveChanges();
        }

        public IEnumerable<Following> GetAllFollowings()
        {
            var result = _dataContext.Following.AsEnumerable();
            return result;
        }

        public Following GetFollowingListById(int Id)
        {
            var result = _dataContext.Following.FirstOrDefault(x => x.FollowingListId == Id);
            return result;
        }

        public void UnfollowBySingInUserIDAndUserID(string singInId, string userProfileId)
        {
            var result = _dataContext.Following.FirstOrDefault(x => x.UserId == singInId && x.UserIFollowId == userProfileId);
            _dataContext.Following.Remove(result);
            _dataContext.SaveChanges();
        }

        public void UpdateFollowingList(Following post)
        {
            _dataContext.Update(post);
            _dataContext.SaveChanges();
        }
    }
}
