using BallerScout.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BallerScout.Service.ServiceInterfaces
{
    public interface ISkillsService
    {
        void AddSkills(Skills skills);
        void UpdateSkills(Skills skills);
        void DeleteSkills(int id);
        public Skills GetSkillsById(int id);
        Skills GetSkillsByUserId(string id);
        IEnumerable<Skills> GetAllSkills();
        bool EditOrAddSkills(string id);
    }
}
