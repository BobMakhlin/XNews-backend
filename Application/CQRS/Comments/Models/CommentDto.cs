using System;
using Application.Common.Mappings.Interfaces;
using Domain.Entities;

namespace Application.CQRS.Comments.Models
{
    public class CommentDto : IMapFrom<Comment>
    {
        #region Properties

        public Guid CommentId { get; set; }
        public string Content { get; set; }
        public Guid? ParentCommentId { get; set; }

        #endregion
    }
}