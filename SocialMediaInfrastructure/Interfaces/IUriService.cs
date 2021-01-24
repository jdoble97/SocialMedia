using SocialMediaCore.QueryFilter;
using System;

namespace SocialMediaInfrastructure.Interfaces
{
    public interface IUriService
    {
        Uri GetPostPaginationUri(PostQueryFilter filter, string actionUrl);
    }
}