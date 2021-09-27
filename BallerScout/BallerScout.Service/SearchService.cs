using BallerScout.Data;
using BallerScout.Entities;
using BallerScout.Service.ServiceInterfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace BallerScout.Service
{
    public class SearchService : ISearchService
    {
        private readonly IPostService _postService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly DataContext _dataContext;

        public SearchService(IPostService postService,
           UserManager<ApplicationUser> userManager,
           SignInManager<ApplicationUser> signInManager,
           DataContext dataContext)
        {
            _postService = postService;
            _signInManager = signInManager;
            _userManager = userManager;
            _dataContext = dataContext;
        }

        public IEnumerable<ApplicationUser> AllUsers()
        {
            var allUsers = _userManager.Users.AsEnumerable();
            return allUsers;
        }

        public async Task<IEnumerable<ApplicationUser>> SearchedUsersResult(string searchString)
        {
            if (searchString != null || searchString != "")
            {
                List<ApplicationUser> searchResult = new List<ApplicationUser>();
                var allUsers = AllUsers().ToList();
                foreach (var user in allUsers)
                {
                    if (user.FirstName.ToLower().Contains(searchString) ||
                        //user.LastName.ToLower().Contains(searchString) ||
                        user.UserName.ToLower().Contains(searchString))
                    {
                        var userProfile = await _userManager.FindByIdAsync(user.Id);
                        searchResult.Add(userProfile);
                    }
                }

                if (searchResult.Count() == 0)
                {
                    return AllUsers();
                }
                else
                {
                    return searchResult.AsEnumerable();
                }   
            }
            else
            {
                return AllUsers();
            }
        }
        public IEnumerable<ApplicationUser> SearchedUsersResultAPI(string searchString)
        {
            
            if (searchString == null || searchString == "")
            {
                return AllUsers();
            }
            else
            {          
                List<ApplicationUser> searchResult = new List<ApplicationUser>();
                var allUsers = AllUsers().ToList();
                foreach (var user in allUsers)
                {
                    if(user.FirstName.ToLower().Contains(searchString.ToLower())  ||
                        user.LastName.ToLower().Contains(searchString.ToLower())  ||
                        user.UserName.ToLower().Contains(searchString.ToLower()))
                    {
                        searchResult.Add(user);
                    }
                    return searchResult.AsEnumerable();
                }

                //var allUsers = from u in AllUsers() select u;
                //searchResult = allUsers.Where(x => x.FirstName.ToLower().Contains(searchString.ToLower()) ||
                //                                        x.LastName.ToLower().Contains(searchString.ToLower()) ||
                //                                        x.UserName.ToLower().Contains(searchString.ToLower()) ||
                //                                        x.Email.ToLower().Contains(searchString.ToLower()));
                return searchResult;
            }
        }

        public Tuple<List<SelectListItem>> SearchDropdownResult(IEnumerable<ApplicationUser> searchedUsers)
        {
            List<SelectListItem> searchUsers = new List<SelectListItem>()
            {
                new SelectListItem() { Text =  "Search", Value = "0", Selected = true },
            };

            foreach (var user in searchedUsers)
            {
                searchUsers.Add(new SelectListItem { Value = user.Id, Text = user.FirstName + user.LastName });
            }          

            return Tuple.Create(searchUsers);
        }
    }
}
