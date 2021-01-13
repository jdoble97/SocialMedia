using AutoMapper;
using SocialMediaCore.DTOs;
using SocialMediaCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMediaInfrastructure.Mapping
{
    class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Post, PostDto>();
            CreateMap<PostDto, Post>();
        }
    }
}
