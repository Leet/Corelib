//-----------------------------------------------------------------------
// <copyright file="DisposableBaseSpecification{TSut}.cs" company="Leet">
//     © 2016 Leet. Licensed under the MIT License.
//     See License.txt in the project root for license information.
// </copyright>
//-----------------------------------------------------------------------

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
    public abstract partial class DisposableBaseSpecification<TSut>
        : ObjectSpecification<TSut>
        where TSut : DisposableBase
    {
        /// <summary>
        ///     Gets a value that determines whether the <typeparamref name="TSut"/> has finalizer defined which behavior
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
        ///     Checks whether the construction of the <typeparamref name="TSut"/> object does not call
        ///     <see cref="DisposableBase.Dispose"/> method.
        /// </summary>
        [Fact]
        public void Dispose_IsNeverCalled_UponConstruction()
        {
            // Fixture setup
            TSut sut = Substitute.For<TSut>();

            // Exercise system

            // Verify outcome
            Assert.False((bool)sut.GetProtectedPropertyValue("IsDisposed"));
            sut.DidNotReceive().InvokeProtectedMethod("Dispose", true);
            sut.DidNotReceive().InvokeProtectedMethod("Dispose", false);

            // Teardown
        }

        /// <summary>
        ///     Checks whether calling <see cref="DisposableBase.Dispose"/> for the first time calls
        ///     <see cref="DisposableBase.Dispose(bool)"/> method with <see langword="true"/> argument.
        /// </summary>
        [Fact]
        public void Dispose_CalledFirstTime_CallsDisposeTrue()
        {
            // Fixture setup
            TSut sut = Substitute.For<TSut>();

            // Exercise system
            sut.Dispose();

            // Verify outcome
            Assert.True((bool)sut.GetProtectedPropertyValue("IsDisposed"));
            sut.Received(1).InvokeProtectedMethod("Dispose", true);
            sut.DidNotReceive().InvokeProtectedMethod("Dispose", false);

            // Teardown
        }

        /// <summary>
        ///     Checks whether calling <see cref="DisposableBase.Dispose"/> for the second time calls
        ///     <see cref="DisposableBase.Dispose(bool)"/> method with <see langword="true"/> argument.
        /// </summary>
        [Fact]
        public void Dispose_CalledSecondTime_DoesNotCallProtectedDispose()
        {
            // Fixture setup
            TSut sut = Substitute.For<TSut>();
            sut.Dispose();
            sut.ClearReceivedCalls();

            // Exercise system
            sut.Dispose();

            // Verify outcome
            Assert.True((bool)sut.GetProtectedPropertyValue("IsDisposed"));
            sut.DidNotReceive().InvokeProtectedMethod("Dispose", true);
            sut.DidNotReceive().InvokeProtectedMethod("Dispose", false);

            // Teardown
        }

        /// <summary>
        ///     Checks whether calling <see cref="DisposableBase.ThrowIfDisposed"/> throws an <see cref="ObjectDisposedException"/>
        ///     when called on disposed object.
        /// </summary>
        [Fact]
        public void ThrowIfDisposed_ForDisposedInstance_Throws()
        {
            // Fixture setup
            TSut sut = Substitute.For<TSut>();
            sut.Dispose();

            // Exercise system
            Exception e = sut.InvokeProtectedMethodWithException("ThrowIfDisposed");
            ObjectDisposedException ode = e as ObjectDisposedException;

            // Verify outcome
            Assert.Equal(e.GetType(), typeof(ObjectDisposedException));
            Assert.Equal(ode.ObjectName, sut.GetType().FullName);
            Assert.Equal(ode.Message, $"Cannot access a disposed object.{Environment.NewLine}Object name: '{sut.GetType().FullName}'.");
            Assert.Null(ode.InnerException);
            Assert.True((bool)sut.GetProtectedPropertyValue("IsDisposed"));
            sut.Received(1).InvokeProtectedMethod("Dispose", true);
            sut.DidNotReceive().InvokeProtectedMethod("Dispose", false);

            // Teardown
        }

        /// <summary>
        ///     Checks whether calling <see cref="DisposableBase.ThrowIfDisposed"/> does not throw an <see cref="Exception"/>
        ///     when called on not disposed object.
        /// </summary>
        [Fact]
        public void ThrowIfDisposed_ForNotDisposedInstance_DoesNotThrow()
        {
            // Fixture setup
            TSut sut = Substitute.For<TSut>();

            // Exercise system
            sut.InvokeProtectedMethod("ThrowIfDisposed");

            // Verify outcome
            Assert.False((bool)sut.GetProtectedPropertyValue("IsDisposed"));
            sut.DidNotReceive().InvokeProtectedMethod("Dispose", true);
            sut.DidNotReceive().InvokeProtectedMethod("Dispose", false);

            // Teardown
        }

        /// <summary>
        ///     Checks whether calling <see cref="DisposableBase.ThrowIfDisposed"/> throws an <see cref="ObjectDisposedException"/>
        ///     when called on object disposed twice.
        /// </summary>
        [Fact]
        public void ThrowIfDisposed_ForInstanceDisposedTwice_Throws()
        {
            // Fixture setup
            TSut sut = Substitute.For<TSut>();
            sut.Dispose();
            sut.Dispose();

            // Exercise system
            Exception e = sut.InvokeProtectedMethodWithException("ThrowIfDisposed");
            ObjectDisposedException ode = e as ObjectDisposedException;

            // Verify outcome
            Assert.Equal(e.GetType(), typeof(ObjectDisposedException));
            Assert.Equal(ode.ObjectName, sut.GetType().FullName);
            Assert.Equal(ode.Message, $"Cannot access a disposed object.{Environment.NewLine}Object name: '{sut.GetType().FullName}'.");
            Assert.Null(ode.InnerException);
            Assert.True((bool)sut.GetProtectedPropertyValue("IsDisposed"));
            sut.Received(1).InvokeProtectedMethod("Dispose", true);
            sut.DidNotReceive().InvokeProtectedMethod("Dispose", false);
            
            // Teardown
        }

        /// <summary>
        ///     Checks whether an object finalizer always calls dispose method with <see langword="false"/> parameter.
        /// </summary>
        [Fact]
        public void Finalize_Always_CallsDisposeFalse()
        {
            // Fixture setup
            TSut sut = Substitute.For<TSut>();
            int actualDispositionCount = 0;
            int actualFinalizationCount = 0;
            sut.When(disposableBase => disposableBase.InvokeProtectedMethod("Dispose", true)).Do(x => ++actualDispositionCount);
            sut.When(disposableBase => disposableBase.InvokeProtectedMethod("Dispose", false)).Do(x => ++actualFinalizationCount);
            int expectedDispositionCount = 0;
            int expectedFinalizationCount = this.HasFinalizer ? 1 : 0;

            // Exercise system
            sut = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();

            // Verify outcome
            Assert.Equal(expectedDispositionCount, actualDispositionCount);
            Assert.Equal(expectedFinalizationCount, actualFinalizationCount);

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="IDisposable.Dispose"/> method always supresses object finalization.
        /// </summary>
        [Fact]
        public void Dispose_Always_SpressesFinalization()
        {
            // Fixture setup
            TSut sut = Substitute.For<TSut>();
            int actualDispositionCount = 0;
            int actualFinalizationCount = 0;
            sut.When(disposableBase => disposableBase.InvokeProtectedMethod("Dispose", true)).Do(x => ++actualDispositionCount);
            sut.When(disposableBase => disposableBase.InvokeProtectedMethod("Dispose", false)).Do(x => ++actualFinalizationCount);
            int expectedDispositionCount = 1;
            int expectedFincalizationCount = 0;

            // Exercise system
            sut.Dispose();
            sut = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();

            // Verify outcome
            Assert.Equal(expectedDispositionCount, actualDispositionCount);
            Assert.Equal(expectedFincalizationCount, actualFinalizationCount);

            // Teardown
        }
    }
}
