using System;
using System.Collections.Generic;

#nullable disable

namespace Domain.Entities
{
    public partial class Post
    {
        public Post()
        {
            Categories = new HashSet<Category>();
            Comments = new HashSet<Comment>();
            PostRates = new HashSet<PostRate>();
        }

        public Guid PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<PostRate> PostRates { get; set; }
    }
}
