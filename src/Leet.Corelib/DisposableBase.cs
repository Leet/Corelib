//-----------------------------------------------------------------------
// <copyright file="DisposableBase.cs" company="Leet">
//     Copyright (c) Leet. All rights reserved.
//     Licensed under the MIT License.
//     See License.txt in the project root for full license information.
// </copyright>
//-----------------------------------------------------------------------

namespace Leet
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Diagnostics.Contracts;
    using System.Threading;

    /// <summary>
    ///     An <see langword="abstract"/> class that provides basic implementation of <see cref="IDisposable"/> interface.
    /// </summary>
    public abstract class DisposableBase : IDisposable
    {
        /// <summary>
        ///     Holds a value that indicates whether current instance has been disposed.
        /// </summary>
        private int isDisposed;

        /// <summary>
        ///     Initializes a new instance of the <see cref="DisposableBase"/> class.
        /// </summary>
        protected DisposableBase()
        {
            Contract.Ensures(!this.IsDisposed);

            this.isDisposed = 0;
        }

        /// <summary>
        ///     Gets a value indicating whether current instance has been disposed.
        /// </summary>
        [Pure]
        protected bool IsDisposed
        {
            get
            {
                Contract.Ensures(Contract.Result<bool>() == (this.isDisposed == 1));
                Contract.Ensures(this.isDisposed == Contract.OldValue(this.isDisposed));

                return this.isDisposed == 1;
            }
        }

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly",
            Justification = "Pattern maintained with a value that tracks the disposition state.")]
        public void Dispose()
        {
            Contract.Ensures(this.IsDisposed);

            if (!this.MarkAsDisposed())
            {
                this.Dispose(true);
                GC.SuppressFinalize(this);
            }
        }

        /// <summary>
        ///     Called upon application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing">
        ///     Value of this parameter is set to <see langword="true"/> if the method is called from <see cref="IDisposable.Dispose"/> method.
        ///     Value of this parameter set to <see langword="false"/> indicates that the method is called from finalizer.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            this.ContractRequiresPureEnsuresIsDisposed();
        }

        /// <summary>
        ///     Throws an <see cref="ObjectDisposedException"/> if current instance is disposed.
        /// </summary>
        /// <exception cref="ObjectDisposedException">
        ///     Current instance is disposed.
        /// </exception>
        [Pure]
        protected void ThrowIfDisposed()
        {
            this.ContractPureEnsuresIsNotDisposed();
            Contract.EnsuresOnThrow<ObjectDisposedException>(this.IsDisposed);

            if (this.IsDisposed)
            {
                this.ThrowObjectDisposedException();
            }
        }

        /// <summary>
        ///     A contract abbreviator method which ensures that the member will not change state of the object disposition.
        /// </summary>
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic",
            Justification = "Required for code contracts.")]
        [Pure]
        [ContractAbbreviator]
        protected void ContractPureIsDisposed()
        {
            Contract.Ensures(this.IsDisposed == Contract.OldValue(this.IsDisposed));
        }

        /// <summary>
        ///     A contract abbreviator method which marks a member with visibility wider than <see langword="protected"/> as callable
        ///     only on not disposed objects and ensures that the object will remain not disposed after the call.
        /// </summary>
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic",
            Justification = "Required for code contracts.")]
        [Pure]
        [ContractAbbreviator]
        protected void ContractPureEnsuresIsNotDisposed()
        {
            Contract.Ensures(!this.IsDisposed);
            this.ContractPureIsDisposed();
        }

        /// <summary>
        ///     A contract abbreviator method which marks a member with visibility narrower or equal to <see langword="protected"/> as callable
        ///     only on not disposed objects and ensures that the object will remain not disposed after the call.
        /// </summary>
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic",
            Justification = "Required for code contracts.")]
        [Pure]
        [ContractAbbreviator]
        protected void ContractRequiresPureEnsuresIsNotDisposed()
        {
            Contract.Requires(!this.IsDisposed);
            Contract.Ensures(!this.IsDisposed);
            this.ContractPureIsDisposed();
        }

        /// <summary>
        ///     A contract abbreviator method which marks a member with visibility wider than <see langword="protected"/> as callable
        ///     only on disposed objects and ensures that the object will remain disposed after the call.
        /// </summary>
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic",
            Justification = "Required for code contracts.")]
        [Pure]
        [ContractAbbreviator]
        protected void ContractPureEnsuresIsDisposed()
        {
            Contract.Ensures(this.IsDisposed);
            this.ContractPureIsDisposed();
        }

        /// <summary>
        ///     A contract abbreviator method which marks a member with visibility narrower or equal to <see langword="protected"/> as callable
        ///     only on disposed objects and ensures that the object will remain disposed after the call.
        /// </summary>
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic",
            Justification = "Required for code contracts.")]
        [Pure]
        [ContractAbbreviator]
        protected void ContractRequiresPureEnsuresIsDisposed()
        {
            Contract.Requires(this.IsDisposed);
            Contract.Ensures(this.IsDisposed);
            this.ContractPureIsDisposed();
        }

        /// <summary>
        ///     Marks current instance as disposed.
        /// </summary>
        /// <returns>
        ///     A value that indicates whether current instance has been already marked as disposed.
        /// </returns>
        private bool MarkAsDisposed()
        {
            Contract.Ensures(this.IsDisposed);
            
            bool result = Interlocked.Exchange(ref this.isDisposed, 1) == 1;
            Contract.Assume(this.isDisposed == 1);
            return result;
        }

        /// <summary>
        ///     Throws an <see cref="ObjectDisposedException"/>  object.
        /// </summary>
        /// <exception cref="ObjectDisposedException">
        ///     Current object is disposed.
        /// </exception>
        [Pure]
        private void ThrowObjectDisposedException()
        {
            Contract.Ensures(false);
            Contract.EnsuresOnThrow<ObjectDisposedException>(true);

            throw new ObjectDisposedException(this.GetType().FullName);
        }
    }
}
