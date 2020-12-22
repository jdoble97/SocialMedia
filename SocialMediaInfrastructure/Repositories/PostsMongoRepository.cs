using SocialMediaCore.Entities;
using SocialMediaCore.Interfaces;
using SocialMediaInfrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaInfrastructure.Repositories
{
    public class PostsMongoRepository : IPostRepository
    {
        public async Task<IEnumerable<Publicacion>> GetPosts()
        {
            var posts = Enumerable.Range(1, 10).Select(x => new Post
            {
                PostId = x,
                Description = $"Description Mongo {x}",
                Date = DateTime.Now,
                Image = $"https://banco-imagenes.com/{x}",
                UserId = x * 2
            });
            await Task.Delay(10);
            return null;
        }
    }
}
