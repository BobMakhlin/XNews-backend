using System;

#nullable disable

namespace Domain.Primary.Entities
{
    public class PostRate
    {
        public Guid PostRateId { get; set; }
        public double Rate { get; set; }
        public string UserId { get; set; }

        public Guid PostId { get; set; }
        public Post Post { get; set; }
    }
}
