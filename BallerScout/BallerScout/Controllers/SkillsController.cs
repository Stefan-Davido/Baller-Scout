using BallerScout.Data;
using BallerScout.Entities;
using BallerScout.Models;
using BallerScout.Service.ServiceInterfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BallerScout.Controllers
{
    public class SkillsController : Controller
    {
        private readonly ISkillsService _skillsService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly DataContext _dataContext;

        public SkillsController(
            ISkillsService skillsService,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            DataContext dataContext)
        {
            _skillsService = skillsService;
            _userManager = userManager;
            _signInManager = signInManager;
            _dataContext = dataContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddSkills()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddSkills(Skills skills)
        {
            var user = await _userManager.GetUserAsync(User);
            skills.UserId = user.Id;
            skills.UserFullName = user.FirstName + " " + user.LastName;

            _skillsService.AddSkills(skills);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult EditSkills(string id)
        {
            var skills = _skillsService.GetSkillsByUserId(id);

            SkillsModel skillsModel = new SkillsModel();
            skillsModel.SkillStatsId = skills.SkillStatsId;
            skillsModel.UserId = skills.UserId;
            skillsModel.UserFullName = skills.UserFullName;
            skillsModel.Kicking = skills.Kicking;
            skillsModel.Handling = skills.Handling;
            skillsModel.Positioning = skills.Positioning;
            skillsModel.Reflexes = skills.Reflexes;
            skillsModel.Speed = skills.Speed;
            skillsModel.Diving = skills.Diving;
            skillsModel.Dribbling = skills.Dribbling;
            skillsModel.Pace = skills.Pace;
            skillsModel.Passing = skills.Passing;
            skillsModel.Physical = skills.Physical;
            skillsModel.Shooting = skills.Shooting;
            skillsModel.Defending = skills.Defending;

            return View(skillsModel);
        }

        [HttpPost]
        public IActionResult EditSkills(SkillsModel skillsModel)
        {
            var skills = _skillsService.GetSkillsById(skillsModel.SkillStatsId);

            skills.UserFullName = skillsModel.UserFullName;
            skills.Kicking = skillsModel.Kicking;
            skills.Handling = skillsModel.Handling;
            skills.Positioning = skillsModel.Positioning;
            skills.Reflexes = skillsModel.Reflexes;
            skills.Speed = skillsModel.Speed;
            skills.Diving = skillsModel.Diving;
            skills.Dribbling = skillsModel.Dribbling;
            skills.Pace = skillsModel.Pace;
            skills.Passing = skillsModel.Passing;
            skills.Physical = skillsModel.Physical;
            skills.Shooting = skillsModel.Shooting;
            skills.Defending = skillsModel.Defending;

            _skillsService.UpdateSkills(skills);

            return RedirectToAction(nameof(Index));
        }
    }
}
