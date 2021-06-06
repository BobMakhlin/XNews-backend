using System;
using System.Collections.Generic;
using Application.Common.Mappings.Interfaces;
using Domain.Primary.Entities;

namespace Application.CQRS.Comments.Models
{
    public class CommentDto : IMapFrom<Comment>
    {
        #region Constructors

        public CommentDto()
        {
            Children = new();
        }

        #endregion
        
        #region Properties

        public Guid CommentId { get; set; }
        public string Content { get; set; }
        public string UserId { get; set; }
        public Guid? ParentCommentId { get; set; }
        public List<CommentDto> Children { get; set; }

        #endregion
    }
}