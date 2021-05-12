using System;

#nullable disable

namespace Domain.Primary.Entities
{
    public class CommentRate
    {
        public Guid CommentRateId { get; set; }
        public double Rate { get; set; }

        public Guid CommentId { get; set; }
        public Comment Comment { get; set; }
    }
}
