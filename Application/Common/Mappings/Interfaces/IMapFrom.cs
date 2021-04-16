using AutoMapper;

namespace Application.Common.Mappings.Interfaces
{
    public interface IMapFrom<T>
    {
        /// <summary>
        /// Creates mapping using the given <paramref name="profile"/>.
        /// </summary>
        /// <param name="profile">The profile used to create the mapping</param>
        void MapUsingProfile(Profile profile);
    }
}