using BallerScout.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BallerScout.Service.ServiceInterfaces
{
    public interface ISearchService
    {
        Task<IEnumerable<ApplicationUser>> SearchedUsersResult(string searchString);
        //IEnumerable<ApplicationUser> SearchedUsersResult(string searchString);
        IEnumerable<ApplicationUser> SearchedUsersResultAPI(string searchString);
        IEnumerable<ApplicationUser> AllUsers();
        Tuple<List<SelectListItem>> SearchDropdownResult(IEnumerable<ApplicationUser> searchedUsers);
          
    }
}
