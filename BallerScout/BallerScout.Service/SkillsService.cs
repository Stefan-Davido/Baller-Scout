using BallerScout.Entities;
using BallerScout.Repository.RepositoryInterfaces;
using BallerScout.Service.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BallerScout.Service
{
    public class SkillsService : ISkillsService
    {
        private readonly ISkillsRepository _skillsRepository;

        public SkillsService(ISkillsRepository skillsRepository)
        {
            _skillsRepository = skillsRepository;
        }

        public void AddSkills(Skills skills)
        {
            _skillsRepository.AddSkills(skills);
        }

        public void DeleteSkills(int id)
        {
            _skillsRepository.DeleteSkills(id);
        }
        public void UpdateSkills(Skills skills)
        {
            _skillsRepository.UpdateSkills(skills);
        }

        public IEnumerable<Skills> GetAllSkills()
        {
            var result = _skillsRepository.GetAllSkills();
            return result;
        }

        public Skills GetSkillsById(int id)
        {
            var result = _skillsRepository.GetSkillsById(id);
            return result;
        }

        public Skills GetSkillsByUserId(string id)
        {
            var result = _skillsRepository.GetSkillsByUserId(id);
            return result;
        }
        public bool EditOrAddSkills(string id)
        {
            var skillList = GetSkillsByUserId(id);
            if (skillList == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
