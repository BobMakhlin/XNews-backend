using System;
using Application.Common.Mappings.Interfaces;
using AutoMapper;
using Domain.Primary.Entities;

namespace Application.CQRS.Posts.Models
{
    public class PostDto : IMapFrom<Post>
    {
        #region Properties

        public Guid PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string UserId { get; set; }

        #endregion
    }
}