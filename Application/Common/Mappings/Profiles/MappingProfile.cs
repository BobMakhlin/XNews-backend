using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Application.Common.Extensions;
using Application.Common.Mappings.Interfaces;
using AutoMapper;

namespace Application.Common.Mappings.Profiles
{
    /// <summary>
    /// Finds all types that implement interface <see cref="IMapFrom{T}"/> inside of the current assembly,
    /// for each one creates an instance and calls method <see cref="IMapFrom{T}.MapUsingProfile"/> on this instance,
    /// passing <see langword="this"/> as the first parameter to this method.
    /// </summary>
    public class MappingProfile : Profile
    {
        #region Fields

        /// <summary>
        /// Type of interface used for mapping.
        /// </summary>
        private readonly Type _mappingInterfaceType = typeof(IMapFrom<>);

        /// <summary>
        /// Name of method used for mapping.
        /// </summary>
        private readonly string _mappingMethodName = nameof(IMapFrom<object>.MapUsingProfile);

        #endregion

        #region Constructors

        public MappingProfile()
        {
            Assembly currentAssembly = Assembly.GetExecutingAssembly();
            ApplyMappingsFromAssembly(currentAssembly);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Finds all types in the <paramref name="assembly"/> that implement type <see cref="_mappingInterfaceType"/>
        /// and calls methods <see cref="_mappingMethodName"/> on each of them.
        /// </summary>
        /// <param name="assembly">An assembly in which the method looks for types</param>
        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            IEnumerable<Type> assemblyTypes = assembly.GetTypes();
            IEnumerable<Type> typesImplementingMappingInterface
                = assemblyTypes.GetTypesImplementingGenericTypeDefinition(_mappingInterfaceType);

            foreach (var type in typesImplementingMappingInterface)
            {
                ApplyMappingsForType(type);
            }
        }

        /// <summary>
        /// Finds interfaces of type <see cref="_mappingInterfaceType"/> inside of <paramref name="type"/>
        /// and calls method <see cref="_mappingMethodName"/> on each of these interfaces, passing there
        /// <see langword="this"/> as the first parameter.
        /// </summary>
        /// <param name="type"></param>
        private void ApplyMappingsForType(Type type)
        {
            IEnumerable<Type> allImplementedInterfaces = type.GetInterfaces();
            IEnumerable<Type> mappingInterfaces = FindMappingInterfacesIn(allImplementedInterfaces);

            foreach (var interfaceType in mappingInterfaces)
            {
                MethodInfo mappingMethod = interfaceType.GetMethod(_mappingMethodName);
                object instance = Activator.CreateInstance(type);

                if (mappingMethod != null && instance != null)
                {
                    CallMappingMethod(mappingMethod, instance);
                }
            }
        }

        /// <summary>
        /// Finds items of type <see cref="_mappingInterfaceType"/> inside of the given <paramref name="interfaces"/>
        /// </summary>
        /// <param name="interfaces"></param>
        /// <returns></returns>
        private IEnumerable<Type> FindMappingInterfacesIn(IEnumerable<Type> interfaces)
        {
            return interfaces.Where
            (
                interfaceType =>
                    interfaceType.IsGenericType &&
                    interfaceType.GetGenericTypeDefinition() == _mappingInterfaceType
            );
        }

        /// <summary>
        /// Calls <paramref name="mappingMethod"/> on <paramref name="instance"/>.
        /// </summary>
        /// <param name="mappingMethod"></param>
        /// <param name="instance"></param>
        private void CallMappingMethod(MethodInfo mappingMethod, object instance)
        {
            object[] parameters = {this};
            mappingMethod.Invoke(instance, parameters);
        }

        #endregion
    }
}