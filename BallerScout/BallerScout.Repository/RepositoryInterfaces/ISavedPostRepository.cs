using BallerScout.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BallerScout.Repository.RepositoryInterfaces
{
    public interface ISavedPostRepository
    {
        void AddSavedPost(SavedPost savedPost);
        void DeleteSavedPost(int Id);
        SavedPost GetSavedPostById(int Id);
        IEnumerable<SavedPost> AllSavedPosts();
    }
}
