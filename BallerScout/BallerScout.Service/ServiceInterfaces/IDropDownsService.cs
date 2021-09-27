using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BallerScout.Service.ServiceInterfaces
{
    public interface IDropDownsService
    {
        public Task<List<SelectListItem>> Postition(string userId);
        public Task<List<SelectListItem>> Foot(string userId);
    }
}
