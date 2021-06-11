using System;
using Application.Common.Mappings.Interfaces;
using Domain.Primary.Entities;

namespace Application.CQRS.CommentRates.Models
{
    public class CommentRateDto : IMapFrom<CommentRate>
    {
        #region Properties

        public double Rate { get; set; }
        public string UserId { get; set; }
        
        #endregion
    }
}