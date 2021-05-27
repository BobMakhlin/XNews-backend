using Application.Common.Mappings.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Application.CQRS.Roles.Models
{
    public class RoleDto : IMapFrom<IdentityRole>
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}