// -----------------------------------------------------------------------
// <copyright file="SequenceEqualityComparerSpecification{TSut,T}.cs" company="Leet">
//     Copyright (c) Leet. All rights reserved.
//     Licensed under the MIT License.
//     See License.txt in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Leet.Specifications
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Leet;
    using NSubstitute;
    using Ploeh.AutoFixture;
    using Xunit;

    /// <summary>
    ///     A class that specifies behavior for <see cref="SequenceEqualityComparer{T}"/> class.
    /// </summary>
    /// <typeparam name="TSut">
    ///     Type which shall be tested for conformance with behavior defined for <see cref="SequenceEqualityComparer{T}"/> class.
    /// </typeparam>
    /// <typeparam name="T">
    ///     The type of objects contained in the <see cref="IEnumerable{T}"/> collection being compared
    ///     by the <see cref="SequenceEqualityComparer{T}"/> class.
    /// </typeparam>
    public abstract class SequenceEqualityComparerSpecification<TSut, T> : ObjectSpecification<TSut>
        where TSut : SequenceEqualityComparer<T>
    {
        /// <summary>
        ///     Checks whether <see cref="SequenceEqualityComparer{T}.Equals(IEnumerable{T}, IEnumerable{T})"/> method returns
        ///     <see langword="true"/> if called with two collection of different type, but same values.
        /// </summary>
        /// <param name="items">
        ///     Enumerable collection of items to compare.
        /// </param>
        [Theory]
        [AutoDomainData]
        public void Equals_IEnumerableOfT_IEnumerableOfT_ForEnumerableAndArrayWithSameElements_ReturnsTrue(IEnumerable<T> items)
        {
            // Fixture setup
            SequenceEqualityComparer<T> sut = new SequenceEqualityComparer<T>();
            T[] array = items.ToArray();

            // Exercise system
            // Verify outcome
            Assert.True(sut.Equals(items, array));

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="SequenceEqualityComparer{T}.Equals(IEnumerable{T}, IEnumerable{T})"/> method
        ///     always evaluates items equality till first diferent items.
        /// </summary>
        /// <param name="collectionSize">
        ///     Size of the larger collection.
        /// </param>
        /// <param name="subcollectionSize">
        ///     Size of the smaller collection.
        /// </param>
        [Theory]
        [InlineData(1, 0)]
        [InlineData(2, 0)]
        [InlineData(2, 1)]
        [InlineData(3, 2)]
        public void Equals_IEnumerableOfT_IEnumerableOfT_ForFirstCollectionBeingSubcolectionOfSecond_ReturnsFalse(int collectionSize, int subcollectionSize)
        {
            // Fixture setup
            IFixture fixture = new DomainFixture();
            SequenceEqualityComparer<T> sut = fixture.Create<SequenceEqualityComparer<T>>();
            IEnumerable<T> collection = fixture.CreateMany<T>(collectionSize);
            IEnumerable<T> subcollection = collection.Take(subcollectionSize);

            // Exercise system
            // Verify outcome
            Assert.False(sut.Equals(subcollection, collection));

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="SequenceEqualityComparer{T}.Equals(IEnumerable{T}, IEnumerable{T})"/> method
        ///     always evaluates items equality till first diferent items.
        /// </summary>
        /// <param name="collectionSize">
        ///     Size of the larger collection.
        /// </param>
        /// <param name="subcollectionSize">
        ///     Size of the smaller collection.
        /// </param>
        [Theory]
        [InlineData(1, 0)]
        [InlineData(2, 0)]
        [InlineData(2, 1)]
        [InlineData(3, 1)]
        public void Equals_IEnumerableOfT_IEnumerableOfT_ForSecondCollectionBeingSubcolectionOfFirst_ReturnsFalse(int collectionSize, int subcollectionSize)
        {
            // Fixture setup
            IFixture fixture = new DomainFixture();
            SequenceEqualityComparer<T> sut = fixture.Create<SequenceEqualityComparer<T>>();
            IEnumerable<T> collection = fixture.CreateMany<T>(collectionSize);
            IEnumerable<T> subcollection = collection.Take(subcollectionSize);

            // Exercise system
            // Verify outcome
            Assert.False(sut.Equals(collection, subcollection));

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="SequenceEqualityComparer{T}.Equals(IEnumerable{T}, IEnumerable{T})"/> method
        ///     always evaluates items equality using item comparer.
        /// </summary>
        /// <param name="collectionsSize">
        ///     Size of the collections to compare.
        /// </param>
        /// <param name="differentItemsIndex">
        ///     Index at which a different item shall be located.
        /// </param>
        [Theory]
        [InlineData(1, 0)]
        [InlineData(2, 0)]
        [InlineData(2, 1)]
        [InlineData(3, 1)]
        public void Equals_IEnumerableOfT_IEnumerableOfT_Always_EvaluatesBasedOnItemComparer(int collectionsSize, int differentItemsIndex)
        {
            // Fixture setup
            IFixture fixture = new DomainFixture();
            IEnumerable<T> first = fixture.CreateMany<T>(collectionsSize);
            IEnumerable<T> second = first.ToArray();
            int itemIndex = 0;
            IEqualityComparer<T> itemsComparer = fixture.Create<IEqualityComparer<T>>();
            itemsComparer.Equals(Arg.Any<T>(), Arg.Any<T>()).Returns(y => itemIndex++ != differentItemsIndex);
            SequenceEqualityComparer<T> sut = new SequenceEqualityComparer<T>(itemsComparer);

            // Exercise system
            // Verify outcome
            Assert.False(sut.Equals(first, second));

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="SequenceEqualityComparer{T}.Equals(IEnumerable{T}, IEnumerable{T})"/> method
        ///     always evaluates items equality till first diferent items.
        /// </summary>
        /// <param name="collectionsSize">
        ///     Size of the collections to compare.
        /// </param>
        /// <param name="differentItemsIndex">
        ///     Index at which a different item shall be located.
        /// </param>
        [Theory]
        [InlineData(1, 0)]
        [InlineData(2, 0)]
        [InlineData(2, 1)]
        [InlineData(3, 1)]
        public void Equals_IEnumerableOfT_IEnumerableOfT_Always_EvaluatesTillFirstDifference(int collectionsSize, int differentItemsIndex)
        {
            // Fixture setup
            IFixture fixture = new DomainFixture();
            IEnumerable<T> first = fixture.CreateMany<T>(collectionsSize);
            IEnumerable<T> second = first.ToArray();
            int itemIndex = 0;
            IEqualityComparer<T> itemsComparer = fixture.Create<IEqualityComparer<T>>();
            itemsComparer.Equals(Arg.Any<T>(), Arg.Any<T>()).Returns(y => itemIndex++ != differentItemsIndex);
            SequenceEqualityComparer<T> sut = new SequenceEqualityComparer<T>(itemsComparer);

            // Exercise system
            sut.Equals(first, second);

            // Verify outcome
            Assert.Equal(differentItemsIndex + 1, itemIndex);

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="SequenceEqualityComparer{T}.Equals(IEnumerable{T}, IEnumerable{T})"/> method
        ///     always evaluates items equality till first diferent items.
        /// </summary>
        /// <param name="collection">
        ///     A collection instance to compare to itself.
        /// </param>
        [Theory]
        [AutoDomainData]
        public void Equals_IEnumerableOfT_IEnumerableOfT_ForSameReferences_ReturnsTrue(IEnumerable<T> collection)
        {
            // Fixture setup
            IFixture fixture = new DomainFixture();
            IEqualityComparer<T> itemsComparer = fixture.Create<IEqualityComparer<T>>();
            itemsComparer.Equals(Arg.Any<T>(), Arg.Any<T>()).Returns(y => false);
            SequenceEqualityComparer<T> sut = new SequenceEqualityComparer<T>(itemsComparer);

            // Exercise system
            // Verify outcome
            Assert.True(sut.Equals(collection, collection));

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="SequenceEqualityComparer{T}.Equals(IEnumerable{T}, IEnumerable{T})"/> method
        ///     always evaluates items equality till first diferent items.
        /// </summary>
        /// <param name="collection">
        ///     A collection instance to compare to itself.
        /// </param>
        [Theory]
        [AutoDomainData]
        public void Equals_IEnumerableOfT_IEnumerableOfT_ForSameInstances_NeverComparesItems(IEnumerable<T> collection)
        {
            // Fixture setup
            IFixture fixture = new DomainFixture();
            IEqualityComparer<T> itemsComparer = fixture.Create<IEqualityComparer<T>>();
            itemsComparer.Equals(Arg.Any<T>(), Arg.Any<T>()).Returns(y => false);
            SequenceEqualityComparer<T> sut = new SequenceEqualityComparer<T>(itemsComparer);

            // Exercise system
            sut.Equals(collection, collection);

            // Verify outcome
            itemsComparer.DidNotReceive().Equals(Arg.Any<T>(), Arg.Any<T>());

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="SequenceEqualityComparer{T}.Equals(IEnumerable{T}, IEnumerable{T})"/> method
        ///     always evaluates items equality till first diferent items.
        /// </summary>
        /// <param name="collection">
        ///     A collection instance to compare to itself.
        /// </param>
        [Theory]
        [AutoDomainData]
        public void Equals_IEnumerableOfT_IEnumerableOfT_ForSameSequences_ReturnsAccordingToItemComparer(IEnumerable<T> collection)
        {
            // Fixture setup
            IFixture fixture = new DomainFixture();
            IEqualityComparer<T> itemsComparer = fixture.Create<IEqualityComparer<T>>();
            itemsComparer.Equals(Arg.Any<T>(), Arg.Any<T>()).Returns(y => false);
            SequenceEqualityComparer<T> sut = new SequenceEqualityComparer<T>(itemsComparer);
            T[] first = collection.ToArray();
            T[] second = collection.ToArray();

            // Exercise system
            // Verify outcome
            Assert.False(sut.Equals(first, second));

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="SequenceEqualityComparer{T}(IEqualityComparer{T})"/> constructor throws
        ///     an <see cref="ArgumentNullException"/> when called with <see langword="null"/> item's comparer.
        /// </summary>
        [Fact]
        internal void Constructor_IEqualityComparerOfT_ForNullItemComparer_Throws()
        {
            // Fixture setup
            IEqualityComparer<T> itemComparer = null;

            // Exercise system
            // Verify outcome
            Assert.Throws<ArgumentNullException>(nameof(itemComparer), () =>
            {
                new SequenceEqualityComparer<T>(itemComparer);
            });

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="SequenceEqualityComparer{T}(IEqualityComparer{T})"/> constructor assigns its parameter to
        ///     <see cref="SequenceEqualityComparer{T}.ItemComparer"/> property.
        /// </summary>
        /// <param name="itemComparer">
        ///     A collection item's comparer.
        /// </param>
        [Theory]
        [AutoDomainData]
        internal void Constructor_IEqualityComparerOfT_Always_AssignsItemComparer(IEqualityComparer<T> itemComparer)
        {
            // Fixture setup
            SequenceEqualityComparer<T> sut = new SequenceEqualityComparer<T>(itemComparer);

            // Exercise system
            IEqualityComparer<T> storedComparer = sut.ItemComparer;

            // Verify outcome
            Assert.Same(itemComparer, storedComparer);

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="SequenceEqualityComparer{T}()"/> constructor assigns default
        ///     comparer for <typeparamref name="T"/> type to <see cref="SequenceEqualityComparer{T}.ItemComparer"/> property.
        /// </summary>
        [Fact]
        internal void Constructor_Void_Always_AssignsItemComparer()
        {
            // Fixture setup
            SequenceEqualityComparer<T> sut = new SequenceEqualityComparer<T>();

            // Exercise system
            IEqualityComparer<T> storedComparer = sut.ItemComparer;

            // Verify outcome
            Assert.Same(EqualityComparer<T>.Default, storedComparer);

            // Teardown
        }
    }
}
