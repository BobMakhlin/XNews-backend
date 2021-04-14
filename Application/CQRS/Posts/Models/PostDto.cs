using System;
using Application.Common.Mappings.Interfaces;
using AutoMapper;
using Domain.Entities;

namespace Application.CQRS.Posts.Models
{
    public class PostDto : IMappable
    {
        #region Properties

        public Guid PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        #endregion
        
        #region IMappable

        public void MapUsingProfile(Profile profile)
        {
            profile.CreateMap<Post, PostDto>();
        }

        #endregion
    }
}