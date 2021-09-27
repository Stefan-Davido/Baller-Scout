using BallerScout.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BallerScout.Services.ServiceInterfaces
{
    public interface IPostService
    {
        void AddPost(Post post);
        void UpdatePost(Post post);
        void DeletePost(int Id);
        IEnumerable<Post> AllPosts();
        Post GetPostById(int Id);
    }
}
