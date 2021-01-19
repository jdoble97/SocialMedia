﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SocialMediaApi.Responses;
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
            var response = new ApiResponse<IEnumerable<PostDto>>(postsDto);
            return Ok(response);   
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
            await _postRepository.InsertPost(post);
            postDto = _mapper.Map<PostDto>(post);
            var response = new ApiResponse<PostDto>(postDto);
            return Ok(response);
        }

        //Actualizar un post
        [HttpPut]
        public async Task<IActionResult> Put(int id, PostDto postDto)
        {
            var post = _mapper.Map<Post>(postDto);
            post.PostId = id;
            var result = await _postRepository.UpdatePost(post);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        //Borrar un registro
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _postRepository.DeletePost(id);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }
    }
}
