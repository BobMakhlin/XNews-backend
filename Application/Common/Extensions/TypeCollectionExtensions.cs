using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Common.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="IEnumerable{T}"/> of <see cref="Type"/>
    /// </summary>
    public static class TypeCollectionExtensions
    {
        /// <summary>
        /// Finds elements inside of <paramref name="types"/> that implement the given interface.
        /// </summary>
        /// <param name="types"></param>
        /// <param name="interfaceGenericTypeDefinition">Generic type definition of the interface (e.g.: IEnumerable<>)</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">
        /// When parameter <paramref name="interfaceGenericTypeDefinition"/> is not a generic type definition or not an interface
        /// </exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public static IEnumerable<Type> GetTypesImplementingGenericTypeDefinition(this IEnumerable<Type> types,
            Type interfaceGenericTypeDefinition)
        {
            if (types == null)
            {
                throw new ArgumentNullException(nameof(types));
            }

            if (interfaceGenericTypeDefinition == null)
            {
                throw new ArgumentNullException(nameof(interfaceGenericTypeDefinition));
            }

            if (!interfaceGenericTypeDefinition.IsGenericTypeDefinition)
            {
                throw new ArgumentException("Specified type must be a generic type definition",
                    nameof(interfaceGenericTypeDefinition));
            }

            if (!interfaceGenericTypeDefinition.IsInterface)
            {
                throw new ArgumentException("Specified type must be an interface",
                    nameof(interfaceGenericTypeDefinition));
            }

            return
            (
                from type in types
                let interfaces = type.GetInterfaces()
                where interfaces.Any
                (
                    interfaceType =>
                        interfaceType.IsGenericType &&
                        interfaceType.GetGenericTypeDefinition() == interfaceGenericTypeDefinition
                )
                select type
            );
        }
    }
}