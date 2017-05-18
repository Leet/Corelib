// -----------------------------------------------------------------------
// <copyright file="DisposableBaseSpecification{TSut}.cs" company="Leet">
//     Copyright (c) Leet. All rights reserved.
//     Licensed under the MIT License.
//     See License.txt in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Leet.Specifications
{
    using System;
    using Leet;
    using NSubstitute;
    using Xunit;

    /// <summary>
    ///     A class that specifies behavior for <see cref="DisposableBase"/> class.
    /// </summary>
    /// <typeparam name="TSut">
    ///     Type which shall be tested for conformance with behavior defined for <see cref="DisposableBase"/> class.
    /// </typeparam>
    public abstract class DisposableBaseSpecification<TSut>
        : ObjectSpecification<TSut>
        where TSut : DisposableBase
    {
        /// <summary>
        ///     Gets a value indicating whether the <typeparamref name="TSut"/> has finalizer defined which behavior
        ///     shall be taken into account.
        /// </summary>
        public virtual bool HasFinalizer
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        ///     Checks whether calling <see cref="DisposableBase.Dispose()"/> for the first time calls
        ///     <see cref="DisposableBase.Dispose(bool)"/> method with <see langword="true"/> argument.
        /// </summary>
        [Fact]
        public void Dispose_Void_CalledFirstTime_CallsDisposeTrue()
        {
            // Fixture setup
            TSut sut = Substitute.For<TSut>();

            // Exercise system
            sut.Dispose();

            // Verify outcome
            sut.Received(1).InvokeProtectedMethod("Dispose", true);

            // Teardown
        }

        /// <summary>
        ///     Checks whether calling <see cref="DisposableBase.Dispose()"/> for the first time does not call
        ///     <see cref="DisposableBase.Dispose(bool)"/> method with <see langword="false"/> argument.
        /// </summary>
        [Fact]
        public void Dispose_Void_CalledFirstTime_DoesNotCallDisposeFalse()
        {
            // Fixture setup
            TSut sut = Substitute.For<TSut>();

            // Exercise system
            sut.Dispose();

            // Verify outcome
            sut.DidNotReceive().InvokeProtectedMethod("Dispose", false);

            // Teardown
        }

        /// <summary>
        ///     Checks whether calling <see cref="DisposableBase.Dispose()"/> for the first time sets <see cref="DisposableBase.IsDisposed"/>
        ///     property to <see langword="true"/>.
        /// </summary>
        [Fact]
        public void Dispose_Void_CalledFirstTime_SetsIsDisposed()
        {
            // Fixture setup
            TSut sut = Substitute.For<TSut>();

            // Exercise system
            sut.Dispose();

            // Verify outcome
            Assert.True((bool)sut.GetProtectedPropertyValue("IsDisposed"));

            // Teardown
        }

        /// <summary>
        ///     Checks whether calling <see cref="DisposableBase.Dispose()"/> for the second time does not call
        ///     <see cref="DisposableBase.Dispose(bool)"/> method with <see cref="bool"/> argument.
        /// </summary>
        [Fact]
        public void Dispose_Void_CalledSecondTime_DoesNotCallProtectedDispose()
        {
            // Fixture setup
            TSut sut = Substitute.For<TSut>();
            sut.Dispose();
            sut.ClearReceivedCalls();

            // Exercise system
            sut.Dispose();

            // Verify outcome
            sut.DidNotReceive().InvokeProtectedMethod("Dispose", Arg.Any<bool>());

            // Teardown
        }

        /// <summary>
        ///     Checks whether calling <see cref="DisposableBase.Dispose()"/> for the second time does not change value of
        ///     <see cref="DisposableBase.IsDisposed"/> property.
        /// </summary>
        [Fact]
        public void Dispose_Void_CalledSecondTime_DoesNotChangeIsDisposedValue()
        {
            // Fixture setup
            TSut sut = Substitute.For<TSut>();
            sut.Dispose();
            sut.ClearReceivedCalls();

            // Exercise system
            sut.Dispose();

            // Verify outcome
            Assert.True((bool)sut.GetProtectedPropertyValue("IsDisposed"));

            // Teardown
        }

        /// <summary>
        ///     Checks whether calling <see cref="DisposableBase.ThrowIfDisposed"/> throws an <see cref="ObjectDisposedException"/>
        ///     when called on disposed object.
        /// </summary>
        [Fact]
        public void ThrowIfDisposed_Void_ForDisposedInstance_Throws()
        {
            // Fixture setup
            TSut sut = Substitute.For<TSut>();
            sut.Dispose();

            // Exercise system
            Assert.Throws<ObjectDisposedException>((Action)(() =>
            {
                throw sut.InvokeProtectedMethodWithException("ThrowIfDisposed");
            }));

            // Teardown
        }

        /// <summary>
        ///     Checks whether calling <see cref="DisposableBase.ThrowIfDisposed"/> does not throw an <see cref="Exception"/>
        ///     when called on not disposed object.
        /// </summary>
        [Fact]
        public void ThrowIfDisposed_Void_ForNotDisposedInstance_DoesNotThrow()
        {
            // Fixture setup
            TSut sut = Substitute.For<TSut>();

            // Exercise system
            // Verify outcome
            sut.InvokeProtectedMethod("ThrowIfDisposed");

            // Teardown
        }

        /// <summary>
        ///     Checks whether an object finalizer always calls dispose method with <see langword="false"/> parameter.
        /// </summary>
        [Fact]
        public void Finalize_Void_Always_CallsDisposeFalse()
        {
            // Fixture setup
            TSut sut = Substitute.For<TSut>();
            int actualFinalizationCount = 0;
            sut.When(disposableBase => disposableBase.InvokeProtectedMethod("Dispose", false)).Do(x => ++actualFinalizationCount);
            int expectedFinalizationCount = this.HasFinalizer ? 1 : 0;

            // Exercise system
            sut = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();

            // Verify outcome
            Assert.Equal(expectedFinalizationCount, actualFinalizationCount);

            // Teardown
        }

        /// <summary>
        ///     Checks whether an object finalizer never calls dispose method with <see langword="true"/> parameter.
        /// </summary>
        [Fact]
        public void Finalize_Void_Never_CallsDisposeTrue()
        {
            // Fixture setup
            TSut sut = Substitute.For<TSut>();
            int actualDispositionCount = 0;
            sut.When(disposableBase => disposableBase.InvokeProtectedMethod("Dispose", true)).Do(x => ++actualDispositionCount);
            int expectedDispositionCount = 0;

            // Exercise system
            sut = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();

            // Verify outcome
            Assert.Equal(expectedDispositionCount, actualDispositionCount);

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="IDisposable.Dispose()"/> method always supresses object finalization.
        /// </summary>
        [Fact]
        public void Dispose_Void_Always_SpressesFinalization()
        {
            // Fixture setup
            TSut sut = Substitute.For<TSut>();
            int actualFinalizationCount = 0;
            sut.When(disposableBase => disposableBase.InvokeProtectedMethod("Dispose", false)).Do(x => ++actualFinalizationCount);
            int expectedFincalizationCount = 0;

            // Exercise system
            sut.Dispose();
            sut = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();

            // Verify outcome
            Assert.Equal(expectedFincalizationCount, actualFinalizationCount);

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="DisposableBase"/> constructor does not sets <see cref="DisposableBase.IsDisposed"/>
        ///     property.
        /// </summary>
        [Fact]
        internal void Constructor_Void_Never_CallsSetIsDisposed()
        {
            // Fixture setup
            // Exercise system
            DisposableBase sut = Substitute.For<DisposableBase>();

            // Verify outcome
            Assert.False((bool)sut.GetProtectedPropertyValue("IsDisposed"));

            // Teardown
        }
    }
}
