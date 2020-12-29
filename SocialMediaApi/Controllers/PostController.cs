using Microsoft.AspNetCore.Mvc;
using SocialMediaCore.Interfaces;
using SocialMediaInfrastructure.Entities;
using System.Threading.Tasks;

namespace SocialMediaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _postRepository;
        public PostController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            var posts = await _postRepository.GetPosts();
            return Ok(posts);   
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost(int id)
        {
            var post = await _postRepository.GetPost(id);
            return Ok(post);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Post post)
        {
            await _postRepository.InsertPost(post);
            return Ok(post);
        }
    }
}
