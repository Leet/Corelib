//-----------------------------------------------------------------------
// <copyright file="ObjectSpecification{TSut}.cs" company="Leet">
//     Copyright (c) Leet. All rights reserved.
//     Licensed under the MIT License.
//     See License.txt in the project root for full license information.
// </copyright>
//-----------------------------------------------------------------------

namespace Leet.Specifications
{
    using Xunit;

    /// <summary>
    ///     A class that specifies behavior for <see cref="object"/> class.
    /// </summary>
    /// <typeparam name="TSut">
    ///     Type which shall be tested for conformance with behavior defined for <see cref="object"/> class.
    /// </typeparam>
    public abstract class ObjectSpecification<TSut>
    {
        /// <summary>
        ///     Checks whether <see cref="object.ToString()"/> method returns non-<see langword="null"/> reference.
        /// </summary>
        /// <param name="sut">
        ///     Object under test.
        /// </param>
        [Theory]
        [AutoDomainData]
        public void ToString_Void_Always_ReturnsNotNull(TSut sut)
        {
            // Fixture setup

            // Exercise system
            string result = sut.ToString();

            // Verify outcome
            Assert.NotNull(result);

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="object.Equals(object)"/> method returns <see langword="false"/> when called with <see langword="null"/> reference.
        /// </summary>
        /// <param name="sut">
        ///     Object under test.
        /// </param>
        [Theory]
        [AutoDomainData]
        public void Equals_Object_CalledWithNull_ReturnsFalse(TSut sut)
        {
            // Fixture setup

            // Exercise system
            bool result = sut.Equals(null);

            // Verify outcome
            Assert.False(result);

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="object.Equals(object)"/> method returns <see langword="true"/> when called with <see langword="this"/> reference.
        /// </summary>
        /// <param name="sut">
        ///     Object under test.
        /// </param>
        [Theory]
        [AutoDomainData]
        public void Equals_Object_CalledWithThis_ReturnsTrue(TSut sut)
        {
            // Fixture setup

            // Exercise system
            bool result = sut.Equals(sut);

            // Verify outcome
            Assert.True(result);

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="object.GetHashCode"/> method returns always the same value upon subsequent calls.
        /// </summary>
        /// <param name="sut">
        ///     Object under test.
        /// </param>
        [Theory]
        [AutoDomainData]
        public void GetHashCode_Void_Alwyas_ReturnsSameValue(TSut sut)
        {
            // Fixture setup

            // Exercise system
            int firstValue = sut.GetHashCode();
            int secondValue = sut.GetHashCode();

            // Verify outcome
            Assert.Equal(firstValue, secondValue);

            // Teardown
        }
    }
}
