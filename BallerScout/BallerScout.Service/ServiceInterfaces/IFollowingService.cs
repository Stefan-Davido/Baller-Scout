using BallerScout.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BallerScout.Service.ServiceInterfaces
{
    public interface IFollowingService
    {
        void AddFollowingList(Following following);
        void UpdateFollowingList(Following following);
        void DeleteFollowingList(int Id);
        IEnumerable<Following> AllFollowingList();
        Following GetFollowingListById(int Id);
        IEnumerable<Following> GetFollowingListByUserId(string Id);
        int NumberOfFollowings(string Id);

        bool FollowCheck(string singInUserId, string userProfileId);
        void Unfollow(string singInUserId, string userProfileId);
        Task<IEnumerable<ApplicationUser>> UsersWhoFollowMe(string Id);
        int NumberOfFollowers(string Id);


    }
}
