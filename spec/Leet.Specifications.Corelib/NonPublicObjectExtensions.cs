//-----------------------------------------------------------------------
// <copyright file="NonPublicObjectExtensions.cs" company="Leet">
//     © 2016 Leet. Licensed under the MIT License.
//     See License.txt in the project root for license information.
// </copyright>
//-----------------------------------------------------------------------

namespace Leet
{
    using System;
    using System.Linq;
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
        ///     Method invokation return value.
        /// </returns>
        public static object InvokeProtectedMethod(this object target, string name, params object[] args)
        {
            var type = target.GetType();
            var method = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
                             .Where(x => x.Name == name).Single();
            return method.Invoke(target, args);
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
        public static Exception InvokeProtectedMethodWithException(this object target, string name, params object[] args)
        {
            var type = target.GetType();
            var method = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
                             .Where(x => x.Name == name).Single();
            try
            {
                method.Invoke(target, args);
            }
            catch (TargetInvocationException e)
            {
                return e.InnerException;
            }

            throw new InvalidOperationException(SpecificationResources.Exception_InvalidOperation_NoExpectedException);
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
        public static object GetProtectedPropertyValue(this object target, string name)
        {
            var type = target.GetType();
            var property = type.GetProperties(BindingFlags.NonPublic | BindingFlags.Instance)
                             .Where(x => x.Name == name).Single();
            return property.GetValue(target);
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
        public static Exception GetProtectedPropertyValueWithException(this object target, string name)
        {
            var type = target.GetType();
            var property = type.GetProperties(BindingFlags.NonPublic | BindingFlags.Instance)
                             .Where(x => x.Name == name).Single();

            try
            {
                property.GetValue(target);
            }
            catch (TargetInvocationException e)
            {
                return e.InnerException;
            }

            throw new InvalidOperationException(SpecificationResources.Exception_InvalidOperation_NoExpectedException);
        }
    }
}
