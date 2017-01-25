//-----------------------------------------------------------------------
// <copyright file="EnumerableExtensionsSpecification{T}.cs" company="Leet">
//     Copyright (c) 2016 Leet. Licensed under the MIT License.
//     See License.txt in the project root for license information.
// </copyright>
//-----------------------------------------------------------------------

namespace Leet.Specifications
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Leet;
    using NSubstitute;
    using Ploeh.AutoFixture;
    using Properties;
    using Xunit;

    /// <summary>
    ///     A class that specifies behavior for <see cref="EnumerableExtensions"/> class.
    /// </summary>
    /// <typeparam name="T">
    ///     Type of the object to test.
    /// </typeparam>
    public abstract class EnumerableExtensionsSpecification<T>
    {
        /// <summary>
        ///     Checks whether <see cref="EnumerableExtensions.Iterate{T}(IEnumerable{T})"/> method throws an <see cref="Exception"/>
        ///     when called on <see langword="null"/> reference.
        /// </summary>
        [Fact]
        public void Iterate_ForNullCollection_Throws()
        {
            // Fixture setup
            IEnumerable<T> source = null;

            // Exercise system
            // Verify outcome
            Assert.Throws<ArgumentNullException>(nameof(source), () =>
            {
                source.Iterate();
            });

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="EnumerableExtensions.Iterate{T}(IEnumerable{T})"/> method iterated all the source's collection elements.
        /// </summary>
        /// <param name="collectionSize">
        ///     Size of the collection under test.
        /// </param>
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [AutoDomainData]
        public void Iterate_Always_VisitsAllElements(int collectionSize)
        {
            // Fixture setup
            IFixture fixture = new DomainFixture();
            IEnumerable<T> source = fixture.CreateMany<T>(collectionSize);
            IEnumerator<T> sourceEnumerator = source.GetEnumerator();

            IEnumerator<T> enumeratorMock = Substitute.For<IEnumerator<T>>();
            enumeratorMock.MoveNext().Returns(x => sourceEnumerator.MoveNext());
            IEnumerable<T> enumerable = Substitute.For<IEnumerable<T>>();
            enumerable.GetEnumerator().Returns(enumeratorMock);

            // Exercise system
            enumerable.Iterate();

            // Verify outcome
            enumerable.Received(1).GetEnumerator();
            enumeratorMock.Received(collectionSize + 1).MoveNext();
            enumeratorMock.Received(1).Dispose();

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="EnumerableExtensions.Insert{T}(IEnumerable{T}, int, T)"/> method throws an <see cref="Exception"/>
        ///     when called on <see langword="null"/> reference.
        /// </summary>
        /// <param name="insertAt">
        ///     An index at which a new item should be added.
        /// </param>
        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(1)]
        public void Insert_ForNullCollection_Throws(int insertAt)
        {
            // Fixture setup
            IEnumerable<T> source = null;

            // Exercise system
            // Verify outcome
            Assert.Throws<ArgumentNullException>(nameof(source), () =>
            {
                source.Insert(0, default(T));
            });

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="EnumerableExtensions.Insert{T}(IEnumerable{T}, int, T)"/> method throws
        ///     an <see cref="ArgumentOutOfRangeException"/> when called with index less than zero.
        /// </summary>
        /// <param name="collectionSize">
        ///     Initial number of the elements in the collection.
        /// </param>
        /// <param name="insertAt">
        ///     An index at which a new item should be added.
        /// </param>
        [Theory]
        [InlineData(0, -1)]
        [InlineData(1, int.MinValue)]
        [InlineData(2, -1)]
        [InlineData(3, int.MinValue)]
        public void Insert_CalledWithIndexLessThanZero_Throws(int collectionSize, int insertAt)
        {
            // Fixture setup
            IFixture fixture = new DomainFixture();
            IEnumerable<T> source = fixture.CreateMany<T>(collectionSize);
            T newItem = fixture.Create<T>();

            // Exercise system
            // Verify outcome
            Assert.Throws<ArgumentOutOfRangeException>(nameof(insertAt), () =>
            {
                source.Insert(insertAt, newItem);
            });

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="EnumerableExtensions.Insert{T}(IEnumerable{T}, int, T)"/> method throws
        ///     an <see cref="ArgumentOutOfRangeException"/> when called with index from outside of the collection.
        /// </summary>
        /// <param name="collectionSize">
        ///     Initial number of the elements in the collection.
        /// </param>
        /// <param name="insertAt">
        ///     An index at which a new item should be added.
        /// </param>
        [Theory]
        [InlineData(0, 1)]
        [InlineData(1, 2)]
        [InlineData(2, int.MaxValue)]
        [InlineData(3, int.MaxValue)]
        public void Insert_CalledWithIndexGreaterThanCollectionSizePlusOne_Throws(int collectionSize, int insertAt)
        {
            // Fixture setup
            IFixture fixture = new DomainFixture();
            IEnumerable<T> source = fixture.CreateMany<T>(collectionSize);
            T newItem = fixture.Create<T>();

            // Exercise system
            IEnumerable<T> result = source.Insert(insertAt, newItem);

            // Verify outcome
            Assert.Throws<ArgumentOutOfRangeException>(nameof(insertAt), () =>
            {
                result.Iterate();
            });

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="EnumerableExtensions.Insert{T}(IEnumerable{T}, int, T)"/> method creates a new collection
        ///     with a specified item added.
        /// </summary>
        /// <param name="collectionSize">
        ///     Initial number of the elements in the collection.
        /// </param>
        /// <param name="insertAt">
        ///     An index at which a new item should be added.
        /// </param>
        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 0)]
        [InlineData(1, 1)]
        [InlineData(2, 0)]
        [InlineData(2, 1)]
        [InlineData(2, 2)]
        [InlineData(3, 1)]
        public void Insert_IntoCollection_AddsItem(int collectionSize, int insertAt)
        {
            // Fixture setup
            IFixture fixture = new Fixture().Customize(new DomainCustomization());
            IEnumerable<T> source = fixture.CreateMany<T>(collectionSize);
            T newItem = fixture.Create<T>();
            int expectedCollectionSize = collectionSize + 1;

            // Exercise system
            IEnumerable<T> result = source.Insert(insertAt, newItem);

            // Verify outcome
            Assert.NotNull(result);
            Assert.Equal(expectedCollectionSize, result.Count());
            Assert.True(result.Take(insertAt).SequenceEqual(source.Take(insertAt)));
            Assert.Equal(newItem, result.ElementAt(insertAt));
            Assert.True(result.Skip(insertAt + 1).SequenceEqual(source.Skip(insertAt)));

            // Teardown
        }
    }
}
