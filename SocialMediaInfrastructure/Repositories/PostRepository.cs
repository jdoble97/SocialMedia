using Microsoft.EntityFrameworkCore;
using SocialMediaCore.Interfaces;
using SocialMediaInfrastructure.Data;
using SocialMediaInfrastructure.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialMediaInfrastructure.Repositories
{
    public class PostRepository: IPostRepository
    {
        private readonly SocialMediaContext _context;
        public PostRepository(SocialMediaContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Post>> GetPosts()
        {
            var posts = await _context.Posts.ToListAsync();
            return posts;
        }

        public async Task<Post> GetPost(int id)
        {
            var post = await _context.Posts.FirstOrDefaultAsync(x => x.PostId == id);
            return post;
        }
    }
}
