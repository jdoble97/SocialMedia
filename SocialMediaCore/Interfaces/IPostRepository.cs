using SocialMediaCore.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialMediaCore.Interfaces
{
    //Definimos los metodos que deben implementar las clases que usen la interfaz->Contrato
    public interface IPostRepository
    {
        Task<IEnumerable<Post>> GetPosts();
        Task<Post> GetPost(int id);

        Task InsertPost(Post post);

    }
}
