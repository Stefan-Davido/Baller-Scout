using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BallerScout.Data;
using BallerScout.Entities;
using BallerScout.Service.ServiceInterfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BallerScout.Areas.Identity.Pages.Account.Manage
{
    public class PersonalInfoModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly DataContext _dataContext;
        private readonly IDropDownsService _dropDownsService; 

        public PersonalInfoModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            DataContext dataContext,
            IDropDownsService dropDownsService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _dataContext = dataContext;
            _dropDownsService = dropDownsService;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            public string Foot { get; set; }
            public double Height { get; set; }
            public string Position { get; set; }
            public string City { get; set; }
            public string Country { get; set; }
            public string Cityzenship { get; set; }
            public string Club { get; set; }
            public DateTime DateOfBirth { get; set; }
            public string PhoneNumber{ get; set; }
            public string LastName{ get; set; }
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = user.FirstName;

            Input = new InputModel
            {
                Foot = user.Foot,
                Club = user.Club,
                Height = user.Height,
                Position = user.Position,
                City = user.City,
                Country = user.Country,
                Cityzenship = user.Cityzenship,
                DateOfBirth = user.DateOfBirth,
                LastName = user.LastName,
                PhoneNumber = phoneNumber
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }

            user.Club = Input.Club;
            user.Position = Input.Position;
            user.Foot = Input.Foot;
            user.Height = Input.Height;
            user.City = Input.City;
            user.Country = Input.Country;
            user.Cityzenship = Input.Cityzenship;
            user.DateOfBirth = Input.DateOfBirth;
            user.LastName = Input.LastName;
            user.PhoneNumber = Input.PhoneNumber;

            _dataContext.Users.Update(user);
            _dataContext.SaveChanges();

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
