using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SocialMediaCore.DTOs;
using SocialMediaCore.Entities;
using SocialMediaCore.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialMediaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        //Por constructor inyectamos dependencias
        public PostController(IPostRepository postRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            var posts = await _postRepository.GetPosts();
            /*var postsDto = posts.Select(x => new PostDto
            {
                PostId = x.PostId,
                Description = x.Description,
                Date = x.Date,
                Image = x.Image,
                UserId = x.UserId
            }) ;*/
            var postsDto = _mapper.Map<IEnumerable<PostDto>>(posts);
            return Ok(postsDto);   
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost(int id)
        {
            var post = await _postRepository.GetPost(id);
            /*PostDto postDto = new PostDto
            {
                PostId = post.PostId,
                Description = post.Description,
                Date = post.Date,
                Image = post.Image,
                UserId = post.UserId
            };*/
            var postDto = _mapper.Map<PostDto>(post);
            return Ok(postDto);
        }

        [HttpPost]
        public async Task<IActionResult> Post(PostDto postDto)
        {
            /*var post = new Post
            {
                PostId = postDto.PostId,
                Description = postDto.Description,
                Date = postDto.Date,
                Image = postDto.Image,
                UserId = postDto.UserId
            };*/
            var post = _mapper.Map<Post>(postDto);
            await _postRepository.InsertPost(post);
            return Ok(post);
        }
    }
}
