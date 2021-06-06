using Application.Common.Mappings.Interfaces;
using Application.Identity.Models;
using AutoMapper;

namespace Application.CQRS.Users.Models
{
    public class UserDto : IMapFrom<ApplicationUser>
    {
        #region Properties

        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        #endregion

        #region IMapFrom<ApplicationUser>

        public void MapUsingProfile(Profile profile)
        {
            profile.CreateMap<ApplicationUser, UserDto>()
                .ForMember
                (
                    x => x.UserId,
                    opts =>
                        opts.MapFrom(x => x.Id)
                );
        }

        #endregion
    }
}