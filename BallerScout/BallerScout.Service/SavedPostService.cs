using BallerScout.Entities;
using BallerScout.Repository.RepositoryInterfaces;
using BallerScout.Service.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BallerScout.Service
{
    public class SavedPostService : ISavedPostService
    {
        private readonly ISavedPostRepository _savedPostRepository;

        public SavedPostService(ISavedPostRepository savedPostRepository)
        {
            _savedPostRepository = savedPostRepository;
        }

        public IEnumerable<SavedPost> AllSavedPost()
        {
            var result = _savedPostRepository.AllSavedPosts();
            return result;
        }

        public SavedPost GetSavedPostById(int Id)
        {
            var result = _savedPostRepository.GetSavedPostById(Id);
            return result;
        }
        
        public SavedPost GetSavedPostByUserIdAndPostId(string signInUserId, int postId)
        {
            var allPost = from p in AllSavedPost() select p;
            var savedPost = allPost.Where(x => x.UserSaveId == signInUserId && x.PostId == postId).FirstOrDefault();

            return savedPost;
        }

        public void SavePost(SavedPost savedPost)
        {
            _savedPostRepository.AddSavedPost(savedPost);
        }

        public void UnsavePost(int Id)
        {
            _savedPostRepository.DeleteSavedPost(Id);
        }

        public IEnumerable<SavedPost> AllSavedPostByUserId(string Id)
        {
            var allSavedPost = from p in AllSavedPost() select p;
            var allUserSavedPostsResult = allSavedPost.Where(x => x.UserSaveId == Id).AsEnumerable();
            var result = allUserSavedPostsResult.OrderBy(x => x.DateSaved.TimeOfDay).Reverse();

            return result;
        }

        public int NumberOfSavedPosts(string Id)
        {
            var numberOfSavedPosts = AllSavedPostByUserId(Id).Count();
            return numberOfSavedPosts;
        }

        public bool SavedCheck(string signInUserId, int postId)
        {
            var allPost = from p in AllSavedPost() select p;
            var savedResult = allPost.Where(x => x.UserSaveId == signInUserId && x.PostId == postId).FirstOrDefault();

            if(savedResult == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
