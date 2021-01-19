using Microsoft.EntityFrameworkCore;
using SocialMediaCore.Entities;
using SocialMediaCore.Interfaces;
using SocialMediaInfrastructure.Data;
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

        /* CRUD OPERATIONS*/

        //READ
        public async Task<IEnumerable<Post>> GetPosts()
        {
            var posts = await _context.Posts.ToListAsync();
            return posts;
        }

        //READ
        public async Task<Post> GetPost(int id)
        {
            var post = await _context.Posts.FirstOrDefaultAsync(x => x.PostId == id);
            return post;
        }

        //CREATE
        public async Task InsertPost(Post post)
        {
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
        }

        //UPDATE
        public async Task<bool> UpdatePost(Post post)
        {
            var currentPost = await GetPost(post.PostId);
            currentPost.Date = post.Date;
            currentPost.Description = post.Description;
            currentPost.Image = post.Image;
            int rows = await _context.SaveChangesAsync();
            return rows > 0;
        }

        //DELETE
        public async Task<bool> DeletePost(int id)
        {
            var currentPost = await _context.Posts.FirstOrDefaultAsync(x => x.PostId == id);
            _context.Posts.Remove(currentPost);
            int rows = await _context.SaveChangesAsync();
            return rows > 0;
        }

    }
}
