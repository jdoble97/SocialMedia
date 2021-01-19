using SocialMediaCore.Entities;
using SocialMediaCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaCore.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;
        public PostService(IPostRepository postRepository, IUserRepository userRepository)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
        }

        public async Task<bool> DeletePost(int id)
        {
            return await _postRepository.DeletePost(id);
        }

        public async Task<Post> GetPost(int id)
        {
            return await _postRepository.GetPost(id);
        }

        public async Task<IEnumerable<Post>> GetPosts()
        {
            return await _postRepository.GetPosts();
        }

        public async Task InsertPost(Post post)
        {
            var user = await _userRepository.GetUser(post.UserId);
            if(user == null)
            {
                throw new Exception("Usuario no existe");
            }
            if (post.Description.Contains("sexo"))
            {
                throw new Exception("Contain not allowed");
            }
            await _postRepository.InsertPost(post);
        }

        public async Task<bool> UpdatePost(Post post)
        {
            return await _postRepository.UpdatePost(post);
        }
    }
}
