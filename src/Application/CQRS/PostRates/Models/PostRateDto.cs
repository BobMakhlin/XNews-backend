using Application.Common.Mappings.Interfaces;
using Domain.Primary.Entities;

namespace Application.CQRS.PostRates.Models
{
    public class PostRateDto : IMapFrom<PostRate>
    {
        #region Properties

        public double Rate { get; set; }
        public string UserId { get; set; }

        #endregion
    }
}