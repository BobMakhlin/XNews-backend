using System;
using Application.Common.Mappings.Interfaces;
using Application.Identity.Models;
using AutoMapper;

namespace Application.CQRS.Roles.Models
{
    public class RoleDto : IMapFrom<ApplicationRole>
    {
        #region Properties

        public string RoleId { get; set; }
        public string Name { get; set; }

        #endregion

        #region IMapFrom<ApplicationRole>

        public void MapUsingProfile(Profile profile)
        {
            profile.CreateMap<ApplicationRole, RoleDto>()
                .ForMember
                (
                    x => x.RoleId,
                    opts =>
                        opts.MapFrom(x => x.Id)
                );
        }

        #endregion
    }
}