using BallerScout.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BallerScout.Service.ServiceInterfaces
{
    public interface ISavedPostService
    {
        void SavePost(SavedPost savedPost);
        void UnsavePost(int Id);
        IEnumerable<SavedPost> AllSavedPost();
        SavedPost GetSavedPostById(int Id);
        SavedPost GetSavedPostByUserIdAndPostId(string signInUserId, int postId);
        bool SavedCheck(string signInUserId, int postId);
        IEnumerable<SavedPost> AllSavedPostByUserId(string Id);
        int NumberOfSavedPosts(string Id);
    }
}
