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
        ///     Checks whether <see cref="IEquatable{T}.Equals(T)"/> method returns <see langword="true"/>
        ///     for same instance as parameter.
        /// </summary>
        /// <param name="sut">
        ///     Object under test.
        /// </param>
        [Theory]
        [AutoDomainData]
        public void Equals_T_ForSameInstance_ReturnsTrue(TSut sut)
        {
            // Fixture setup

            // Exercise system
            bool result = sut.Equals(sut);

            // Verify outcome
            Assert.True(result);

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="IEquatable{T}.Equals(T)"/> returns same results as
        ///     <see cref="object.Equals(object)"/> implementation.
        /// </summary>
        /// <param name="sut">
        ///     Object under test.
        /// </param>
        /// <param name="other">
        ///     Other instance to compare.
        /// </param>
        [Theory]
        [AutoDomainData]
        public void Equals_T_Always_ReturnsSameResultsAsObjectEquals(TSut sut, TSut other)
        {
            // Fixture setup
            bool expectedResult = sut.Equals((object)other);

            // Exercise system
            bool result = sut.Equals(other);

            // Verify outcome
            Assert.Equal(expectedResult, result);

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="IEquatable{T}.Equals(T)"/> returns same results as
        ///     this same method called on the second instance.
        /// </summary>
        /// <param name="sut">
        ///     Object under test.
        /// </param>
        /// <param name="other">
        ///     Other instance to compare.
        /// </param>
        [Theory]
        [AutoDomainData]
        public void Equals_T_Always_ReturnsSameResultForSwappedInstance(TSut sut, TSut other)
        {
            // Fixture setup
            bool expectedResult = other.Equals(sut);

            // Exercise system
            bool result = sut.Equals(other);

            // Verify outcome
            Assert.Equal(expectedResult, result);

            // Teardown
        }
    }
}
