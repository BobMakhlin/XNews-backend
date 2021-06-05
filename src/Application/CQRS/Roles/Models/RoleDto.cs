using Application.Common.Mappings.Interfaces;
using Application.Identity.Models;

namespace Application.CQRS.Roles.Models
{
    public class RoleDto : IMapFrom<ApplicationRole>
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}