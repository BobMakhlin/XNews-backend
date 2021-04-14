#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Application.Common.Mappings.Interfaces;
using AutoMapper;

namespace Application.Common.Mappings.Profiles
{
    /// <summary>
    /// Looks for types that implement interface <see cref="IMappable"/> in the current assembly
    /// and applies mapping for them (using method <see cref="IMappable.MapUsingProfile"/> of each of these types).
    /// </summary>
    public class MappingProfile : Profile
    {
        #region Constructors

        public MappingProfile()
        {
            Assembly currentAssembly = Assembly.GetExecutingAssembly();
            ApplyMappingsFromAssembly(currentAssembly);
        }

        #endregion

        #region Methods

        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            IEnumerable<Type> assemblyTypes = assembly.GetTypes();
            IEnumerable<Type> targetTypes = GetTypesThatImplementInterface(assemblyTypes, typeof(IMappable));

            ApplyMappingsForTypes(targetTypes);
        }


        /// <summary>
        /// Finds elements inside of <paramref name="types"/> that implement the <paramref name="interfaceType"/>.
        /// </summary>
        /// <param name="types"></param>
        /// <param name="interfaceType"></param>
        /// <returns></returns>
        private IEnumerable<Type> GetTypesThatImplementInterface(IEnumerable<Type> types, Type interfaceType)
        {
            return types
                .Where
                (
                    type => !type.IsInterface && interfaceType.IsAssignableFrom(type)
                );
        }

        /// <summary>
        /// Creates an instance of each type of the given <paramref name="types"/> and calls method
        /// <see cref="IMappable.MapUsingProfile"/> on this instance, passing there this as the first parameter.
        /// </summary>
        /// <param name="types"></param>
        private void ApplyMappingsForTypes(IEnumerable<Type> types)
        {
            foreach (var type in types)
            {
                MethodInfo? mapUsingProfileMethod = type.GetMethod(nameof(IMappable.MapUsingProfile));

                object? instanceOfCurrentType = Activator.CreateInstance(type);
                object[] parameters = {this};

                mapUsingProfileMethod?.Invoke(instanceOfCurrentType, parameters);
            }
        }

        #endregion
    }
}