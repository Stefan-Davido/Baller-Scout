using BallerScout.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BallerScout.Repository.RepositoryInterfaces
{
    public interface ISkillsRepository
    {
        public void AddSkills(Skills skills );
        public void UpdateSkills(Skills skills);
        public void DeleteSkills(int id);
        Skills GetSkillsById(int id);
        Skills GetSkillsByUserId(string id);
        public IEnumerable<Skills> GetAllSkills();
    }
}
