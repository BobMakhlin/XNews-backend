using System;
using AutoMapper;

namespace Application.Common.Mappings.Interfaces
{
    public interface IMapFrom<T>
    {
        /// <summary>
        /// Creates mapping using the given <paramref name="profile"/>.
        /// By default maps type <see cref="T"/> into the type of the current instance.
        /// </summary>
        /// <param name="profile">The profile used to create the mapping</param>
        void MapUsingProfile(Profile profile)
        {
            Type currentInstanceType = GetType();
            profile.CreateMap(typeof(T), currentInstanceType);
        }
    }
}