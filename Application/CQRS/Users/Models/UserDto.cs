using Application.Common.Mappings.Interfaces;
using Application.Identity.Models;

namespace Application.CQRS.Users.Models
{
    public class UserDto : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}