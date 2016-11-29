//-----------------------------------------------------------------------
// <copyright file="ObjectExtensions.cs" company="Leet">
//     © 2016 Leet. Licensed under the MIT License.
//     See License.txt in the project root for license information.
// </copyright>
//-----------------------------------------------------------------------

namespace Leet
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Diagnostics.Contracts;

    /// <summary>
    ///     A <see langword="static"/> class that contains extension methods for <see cref="Object"/> type.
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        ///     Gets a hash code of the specified object or 0 if the object is a <see langword="null"/> reference.
        /// </summary>
        /// <param name="obj">
        ///     A reference to the object which hash code shall be obtained.
        /// </param>
        /// <returns>
        ///     A hash code of the specified object or 0 if the object is a <see langword="null"/> reference.
        /// </returns>
        [SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "obj",
            Justification = "Parameter name conforms object methods parameter naming convention.")]
        public static int GetHashCodeAllowNull(this object obj)
        {
            Contract.Ensures(!object.ReferenceEquals(obj, null) || Contract.Result<int>() == 0);

            if (object.ReferenceEquals(obj, null))
            {
                return 0;
            }

            return obj.GetHashCode();
        }

        /// <summary>
        ///     Returns a string that represents the specified object or specified string if the object is a <see langword="null"/> reference.
        /// </summary>
        /// <param name="obj">
        ///     A reference to the object which string representation shall be obtained.
        /// </param>
        /// <param name="nullRepresentation">
        ///     A value that shall be returned if the specified object is a <see langword="null"/> reference.
        /// </param>
        /// <returns>
        ///     A string that represents the specified object or specified string if the object is a <see langword="null"/> reference.
        /// </returns>
        [SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "obj",
            Justification = "Parameter name conforms object methods parameter naming convention.")]
        public static string ToStringAllowNull(this object obj, string nullRepresentation)
        {
            Contract.Ensures(!object.ReferenceEquals(obj, null) || object.ReferenceEquals(Contract.Result<string>(), nullRepresentation));

            if (object.ReferenceEquals(obj, null))
            {
                return nullRepresentation;
            }

            return obj.ToString();
        }

        /// <summary>
        ///     Gets a type of the specified object and allows the object to be a <see langword="null"/> reference.
        /// </summary>
        /// <param name="value">
        ///     Object which type shall be obtained.
        /// </param>
        /// <returns>
        ///     Type of the specified object or <see langword="null"/> if the object is a <see langword="null"/> reference.
        /// </returns>
        public static Type GetTypeAllowNull(this object value)
        {
            Contract.Ensures(object.ReferenceEquals(Contract.Result<Type>(), null) == object.ReferenceEquals(value, null));

            if (object.ReferenceEquals(value, null))
            {
                return null;
            }

            return value.GetType();
        }
    }
}