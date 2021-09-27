using BallerScout.Data;
using BallerScout.Entities;
using BallerScout.Repository.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BallerScout.Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly DataContext _dataContext;

        public PostRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void AddPost(Post post)
        {
            _dataContext.Post.Add(post);
            _dataContext.SaveChanges();
        }

        public void DeletePost(int Id)
        {
            var post = GetPostById(Id);
            _dataContext.Remove(post);
            _dataContext.SaveChanges();
        }

        public IEnumerable<Post> GetAllPosts()
        {
            var result = _dataContext.Post.AsEnumerable();
            return result;
        }

        public Post GetPostById(int Id)
        {
            var result = _dataContext.Post.FirstOrDefault(x => x.PostId == Id);
            return result;
        }

        public void UpdatePost(Post post)
        {
            _dataContext.Post.Update(post);
            _dataContext.SaveChanges();
        }
    }
}
