//-----------------------------------------------------------------------
// <copyright file="ObjectExtensionsSpecification{T}.cs" company="Leet">
//     Copyright (c) Leet. All rights reserved.
//     Licensed under the MIT License.
//     See License.txt in the project root for full license information.
// </copyright>
//-----------------------------------------------------------------------

namespace Leet.Specifications
{
    using System;
    using Xunit;
    using Leet;

    /// <summary>
    ///     A class that specifies behavior for <see cref="ObjectExtensions"/> class.
    /// </summary>
    /// <typeparam name="T">
    ///     Type of the object to test.
    /// </typeparam>
    public abstract class ObjectExtensionsSpecification<T>
    {
        /// <summary>
        ///     Checks whether <see cref="ObjectExtensions.GetHashCodeAllowNull(object)"/> method returns zero for default instance.
        /// </summary>
        [Fact]
        public void GetHashCodeAllowNull_ForDefaultInstance_ReturnsZero()
        {
            // Fixture setup
            T sut = default(T);

            // Exercise system
            int result = sut.GetHashCodeAllowNull();

            // Verify outcome
            Assert.Equal(0, result);

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="ObjectExtensions.GetHashCodeAllowNull(object)"/> method returns same result as
        ///     <see cref="object.GetHashCode"/> method for non-<see langword="null"/> reference.
        /// </summary>
        /// <param name="sut">
        ///     Object under test.
        /// </param>
        [Theory]
        [AutoDomainData]
        public void GetHashCodeAllowNull_ForNonNullReference_ReturnsSameResultAsGetHashCode(T sut)
        {
            // Fixture setup
            int expected = sut.GetHashCode();

            // Exercise system
            int result = sut.GetHashCodeAllowNull();

            // Verify outcome
            Assert.Equal(expected, result);

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="ObjectExtensions.ToStringAllowNull(object)"/> method returns <param name="nullRepresentation"/>
        ///     for default instance of the reference type and result same as <see cref="object.ToString"/> for structures.
        /// </summary>
        /// <param name="nullRepresentation">
        ///     A value that shall be returned if the specified object is a <see langword="null"/> reference.
        /// </param>
        [Theory]
        [AutoDomainData]
        public void ToStringAllowNull_ForDefaultInstance_ReturnsNullRepresentationForReferenceTypeAndResultOfToStringForStructure(string nullRepresentation)
        {
            // Fixture setup
            T sut = default(T);
            string expected = object.ReferenceEquals(sut, null) ? nullRepresentation : sut.ToString();

            // Exercise system
            string result = sut.ToStringAllowNull(nullRepresentation);

            // Verify outcome
            Assert.NotNull(result);
            Assert.Equal(expected, result);

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="ObjectExtensions.ToStringAllowNull(object)"/> method returns same result
        ///     as <see cref="object.ToString"/> for non-<see langword="null"/> references.
        /// </summary>
        /// <param name="sut">
        ///     Object under test.
        /// </param>
        /// <param name="nullRepresentation">
        ///     A value that shall be returned if the specified object is a <see langword="null"/> reference.
        /// </param>
        [Theory]
        [AutoDomainData]
        public void ToStringAllowNull_ForNonNullReference_ReturnsSameResultAsToString(T sut, string nullRepresentation)
        {
            // Fixture setup
            string expected = sut.ToString();

            // Exercise system
            string result = sut.ToStringAllowNull(nullRepresentation);

            // Verify outcome
            Assert.NotNull(result);
            Assert.Equal(expected, result);

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="ObjectExtensions.ToStringAllowNull(object)"/> method returns <see langword="null"/>
        ///     for <see langword="null"/> reference and <see langword="null"/> representation.
        /// </summary>
        [Fact]
        public void ToStringAllowNull_ForNullReferenceAndNullRepresentation_ReturnsNull()
        {
            // Fixture setup
            T sut = default(T);
            string expected = object.ReferenceEquals(sut, null) ? null : sut.ToString();

            // Exercise system
            string result = sut.ToStringAllowNull(null);

            // Verify outcome
            Assert.Equal(expected, result);

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="ObjectExtensions.ToStringAllowNull(object)"/> method returns result same as
        ///     <see cref="object.ToString"/> for non-<see langword="null"/> reference and <see langword="null"/> representation.
        /// </summary>
        /// <param name="sut">
        ///     Object under test.
        /// </param>
        [Theory]
        [AutoDomainData]
        public void ToStringAllowNull_ForNonNullReferenceAndNullRepresentation_ReturnsResultSameAsToString(T sut)
        {
            // Fixture setup
            string expected = sut.ToString();

            // Exercise system
            string result = sut.ToStringAllowNull(null);

            // Verify outcome
            Assert.NotNull(result);
            Assert.Equal(expected, result);

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="ObjectExtensions.GetTypeAllowNull(object)"/> method returns <see langword="null"/>
        ///     for <see langword="null"/> reference.
        /// </summary>
        [Fact]
        public void GetTypeAllowNull_ForNullReference_ReturnsNull()
        {
            // Fixture setup
            T sut = default(T);
            Type expected = object.ReferenceEquals(sut, null) ? null : sut.GetType();

            // Exercise system
            Type result = sut.GetTypeAllowNull();

            // Verify outcome
            Assert.Equal(expected, result);

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="ObjectExtensions.GetTypeAllowNull(object)"/> method returns result same as
        ///     <see cref="object.GetType"/> for non-<see langword="null"/> reference.
        /// </summary>
        /// <param name="sut">
        ///     Object under test.
        /// </param>
        [Theory]
        [AutoDomainData]
        public void GetTypeAllowNull_ForNonNullReference_ReturnsSameResultAsGetType(T sut)
        {
            // Fixture setup
            Type expectedType = sut.GetType();

            // Exercise system
            Type result = sut.GetTypeAllowNull();

            // Verify outcome
            Assert.NotEqual(null, result);
            Assert.Equal(expectedType, result);

            // Teardown
        }
    }
}
