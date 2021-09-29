using AutoMapper;
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
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly DataContext _dataContext;

        public SkillsController(
            ISkillsService skillsService,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            DataContext dataContext,
            IMapper mapper)
        {
            _skillsService = skillsService;
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
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

            skillsModel = _mapper.Map<Skills, SkillsModel>(skills);

            return View(skillsModel);
        }

        [HttpPost]
        public IActionResult EditSkills(SkillsModel skillsModel)
        {
            Skills skills = new Skills();
            skills = _mapper.Map<SkillsModel, Skills>(skillsModel);

            _skillsService.UpdateSkills(skills);

            return RedirectToAction(nameof(Index));
        }
    }
}
