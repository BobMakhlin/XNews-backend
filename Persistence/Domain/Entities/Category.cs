using System;
using System.Collections.Generic;

#nullable disable

namespace Domain.Entities
{
    public partial class Category
    {
        public Category()
        {
            Posts = new HashSet<Post>();
        }

        public Guid CategoryId { get; set; }
        public string Title { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
