﻿// -----------------------------------------------------------------------
// <copyright file="NonPublicObjectExtensions.cs" company="Leet">
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
    ///     A <see langword="static"/> class that provides an extension methods for <see cref="object"/> type.
    /// </summary>
    public static class NonPublicObjectExtensions
    {
        /// <summary>
        ///     Invokes a protected method with a specified name defined on a specified target object
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
        ///     Method invocation return value.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="target"/> is <see langword="null"/>.
        ///     <para>-OR-</para>
        ///     <paramref name="name"/> is <see langword="null"/>.
        /// </exception>
        public static object InvokeProtectedMethod(this object target, string name, params object[] args)
        {
            if (object.ReferenceEquals(target, null))
            {
                throw new ArgumentNullException(nameof(target));
            }

            if (object.ReferenceEquals(name, null))
            {
                throw new ArgumentNullException(nameof(name));
            }

            var type = target.GetType();
            return type.InvokeMember(
                name,
                BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.Instance,
                Type.DefaultBinder,
                target,
                args);
        }

        /// <summary>
        ///     Invokes a protected method with a specified name defined on a specified target object
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
        public static Exception InvokeProtectedMethodWithException(this object target, string name, params object[] args)
        {
            if (object.ReferenceEquals(target, null))
            {
                throw new ArgumentNullException(nameof(target));
            }

            if (object.ReferenceEquals(name, null))
            {
                throw new ArgumentNullException(nameof(name));
            }

            var type = target.GetType();

            try
            {
                type.InvokeMember(
                    name,
                    BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.Instance,
                    Type.DefaultBinder,
                    target,
                    args);
            }
            catch (TargetInvocationException e)
            {
                return e.InnerException;
            }

            throw new InvalidOperationException(TestResources.Exception_InvalidOperation_NoExpectedException);
        }

        /// <summary>
        ///     Gets a value of the protected property with a specified name defined on a specified target object.
        /// </summary>
        /// <param name="target">
        ///     The reference to the object from which the property value shall be obtained.
        /// </param>
        /// <param name="name">
        ///     The name of the property which value shall be obtained.
        /// </param>
        /// <returns>
        ///     Value of the specified property.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="target"/> is <see langword="null"/>.
        ///     <para>-OR-</para>
        ///     <paramref name="name"/> is <see langword="null"/>.
        /// </exception>
        public static object GetProtectedPropertyValue(this object target, string name)
        {
            if (object.ReferenceEquals(target, null))
            {
                throw new ArgumentNullException(nameof(target));
            }

            if (object.ReferenceEquals(name, null))
            {
                throw new ArgumentNullException(nameof(name));
            }

            var type = target.GetType();
            return type.InvokeMember(
                name,
                BindingFlags.GetProperty | BindingFlags.NonPublic | BindingFlags.Instance,
                Type.DefaultBinder,
                target,
                null);
        }

        /// <summary>
        ///     Gets a value of the protected property with a specified name defined on a specified target object.
        /// </summary>
        /// <param name="target">
        ///     The reference to the object from which the property value shall be obtained.
        /// </param>
        /// <param name="name">
        ///     The name of the property which value shall be obtained.
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
        public static Exception GetProtectedPropertyValueWithException(this object target, string name)
        {
            if (object.ReferenceEquals(target, null))
            {
                throw new ArgumentNullException(nameof(target));
            }

            if (object.ReferenceEquals(name, null))
            {
                throw new ArgumentNullException(nameof(name));
            }

            var type = target.GetType();

            try
            {
                type.InvokeMember(
                    name,
                    BindingFlags.GetProperty | BindingFlags.NonPublic | BindingFlags.Instance,
                    Type.DefaultBinder,
                    target,
                    null);
            }
            catch (TargetInvocationException e)
            {
                return e.InnerException;
            }

            throw new InvalidOperationException(TestResources.Exception_InvalidOperation_NoExpectedException);
        }
    }
}
