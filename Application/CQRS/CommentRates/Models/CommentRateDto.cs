
using System;
using Application.Common.Mappings.Interfaces;
using Domain.Entities;

namespace Application.CQRS.CommentRates.Models
{
    public class CommentRateDto : IMapFrom<CommentRate>
    {
        #region Properties

        public Guid CommentRateId { get; set; }
        public double Rate { get; set; }

        #endregion
    }
}