using BallerScout.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BallerScout.Repository.RepositoryInterfaces
{
    public interface IFollowingRepository
    {
        void AddFollowingList(Following post);
        void UpdateFollowingList(Following post);
        void DeleteFollowingList(int Id);
        Following GetFollowingListById (int Id);
        IEnumerable<Following> GetAllFollowings();
        void UnfollowBySingInUserIDAndUserID(string singInId, string userProfileId);
    }
}
