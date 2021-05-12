using System;
using System.Collections.Generic;

#nullable disable

namespace Domain.Primary.Entities
{
    public class Category
    {
        public Category()
        {
            Posts = new HashSet<Post>();
        }

        public Guid CategoryId { get; set; }
        public string Title { get; set; }

        public ICollection<Post> Posts { get; set; }
    }
}
