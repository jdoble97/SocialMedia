using SocialMediaCore.Entities;
using System;
using System.Threading.Tasks;

namespace SocialMediaCore.Interfaces
{
    public interface IUnitWork : IDisposable
    {
        IRepository<Post> PostRepository { get;}
        IRepository<User> UserRepository { get; }
        IRepository<Comment> CommentRepository { get; }
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
