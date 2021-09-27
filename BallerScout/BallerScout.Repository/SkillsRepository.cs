using BallerScout.Data;
using BallerScout.Entities;
using BallerScout.Repository.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace BallerScout.Repository
{
    public class SkillsRepository : ISkillsRepository
    {
        private readonly DataContext _dataContext;

        public SkillsRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void AddSkills(Skills skills)
        {
            _dataContext.Skills.Add(skills);
            _dataContext.SaveChanges();
        }
        public void UpdateSkills(Skills skills)
        {
            _dataContext.Skills.Update(skills);
            _dataContext.SaveChanges();
        }

        public void DeleteSkills(int id)
        {
            var SkillList = GetSkillsById(id);
            _dataContext.Skills.Remove(SkillList);
            _dataContext.SaveChanges();
        }

        public Skills GetSkillsById(int id)
        {
            var result = _dataContext.Skills.FirstOrDefault(x => x.SkillStatsId == id);
            return result;
        }

        public Skills GetSkillsByUserId(string id)
        {
            var result = _dataContext.Skills.FirstOrDefault(x => x.UserId == id);
            return result;
        }

        public IEnumerable<Skills> GetAllSkills()
        {
            var result = _dataContext.Skills.AsEnumerable();
            return result;
        }
    }
}
