using SocialMediaCore.Entities;
using SocialMediaCore.QueryFilter;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialMediaCore.Interfaces
{
    public interface IPostService
    {
        PagedList<Post> GetPosts(PostQueryFilter postQueryFilters);
        Task<Post> GetPost(int id);

        Task InsertPost(Post post);

        Task<bool> UpdatePost(Post post);
        Task<bool> DeletePost(int id);
    }
}