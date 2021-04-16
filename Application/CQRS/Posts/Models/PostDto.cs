using System;
using Application.Common.Mappings.Interfaces;
using AutoMapper;
using Domain.Entities;

namespace Application.CQRS.Posts.Models
{
    public class PostDto : IMapFrom<Post>
    {
        #region Properties

        public Guid PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        #endregion
        
        // #region IMapFrom<Post>
        //
        // void IMapFrom<Post>.MapUsingProfile(Profile profile)
        // {
        //     profile.CreateMap<Post, PostDto>();
        // }
        //
        // #endregion
    }
}