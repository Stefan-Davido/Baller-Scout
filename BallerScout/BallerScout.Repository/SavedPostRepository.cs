using BallerScout.Data;
using BallerScout.Entities;
using BallerScout.Repository.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BallerScout.Repository
{
    public class SavedPostRepository : ISavedPostRepository
    {
        private readonly DataContext _dataContext;

        public SavedPostRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void AddSavedPost(SavedPost savedPost)
        {
            _dataContext.SavedPosts.Add(savedPost);
            _dataContext.SaveChanges();
        }

        public IEnumerable<SavedPost> AllSavedPosts()
        {
            var result = _dataContext.SavedPosts.AsEnumerable();
            return result;
        }

        public void DeleteSavedPost(int Id)
        {
            var savedPost = GetSavedPostById(Id);
            _dataContext.SavedPosts.Remove(savedPost);
            _dataContext.SaveChanges();
        }

        public SavedPost GetSavedPostById(int Id)
        {
            var result = _dataContext.SavedPosts.Where(x => x.SavingId == Id).FirstOrDefault();
            return result;
        }
    }
}
