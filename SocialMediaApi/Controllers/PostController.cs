using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SocialMediaApi.Responses;
using SocialMediaCore.CustomEntities;
using SocialMediaCore.DTOs;
using SocialMediaCore.Entities;
using SocialMediaCore.Interfaces;
using SocialMediaCore.QueryFilter;
using SocialMediaInfrastructure.Interfaces;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace SocialMediaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        //Por constructor inyectamos dependencias
        public PostController(IPostService postRepository, IMapper mapper, IUriService uriService)
        {
            _postService = postRepository;
            _mapper = mapper;
            _uriService = uriService;
        }
        [HttpGet(Name =nameof(GetPosts))]
        //[ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<PostDto>>))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetPosts([FromQuery]PostQueryFilter filters)//FromQuery conseguimos mapear las querys de entrada en el objeto QueryFilter
        {
            var posts = _postService.GetPosts(filters);
            /*var postsDto = posts.Select(x => new PostDto
            {
                PostId = x.PostId,
                Description = x.Description,
                Date = x.Date,  
                Image = x.Image,
                UserId = x.UserId
            }) ;*/
            var postsDto = _mapper.Map<IEnumerable<PostDto>>(posts);
            var metadata = new Metadata
            {
                TotalCount = posts.TotalCount,
                PageSize = posts.PageSize,
                CurrentPage = posts.CurrentPage,
                TotalPages = posts.TotalPages,
                HasNextPage = posts.HasNextPage,
                HasPreviousPage = posts.HasPreviousPage,
                NextPageUrl = _uriService.GetPostPaginationUri(filters, Url.RouteUrl(nameof(GetPosts))).ToString()
            };
            /* Opcion 1: Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata)); */
            //Opcion 2
            var response = new ApiResponse<IEnumerable<PostDto>>(postsDto)
            {
                Meta = metadata
            };
            return Ok(response);   
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost(int id)
        {
            var post = await _postService.GetPost(id);
            /*PostDto postDto = new PostDto
            {
                PostId = post.PostId,
                Description = post.Description,
                Date = post.Date,
                Image = post.Image,
                UserId = post.UserId
            };*/
            var postDto = _mapper.Map<PostDto>(post);
            var response = new ApiResponse<PostDto>(postDto);
            return Ok(response);
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
            await _postService.InsertPost(post);
            postDto = _mapper.Map<PostDto>(post);
            var response = new ApiResponse<PostDto>(postDto);
            return Ok(response);
        }

        //Actualizar un post
        [HttpPut]
        public async Task<IActionResult> Put(int id, PostDto postDto)
        {
            var post = _mapper.Map<Post>(postDto);
            post.Id = id;
            var result = await _postService.UpdatePost(post);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        //Borrar un registro
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _postService.DeletePost(id);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }
    }
}
