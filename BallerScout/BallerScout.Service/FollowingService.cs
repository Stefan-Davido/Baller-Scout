using BallerScout.Data;
using BallerScout.Entities;
using BallerScout.Repository.RepositoryInterfaces;
using BallerScout.Service.ServiceInterfaces;
using Microsoft.AspNetCore.Identity;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BallerScout.Service
{
    public class FollowingService : IFollowingService
    {
        private readonly IFollowingRepository _followingRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly DataContext _dataContext;

        public FollowingService(IFollowingRepository followingRepository,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            DataContext dataContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _followingRepository = followingRepository;
            _dataContext = dataContext;
        }

        public void AddFollowingList(Following following)
        {
            _followingRepository.AddFollowingList(following);
        }

        public IEnumerable<Following> AllFollowingList()
        {
            var result = _followingRepository.GetAllFollowings();
            return result;
        }

        public void DeleteFollowingList(int Id)
        {
            _followingRepository.DeleteFollowingList(Id);
        }

        public void Unfollow(string singInUserId, string userProfileId)
        {
            _followingRepository.UnfollowBySingInUserIDAndUserID(singInUserId, userProfileId);
        }

        public Following GetFollowingListById(int Id)
        {
            var result = _followingRepository.GetFollowingListById(Id);
            return result;
        }

        public void UpdateFollowingList(Following following)
        {
            _followingRepository.UpdateFollowingList(following);
        }

        public IEnumerable<Following> GetFollowingListByUserId(string Id)
        {
            var allFollowings = from f in AllFollowingList() select f;
            allFollowings = allFollowings.Where(x => x.UserId == Id).AsEnumerable().DistinctBy(x => x.UserIFollowId);

            return allFollowings;
        }

        public int NumberOfFollowings(string Id)
        {
            var allFollowings = from f in AllFollowingList() select f;
            allFollowings = allFollowings.Where(x => x.UserId == Id);
            var result = allFollowings.ToList().Count();

            return result;
        }

        public bool FollowCheck(string singInUserId, string userProfileId)
        {
            var allFollowings = from f in AllFollowingList() select f;
            allFollowings = allFollowings.Where(x => x.UserId == singInUserId && x.UserIFollowId == userProfileId);
            var result = allFollowings.ToList().Count();

            if(result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<IEnumerable<ApplicationUser>> UsersWhoFollowMe(string Id)
        {
            List<ApplicationUser> followers = new List<ApplicationUser>();
            var allFollowers = from f in AllFollowingList() select f;
            allFollowers = allFollowers.Where(x => x.UserIFollowId == Id).AsEnumerable().DistinctBy(x => x.UserId);

            foreach (var user in allFollowers)
            {
                var userProfile = await _userManager.FindByIdAsync(user.UserId);
                followers.Add(userProfile);
            }

            return followers.AsEnumerable();
        }

        public int NumberOfFollowers(string Id)
        {
            var followers = from f in AllFollowingList() select f;
            followers = followers.Where(x => x.UserIFollowId == Id).AsEnumerable().DistinctBy(x => x.UserId);

            return followers.Count();
        }
    }
}
