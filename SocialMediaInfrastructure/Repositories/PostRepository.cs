using SocialMediaCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocialMediaInfrastructure.Repositories
{
    public class PostRepository
    {
        public IEnumerable<Post> GetPosts()
        {
            var posts = Enumerable.Range(1, 10).Select(x => new Post 
            {
                PostId = x,
                Description = $"Description{x}",
                Date = DateTime.Now,
                Image = $"https://banco-imagenes.com/{x}",
                UserId = x*2
            });
            return posts;
        }
    }
}
