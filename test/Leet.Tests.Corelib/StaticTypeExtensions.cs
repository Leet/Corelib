// -----------------------------------------------------------------------
// <copyright file="StaticTypeExtensions.cs" company="Leet">
//     Copyright (c) Leet. All rights reserved.
//     Licensed under the MIT License.
//     See License.txt in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Leet
{
    using System;
    using System.Diagnostics.Contracts;
    using System.Reflection;
    using Properties;

    /// <summary>
    ///     A <see langword="static"/> class that provides an extension methods for invoking static members.
    /// </summary>
    public static class StaticTypeExtensions
    {
        /// <summary>
        ///     Invokes a static public method with a specified name defined on a specified target type
        ///     with the arguments specified.
        /// </summary>
        /// <param name="target">
        ///     The reference to the type on which the method shall be invoked.
        /// </param>
        /// <param name="name">
        ///     The name of the method to be invoked.
        /// </param>
        /// <param name="args">
        ///     The collection of the arguments to pass to the invoked method.
        /// </param>
        /// <returns>
        ///     Method invocation return value.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="target"/> is <see langword="null"/>.
        ///     <para>-OR-</para>
        ///     <paramref name="name"/> is <see langword="null"/>.
        /// </exception>
        public static object InvokePublicMethod(this Type target, string name, params object[] args)
        {
            Contract.Requires(!object.ReferenceEquals(target, null));
            Contract.Requires(!object.ReferenceEquals(name, null));

            if (object.ReferenceEquals(target, null))
            {
                throw new ArgumentNullException(nameof(target));
            }

            if (object.ReferenceEquals(name, null))
            {
                throw new ArgumentNullException(nameof(name));
            }

            return target.InvokeMember(name, BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Static, Type.DefaultBinder, null, args);
        }

        /// <summary>
        ///     Invokes a public static method with a specified name defined on a specified target type
        ///     with the arguments specified.
        /// </summary>
        /// <param name="target">
        ///     The reference to the object on which the method shall be invoked.
        /// </param>
        /// <param name="name">
        ///     The name of the method to be invoked.
        /// </param>
        /// <param name="args">
        ///     The collection of the arguments to pass to the invoked method.
        /// </param>
        /// <returns>
        ///     An exception thrown during the invokation.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="target"/> is <see langword="null"/>.
        ///     <para>-OR-</para>
        ///     <paramref name="name"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     Expected exception has not been thrown.
        /// </exception>
        public static Exception InvokePublicMethodWithException(this Type target, string name, params object[] args)
        {
            Contract.Requires(!object.ReferenceEquals(target, null));
            Contract.Requires(!object.ReferenceEquals(name, null));

            if (object.ReferenceEquals(target, null))
            {
                throw new ArgumentNullException(nameof(target));
            }

            if (object.ReferenceEquals(name, null))
            {
                throw new ArgumentNullException(nameof(name));
            }

            try
            {
                target.InvokeMember(name, BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Static, Type.DefaultBinder, null, args);
            }
            catch (TargetInvocationException e)
            {
                return e.InnerException;
            }

            throw new InvalidOperationException(TestResources.Exception_InvalidOperation_NoExpectedException);
        }
    }
}
