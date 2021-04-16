using System;
using Application.Common.Mappings.Interfaces;
using AutoMapper;
using Domain.Entities;

namespace Application.CQRS.Categories.Models
{
    public class CategoryDto : IMapFrom<Category>
    {
        #region Properties

        public Guid CategoryId { get; set; }
        public string Title { get; set; }

        #endregion
    }
}