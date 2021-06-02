using System;
using Application.Common.Mappings.Interfaces;
using AutoMapper;
using Domain.Primary.Entities;

namespace Application.CQRS.PostRates.Models
{
    public class PostRateDto : IMapFrom<PostRate>
    {
        #region Properties

        public Guid PostRateId { get; set; }
        public double Rate { get; set; }

        #endregion
    }
}