// -----------------------------------------------------------------------
// <copyright file="IEquatableSpecification{TSut,T}.cs" company="Leet">
//     Copyright (c) Leet. All rights reserved.
//     Licensed under the MIT License.
//     See License.txt in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Leet.Specifications
{
    using System;
    using System.Reflection;
    using Xunit;

    /// <summary>
    ///     A class that specifies behavior for <see cref="IEquatable{T}"/> interface.
    /// </summary>
    /// <typeparam name="TSut">
    ///     Type which shall be tested for conformance with behavior defined for <see cref="IEquatable{T}"/> class.
    /// </typeparam>
    /// <typeparam name="T">
    ///     The type of objects to compare.
    /// </typeparam>
    public abstract class IEquatableSpecification<TSut, T>
        where TSut : IEquatable<T>
    {
        /// <summary>
        ///     Checks whether <see cref="IEquatable{T}.Equals(T)"/> method returns correct result
        ///     for default instance parameter.
        /// </summary>
        /// <param name="sut">
        ///     Object under test.
        /// </param>
        [Theory]
        [AutoDomainData]
        public void Equals_T_ForDefaultInstance_ReturnsCorrectResult(TSut sut)
        {
            // Fixture setup
            T other = default(T);

            // Exercise system
            bool result = sut.Equals(other);

            // Verify outcome
            Assert.True(!object.ReferenceEquals(other, null) || !result);

            // Teardown
        }

        /// <summary>
        ///     Checks whether <typeparamref name="TSut"/> defaines a <see langword="static"/> <c>Equals(TSut,T)</c> method.
        /// </summary>
        [Fact]
        public void Type_Defines_StaticEqualsTSutTMethod()
        {
            // Fixture setup
            Type sutType = typeof(TSut);

            // Exercise system
            MethodInfo method = sutType.GetMethod(
                "Equals",
                BindingFlags.Static | BindingFlags.Public,
                Type.DefaultBinder,
                new Type[] { typeof(TSut), typeof(T) },
                null);

            // Verify outcome
            Assert.NotNull(method);

            // Teardown
        }

        /// <summary>
        ///     Checks whether <typeparamref name="TSut"/> defaines a <see langword="static"/> <c>Equals(T,TSut)</c> method.
        /// </summary>
        [Fact]
        public void Type_Defines_StaticEqualsTTSutMethod()
        {
            // Fixture setup
            Type sutType = typeof(TSut);

            // Exercise system
            MethodInfo method = sutType.GetMethod(
                "Equals",
                BindingFlags.Static | BindingFlags.Public,
                Type.DefaultBinder,
                new Type[] { typeof(T), typeof(TSut) },
                null);

            // Verify outcome
            Assert.NotNull(method);

            // Teardown
        }

        /// <summary>
        ///     Checks whether <c>TSut.Equals(TSut,T)</c> method returns correct result
        ///     for default instance parameter.
        /// </summary>
        /// <param name="sut">
        ///     Object under test.
        /// </param>
        [Theory]
        [AutoDomainData]
        public void StaticEquals_TSut_T_ForDefaultInstance_ReturnsCorrectResult(TSut sut)
        {
            // Fixture setup
            Type sutType = typeof(TSut);
            T other = default(T);

            // Exercise system
            bool result = (bool)sutType.InvokePublicMethod("Equals", sut, other);

            // Verify outcome
            Assert.True(!object.ReferenceEquals(other, null) || !result);

            // Teardown
        }

        /// <summary>
        ///     Checks whether <c>TSut.Equals(T,TSut)</c> method returns correct result
        ///     for default instance parameter.
        /// </summary>
        /// <param name="sut">
        ///     Object under test.
        /// </param>
        [Theory]
        [AutoDomainData]
        public void StaticEquals_T_TSut_ForDefaultInstance_ReturnsCorrectResult(TSut sut)
        {
            // Fixture setup
            Type sutType = typeof(TSut);
            T other = default(T);

            // Exercise system
            bool result = (bool)sutType.InvokePublicMethod("Equals", sut, other);

            // Verify outcome
            Assert.True(!object.ReferenceEquals(other, null) || !result);

            // Teardown
        }
    }
}
