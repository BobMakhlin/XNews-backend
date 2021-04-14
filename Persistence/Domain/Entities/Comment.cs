using System;
using System.Collections.Generic;

#nullable disable

namespace Domain.Entities
{
    public partial class Comment
    {
        public Comment()
        {
            CommentRates = new HashSet<CommentRate>();
            InverseParentComment = new HashSet<Comment>();
        }

        public Guid CommentId { get; set; }
        public string Content { get; set; }

        public Guid PostId { get; set; }
        public virtual Post Post { get; set; }
        
        public Guid? ParentCommentId { get; set; }
        public virtual Comment ParentComment { get; set; }
        
        public virtual ICollection<CommentRate> CommentRates { get; set; }
        public virtual ICollection<Comment> InverseParentComment { get; set; }
    }
}
