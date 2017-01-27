//-----------------------------------------------------------------------
// <copyright file="ArraySegmentExtensionsSpecification{T}.cs" company="Leet">
//     Copyright (c) Leet. All rights reserved.
//     Licensed under the MIT License.
//     See License.txt in the project root for full license information.
// </copyright>
//-----------------------------------------------------------------------

namespace Leet.Specifications
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Leet;
    using Ploeh.AutoFixture;
    using Xunit;

    /// <summary>
    ///     A class that defines tests for <see cref="ArraySegmentExtensions"/> class.
    /// </summary>
    /// <typeparam name="T">
    ///     Type of the array segment items.
    /// </typeparam>
    public abstract class ArraySegmentExtensionsSpecification<T>
    {
        /// <summary>
        ///     Checks whether the <see cref="ArraySegmentExtensions.ToEnumerable{T}(ArraySegment{T})"/> method returns an empty collection
        ///     for <see cref="ArraySegment{T}"/> created for <see langword="null"/> array.
        /// </summary>
        [Fact]
        public void ToEnumerable_ArraySegmentOfT_ForSegmentOfNullArray_ReturnsEmptyCollection()
        {
            // Fixture setup
            ArraySegment<T> sut = default(ArraySegment<T>);

            // Exercise system
            IEnumerable<T> result = sut.ToEnumerable();

            // Verify outcome
            Assert.NotNull(result);
            Assert.False(result.Any());

            // Teardown
        }

        /// <summary>
        ///     Checks whether the <see cref="ArraySegmentExtensions.ToEnumerable{T}(ArraySegment{T})"/> method returns an empty collection
        ///     for zero length <see cref="ArraySegment{T}"/>.
        /// </summary>
        /// <param name="arraySize">
        ///     Size of the array for which an <see cref="ArraySegment{T}"/> is created.
        /// </param>
        /// <param name="arrayIndex">
        ///     Index at which the segment shall begin.
        /// </param>
        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 0)]
        [InlineData(1, 1)]
        [InlineData(3, 1)]
        public void ToEnumerable_ArraySegmentOfT_ForEmptyArraySegment_ReturnsEmptyCollection(int arraySize, int arrayIndex)
        {
            // Fixture setup
            IFixture fixture = new DomainFixture();
            T[] array = fixture.CreateMany<T>(arraySize).ToArray();
            ArraySegment<T> sut = new ArraySegment<T>(array, arrayIndex, 0);
            
            // Exercise system
            IEnumerable<T> result = sut.ToEnumerable();

            // Verify outcome
            Assert.NotNull(result);
            Assert.False(result.Any());

            // Teardown
        }

        /// <summary>
        ///     Checks whether the <see cref="ArraySegmentExtensions.ToEnumerable{T}(ArraySegment{T})"/> method returns correct collection
        ///     for non-empty <see cref="ArraySegment{T}"/>.
        /// </summary>
        /// <param name="arraySize">
        ///     Size of the array for which an <see cref="ArraySegment{T}"/> is created.
        /// </param>
        /// <param name="arrayIndex">
        ///     Index at which the segment shall begin.
        /// </param>
        /// <param name="segmentSize">
        ///     Number of elements contains in the <see cref="ArraySegment{T}"/>.
        /// </param>
        [Theory]
        [InlineData(1, 0, 1)]
        [InlineData(2, 0, 1)]
        [InlineData(2, 0, 2)]
        [InlineData(2, 1, 1)]
        [InlineData(3, 0, 3)]
        [InlineData(3, 1, 1)]
        public void ToEnumerable_ArraySegmentOfT_ForNonEmptyArraySegment_ReturnsSequenceOfArraySegmentElements(int arraySize, int arrayIndex, int segmentSize)
        {
            // Fixture setup
            IFixture fixture = new DomainFixture();
            T[] array = fixture.CreateMany<T>(arraySize).ToArray();
            ArraySegment<T> sut = new ArraySegment<T>(array, arrayIndex, segmentSize);
            IEnumerable<T> expectedSequence = array.Skip(arrayIndex).Take(segmentSize);

            // Exercise system
            IEnumerable<T> result = sut.ToEnumerable();

            // Verify outcome
            Assert.NotNull(result);
            Assert.True(result.Any());
            Assert.Equal(segmentSize, result.Count());
            Assert.True(result.SequenceEqual(expectedSequence));

            // Teardown
        }
    }
}
