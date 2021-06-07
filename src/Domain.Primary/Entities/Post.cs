using System;
using System.Collections.Generic;

#nullable disable

namespace Domain.Primary.Entities
{
    public class Post
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
        public string UserId { get; set; }

        public ICollection<Category> Categories { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<PostRate> PostRates { get; set; }
    }
}
