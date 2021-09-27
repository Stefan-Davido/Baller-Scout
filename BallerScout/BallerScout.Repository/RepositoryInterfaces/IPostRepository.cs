using BallerScout.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BallerScout.Repository.RepositoryInterfaces
{
    public interface IPostRepository
    {
        void AddPost(Post post);
        void UpdatePost(Post post);
        void DeletePost(int Id);
        Post GetPostById(int Id);
        IEnumerable<Post> GetAllPosts();
    }
}
