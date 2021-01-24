using SocialMediaCore.Entities;
using SocialMediaCore.Exceptions;
using SocialMediaCore.Interfaces;
using SocialMediaCore.QueryFilter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMediaCore.Services
{
    public class PostService : IPostService
    {
        /*Se susituirá IPost y IUser por IRepository
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;*/
        /*
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Post> _postRepository;
        private readonly IRepository<Comment> _commentRepository;*/
        private readonly IUnitWork _unitOfWork;


        public PostService(IUnitWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> DeletePost(int id)
        {
             await _unitOfWork.PostRepository.Delete(id);
            return true;
        }

        public async Task<Post> GetPost(int id)
        {
            return await _unitOfWork.PostRepository.GetById(id);
        }

        public PagedList<Post> GetPosts(PostQueryFilter filters)
        {
            //A nivel de dominio no debo tener referencia ef ni a temas de infraestructura
            var posts = _unitOfWork.PostRepository.GetAll();
            if(filters.UserId != null)
            {
                posts = posts.Where(x => x.UserId == filters.UserId);
            }
            if (filters.Date != null)
            {
                posts = posts.Where(x => x.Date.ToShortDateString() == filters.Date?.ToShortDateString());
            }
            if (filters.Description != null)
            {
                posts = posts.Where(x => x.Description.ToLower().Contains(filters.Description.ToLower()));
            }

            var pagedPosts = PagedList<Post>.Create(posts, filters.PageNumber, filters.PageSize);
            return pagedPosts;
        }

        public async Task InsertPost(Post post)
        {
            var user = await _unitOfWork.UserRepository.GetById(post.UserId);
            if(user == null)
            {
                throw new BusinessException("User doesn't exist");
            }

            var userPost = await _unitOfWork.PostRepository.GetPostsByUser(user.Id);
            if (userPost.Count() < 10)
            {
                var lastPost = userPost.OrderByDescending(x=>x.Date).FirstOrDefault();
                if (( DateTime.Now- lastPost.Date).TotalDays < 7)
                {
                    throw new BusinessException("No puedes publicar");
                }
            }
            if (post.Description.Contains("sexo"))
            {
                throw new BusinessException("Contain not allowed");
            }
            await _unitOfWork.PostRepository.Add(post);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> UpdatePost(Post post)
        {
            _unitOfWork.PostRepository.Update(post);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
