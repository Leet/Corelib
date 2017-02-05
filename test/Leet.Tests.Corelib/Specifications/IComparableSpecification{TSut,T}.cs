// -----------------------------------------------------------------------
// <copyright file="IComparableSpecification{TSut,T}.cs" company="Leet">
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
    ///     A class that specifies behavior for <see cref="IComparable{T}"/> interface.
    /// </summary>
    /// <typeparam name="TSut">
    ///     Type which shall be tested for conformance with behavior defined for <see cref="IComparable{T}"/> class.
    /// </typeparam>
    /// <typeparam name="T">
    ///     The type of objects to compare.
    /// </typeparam>
    public abstract class IComparableSpecification<TSut, T>
        where TSut : IComparable<T>, T
    {
        /// <summary>
        ///     Checks whether <see cref="IComparable{T}.CompareTo(T)"/> method called with same instance
        ///     returns 0.
        /// </summary>
        /// <param name="sut">
        ///     Object under test.
        /// </param>
        [Theory]
        [AutoDomainData]
        public void CompareTo_TSut_CalledWithSameInstance_ReturnsZero(TSut sut)
        {
            // Fixture setup

            // Exercise system
            // Verify outcome
            Assert.Equal(0, sut.CompareTo(sut));

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="IComparable{T}.CompareTo(T)"/> method when called with <see langword="null"/>
        ///     reference returns 1.
        /// </summary>
        /// <param name="sut">
        ///     Object under test.
        /// </param>
        [Theory]
        [AutoDomainData]
        public void CompareTo_TSut_CalledWithNull_ReturnsOne(TSut sut)
        {
            // Fixture setup
            TSut other = default(TSut);

            // Exercise system
            // Verify outcome
            Assert.True(!object.ReferenceEquals(other, null) || sut.CompareTo(other) == 1);

            // Teardown
        }

        /// <summary>
        ///     Checks whether <typeparamref name="TSut"/> type implements also <see cref="IEquatable{T}"/> interface.
        /// </summary>
        [Fact]
        public void Type_Implements_IEquatable_T()
        {
            // Fixture setup

            // Exercise system
            // Verify outcome
            Assert.True(typeof(IEquatable<T>).IsAssignableFrom(typeof(TSut)));

            // Teardown
        }

        /// <summary>
        ///     Checks whether <typeparamref name="TSut"/> type implements op_GreaterThan operator.
        /// </summary>
        [Fact]
        public void Type_Implements_GreaterThanOperator()
        {
            // Fixture setup
            string methodName = "op_GreaterThan";
            BindingFlags flags = BindingFlags.Static | BindingFlags.Public;
            Type[] paraeterTypes = new Type[]
            {
                typeof(T), typeof(T)
            };

            // Exercise system
            MethodInfo operatorMethod = typeof(TSut).GetMethod(methodName, flags, Type.DefaultBinder, paraeterTypes, null);

            // Verify outcome
            Assert.Equal(typeof(bool), operatorMethod.ReturnType);

            // Teardown
        }

        /// <summary>
        ///     Checks whether <typeparamref name="TSut"/> type implements op_GreaterThanOrEqual operator.
        /// </summary>
        [Fact]
        public void Type_Implements_GreaterThanOrEqualOperator()
        {
            // Fixture setup
            string methodName = "op_GreaterThanOrEqual";
            BindingFlags flags = BindingFlags.Static | BindingFlags.Public;
            Type[] paraeterTypes = new Type[]
            {
                typeof(T), typeof(T)
            };

            // Exercise system
            MethodInfo operatorMethod = typeof(TSut).GetMethod(methodName, flags, Type.DefaultBinder, paraeterTypes, null);

            // Verify outcome
            Assert.Equal(typeof(bool), operatorMethod.ReturnType);

            // Teardown
        }

        /// <summary>
        ///     Checks whether <typeparamref name="TSut"/> type implements op_LessThan operator.
        /// </summary>
        [Fact]
        public void Type_Implements_LessThanOperator()
        {
            // Fixture setup
            string methodName = "op_LessThan";
            BindingFlags flags = BindingFlags.Static | BindingFlags.Public;
            Type[] paraeterTypes = new Type[]
            {
                typeof(T), typeof(T)
            };

            // Exercise system
            MethodInfo operatorMethod = typeof(TSut).GetMethod(methodName, flags, Type.DefaultBinder, paraeterTypes, null);

            // Verify outcome
            Assert.Equal(typeof(bool), operatorMethod.ReturnType);

            // Teardown
        }

        /// <summary>
        ///     Checks whether <typeparamref name="TSut"/> type implements op_LessThanOrEqual operator.
        /// </summary>
        [Fact]
        public void Type_Implements_LessThanOrEqualOperator()
        {
            // Fixture setup
            string methodName = "op_LessThanOrEqual";
            BindingFlags flags = BindingFlags.Static | BindingFlags.Public;
            Type[] paraeterTypes = new Type[]
            {
                typeof(T), typeof(T)
            };

            // Exercise system
            MethodInfo operatorMethod = typeof(TSut).GetMethod(methodName, flags, Type.DefaultBinder, paraeterTypes, null);

            // Verify outcome
            Assert.Equal(typeof(bool), operatorMethod.ReturnType);

            // Teardown
        }

        /// <summary>
        ///     Checks whether GreaterThan operator called with same instance
        ///     returns <see langword="false"/>.
        /// </summary>
        /// <param name="sut">
        ///     Object under test.
        /// </param>
        [Theory]
        [AutoDomainData]
        public void GreaterThanOperator_CalledWithSameInstance_ReturnsFalse(TSut sut)
        {
            // Fixture setup
            string methodName = "op_GreaterThan";
            BindingFlags flags = BindingFlags.Static | BindingFlags.Public | BindingFlags.InvokeMethod;
            object[] args = new object[]
            {
                sut, sut
            };

            // Exercise system
            bool result = (bool)typeof(TSut).InvokeMember(
                methodName,
                flags,
                Type.DefaultBinder,
                null,
                args);

            // Verify outcome
            Assert.False(result);

            // Teardown
        }

        /// <summary>
        ///     Checks whether GreaterThanOrEqual operator called with same instance
        ///     returns <see langword="true"/>.
        /// </summary>
        /// <param name="sut">
        ///     Object under test.
        /// </param>
        [Theory]
        [AutoDomainData]
        public void GreaterThanOrEqualOperator_CalledWithSameInstance_ReturnsTrue(TSut sut)
        {
            // Fixture setup
            string methodName = "op_GreaterThanOrEqual";
            BindingFlags flags = BindingFlags.Static | BindingFlags.Public | BindingFlags.InvokeMethod;
            object[] args = new object[]
            {
                sut, sut
            };

            // Exercise system
            bool result = (bool)typeof(TSut).InvokeMember(
                methodName,
                flags,
                Type.DefaultBinder,
                null,
                args);

            // Verify outcome
            Assert.True(result);

            // Teardown
        }

        /// <summary>
        ///     Checks whether LessThan operator called with same instance
        ///     returns <see langword="false"/>.
        /// </summary>
        /// <param name="sut">
        ///     Object under test.
        /// </param>
        [Theory]
        [AutoDomainData]
        public void LessThanOperator_CalledWithSameInstance_ReturnsFalse(TSut sut)
        {
            // Fixture setup
            string methodName = "op_LessThan";
            BindingFlags flags = BindingFlags.Static | BindingFlags.Public | BindingFlags.InvokeMethod;
            object[] args = new object[]
            {
                sut, sut
            };

            // Exercise system
            bool result = (bool)typeof(TSut).InvokeMember(
                methodName,
                flags,
                Type.DefaultBinder,
                null,
                args);

            // Verify outcome
            Assert.False(result);

            // Teardown
        }

        /// <summary>
        ///     Checks whether LessThanOrEqual operator called with same instance
        ///     returns <see langword="true"/>.
        /// </summary>
        /// <param name="sut">
        ///     Object under test.
        /// </param>
        [Theory]
        [AutoDomainData]
        public void LessThanOrEqualoperator_CalledWithSameInstance_ReturnsTrue(TSut sut)
        {
            // Fixture setup
            string methodName = "op_LessThanOrEqual";
            BindingFlags flags = BindingFlags.Static | BindingFlags.Public | BindingFlags.InvokeMethod;
            object[] args = new object[]
            {
                sut, sut
            };

            // Exercise system
            bool result = (bool)typeof(TSut).InvokeMember(
                methodName,
                flags,
                Type.DefaultBinder,
                null,
                args);

            // Verify outcome
            Assert.True(result);

            // Teardown
        }

        /// <summary>
        ///     Checks whether the GreaterThan operator when called with <see langword="null"/>
        ///     reference returns <see langword="true"/>.
        /// </summary>
        /// <param name="sut">
        ///     Object under test.
        /// </param>
        [Theory]
        [AutoDomainData]
        public void GreaterThanOperator_CalledWithNull_ReturnsTrue(TSut sut)
        {
            // Fixture setup
            TSut other = default(TSut);
            string methodName = "op_GreaterThan";
            BindingFlags flags = BindingFlags.Static | BindingFlags.Public | BindingFlags.InvokeMethod;
            object[] args = new object[]
            {
                sut, sut
            };

            // Exercise system
            bool result = (bool)typeof(TSut).InvokeMember(
                methodName,
                flags,
                Type.DefaultBinder,
                null,
                args);

            // Verify outcome
            Assert.True(!object.ReferenceEquals(other, null) || result);

            // Teardown
        }

        /// <summary>
        ///     Checks whether the GreaterThanOrEqual operator when called with <see langword="null"/>
        ///     reference returns <see langword="true"/>.
        /// </summary>
        /// <param name="sut">
        ///     Object under test.
        /// </param>
        [Theory]
        [AutoDomainData]
        public void GreaterThanOrEqualOperator_CalledWithNull_ReturnsTrue(TSut sut)
        {
            // Fixture setup
            TSut other = default(TSut);
            string methodName = "op_GreaterThanOrEqual";
            BindingFlags flags = BindingFlags.Static | BindingFlags.Public | BindingFlags.InvokeMethod;
            object[] args = new object[]
            {
                sut, sut
            };

            // Exercise system
            bool result = (bool)typeof(TSut).InvokeMember(
                methodName,
                flags,
                Type.DefaultBinder,
                null,
                args);

            // Verify outcome
            Assert.True(!object.ReferenceEquals(other, null) || result);

            // Teardown
        }

        /// <summary>
        ///     Checks whether the LessThan operator when called with <see langword="null"/>
        ///     reference returns <see langword="false"/>.
        /// </summary>
        /// <param name="sut">
        ///     Object under test.
        /// </param>
        [Theory]
        [AutoDomainData]
        public void LessThanOperator_CalledWithNull_ReturnsFalse(TSut sut)
        {
            // Fixture setup
            TSut other = default(TSut);
            string methodName = "op_LessThan";
            BindingFlags flags = BindingFlags.Static | BindingFlags.Public | BindingFlags.InvokeMethod;
            object[] args = new object[]
            {
                sut, sut
            };

            // Exercise system
            bool result = (bool)typeof(TSut).InvokeMember(
                methodName,
                flags,
                Type.DefaultBinder,
                null,
                args);

            // Verify outcome
            Assert.True(!object.ReferenceEquals(other, null) || !result);

            // Teardown
        }

        /// <summary>
        ///     Checks whether the LessThanOrEqual operator when called with <see langword="null"/>
        ///     reference returns <see langword="false"/>.
        /// </summary>
        /// <param name="sut">
        ///     Object under test.
        /// </param>
        [Theory]
        [AutoDomainData]
        public void LessThanOrEqualOperator_CalledWithNull_ReturnsFalse(TSut sut)
        {
            // Fixture setup
            TSut other = default(TSut);
            string methodName = "op_LessThanOrEqual";
            BindingFlags flags = BindingFlags.Static | BindingFlags.Public | BindingFlags.InvokeMethod;
            object[] args = new object[]
            {
                sut, sut
            };

            // Exercise system
            bool result = (bool)typeof(TSut).InvokeMember(
                methodName,
                flags,
                Type.DefaultBinder,
                null,
                args);

            // Verify outcome
            Assert.True(!object.ReferenceEquals(other, null) || !result);

            // Teardown
        }
    }
}