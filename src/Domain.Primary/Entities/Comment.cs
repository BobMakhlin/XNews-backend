using System;
using System.Collections.Generic;

#nullable disable

namespace Domain.Primary.Entities
{
    public class Comment
    {
        public Comment()
        {
            CommentRates = new HashSet<CommentRate>();
            InverseParentComment = new HashSet<Comment>();
        }

        public Guid CommentId { get; set; }
        public string Content { get; set; }

        public Guid PostId { get; set; }
        public Post Post { get; set; }
        
        public Guid? ParentCommentId { get; set; }
        public Comment ParentComment { get; set; }
        
        public ICollection<CommentRate> CommentRates { get; set; }
        public ICollection<Comment> InverseParentComment { get; set; }
    }
}
