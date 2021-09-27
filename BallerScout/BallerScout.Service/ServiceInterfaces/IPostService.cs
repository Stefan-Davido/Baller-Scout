using BallerScout.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BallerScout.Service.ServiceInterfaces
{
    public interface IPostService
    {
        void AddPost(Post post);
        void UpdatePost(Post post);
        void DeletePost(int Id);
        IEnumerable<Post> AllPosts();
        Post GetPostById(int Id);
        IEnumerable<Post> GetListOfPostsByUserId(string Id);
        Task<IEnumerable<Post>> GetPostsByUsersIFollow(string Id);
        IEnumerable<Post> GetAllUserSavedPost(string Id);
    }
}
