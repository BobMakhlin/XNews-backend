using System;

#nullable disable

namespace Domain.Primary.Entities
{
    public partial class CommentRate
    {
        public Guid CommentRateId { get; set; }
        public double Rate { get; set; }

        public Guid CommentId { get; set; }
        public virtual Comment Comment { get; set; }
    }
}
