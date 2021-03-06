﻿// -----------------------------------------------------------------------
// <copyright file="IEquatableSpecification{TSut}.cs" company="Leet">
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
    ///     A class that specifies behavior for <see cref="IEquatable{T}"/> interface.
    /// </summary>
    /// <typeparam name="TSut">
    ///     Type which shall be tested for conformance with behavior defined for <see cref="IEquatable{T}"/> class.
    /// </typeparam>
    public abstract class IEquatableSpecification<TSut> : IEquatableSpecification<TSut, TSut>
        where TSut : IEquatable<TSut>
    {
        /// <summary>
        ///     Checks whether <see cref="IEquatable{T}.Equals(T)"/> method returns <see langword="true"/>
        ///     for same instance as parameter.
        /// </summary>
        /// <param name="sut">
        ///     Object under test.
        /// </param>
        [Theory]
        [AutoDomainData]
        public void Equals_TSut_ForSameInstance_ReturnsTrue(TSut sut)
        {
            // Fixture setup

            // Exercise system
            bool result = sut.Equals(sut);

            // Verify outcome
            Assert.True(result);

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="IEquatable{T}.Equals(T)"/> method returns same result if object on which the method is called
        ///     and a paraeter are swapped.
        /// </summary>
        /// <param name="sut">
        ///     Object under test.
        /// </param>
        /// <param name="other">
        ///     Other instance to compare.
        /// </param>
        [Theory]
        [AutoDomainData]
        public void Equals_TSut_ForSwappedInstances_ReturnsSameResult(TSut sut, TSut other)
        {
            // Fixture setup
            bool expectedResult = other.Equals(sut);

            // Exercise system
            bool result = sut.Equals(other);

            // Verify outcome
            Assert.Equal(expectedResult, result);

            // Teardown
        }

        /// <summary>
        ///     Checks whether static <c>TSut.Equals(TSut,TSut)</c> method returns <see langword="true"/>
        ///     for both same instances as parameters.
        /// </summary>
        /// <param name="sut">
        ///     Object under test.
        /// </param>
        [Theory]
        [AutoDomainData]
        public void StaticEquals_TSut_TSut_ForSameInstance_ReturnsTrue(TSut sut)
        {
            // Fixture setup
            Type sutType = typeof(TSut);

            // Exercise system
            bool result = (bool)sutType.InvokePublicMethod("Equals", sut, sut);

            // Verify outcome
            Assert.True(result);

            // Teardown
        }

        /// <summary>
        ///     Checks whether <c>TSut.Equals(TSut,TSut)</c> method returns same results if object on which the method is called
        ///     and a paraeter are swapped.
        /// </summary>
        /// <param name="sut">
        ///     Object under test.
        /// </param>
        /// <param name="other">
        ///     Other instance to compare.
        /// </param>
        [Theory]
        [AutoDomainData]
        public void StaticEquals_TSut_T_ForSwappedInstances_ReturnsSameResult(TSut sut, TSut other)
        {
            // Fixture setup
            Type sutType = typeof(TSut);
            bool expectedResult = (bool)sutType.InvokePublicMethod("Equals", other, sut);

            // Exercise system
            bool result = (bool)sutType.InvokePublicMethod("Equals", sut, other);

            // Verify outcome
            Assert.Equal(expectedResult, result);

            // Teardown
        }

        /// <summary>
        ///     Checks whether <typeparamref name="TSut"/> defaines a <see langword="static"/> <c>op_Equality(TSut,TSut)</c> operator.
        /// </summary>
        [Fact]
        public void Type_Defines_StaticEqualityTSutTSutOperator_OrIsReferenceType()
        {
            // Fixture setup
            Type sutType = typeof(TSut);

            // Exercise system
            MethodInfo method = sutType.GetMethod(
                "op_Equality",
                BindingFlags.Static | BindingFlags.Public,
                Type.DefaultBinder,
                new Type[] { typeof(TSut), typeof(TSut) },
                null);

            // Verify outcome
            Assert.True(!object.ReferenceEquals(method, null) || !typeof(TSut).IsValueType);

            // Teardown
        }

        /// <summary>
        ///     Checks whether <typeparamref name="TSut"/> defaines a <see langword="static"/> <c>op_Inequality(TSut,TSut)</c> operator.
        /// </summary>
        [Fact]
        public void Type_Defines_StaticInequalityTSutTSutOperator_OrIsReferenceType()
        {
            // Fixture setup
            Type sutType = typeof(TSut);

            // Exercise system
            MethodInfo method = sutType.GetMethod(
                "op_Inequality",
                BindingFlags.Static | BindingFlags.Public,
                Type.DefaultBinder,
                new Type[] { typeof(TSut), typeof(TSut) },
                null);

            // Verify outcome
            Assert.True(!object.ReferenceEquals(method, null) || !typeof(TSut).IsValueType);

            // Teardown
        }

        /// <summary>
        ///     Checks whether <c>TSut.op_Equality(TSut,TSut)</c> operator returns <see langword="true"/>
        ///     for both same instances as parameters.
        /// </summary>
        /// <param name="sut">
        ///     Object under test.
        /// </param>
        [Theory]
        [AutoDomainData]
        public void OperatorEquality_TSut_TSut_ForSameInstance_ReturnsTrue(TSut sut)
        {
            // Fixture setup
            Type sutType = typeof(TSut);

            // Exercise system
            bool result = (bool)sutType.InvokePublicMethod("op_Equality", sut, sut);

            // Verify outcome
            Assert.True(result);

            // Teardown
        }

        /// <summary>
        ///     Checks whether <c>TSut.op_Equality(TSut,TSut)</c> operator returns same results if object on which the method is called
        ///     and a paraeter are swapped.
        /// </summary>
        /// <param name="sut">
        ///     Object under test.
        /// </param>
        /// <param name="other">
        ///     Other instance to compare.
        /// </param>
        [Theory]
        [AutoDomainData]
        public void OperatorEquality_TSut_TSut_ForSwappedInstances_ReturnsSameResult(TSut sut, TSut other)
        {
            // Fixture setup
            Type sutType = typeof(TSut);
            bool expectedResult = (bool)sutType.InvokePublicMethod("op_Equality", other, sut);

            // Exercise system
            bool result = (bool)sutType.InvokePublicMethod("op_Equality", sut, other);

            // Verify outcome
            Assert.Equal(expectedResult, result);

            // Teardown
        }

        /// <summary>
        ///     Checks whether <c>TSut.op_Inequality(TSut,TSut)</c> operator returns <see langword="false"/>
        ///     for both same instances as parameters.
        /// </summary>
        /// <param name="sut">
        ///     Object under test.
        /// </param>
        [Theory]
        [AutoDomainData]
        public void OperatorInequality_TSut_TSut_ForSameInstance_ReturnsFalse(TSut sut)
        {
            // Fixture setup
            Type sutType = typeof(TSut);

            // Exercise system
            bool result = (bool)sutType.InvokePublicMethod("op_Inequality", sut, sut);

            // Verify outcome
            Assert.False(result);

            // Teardown
        }

        /// <summary>
        ///     Checks whether <c>TSut.op_Inequality(TSut,TSut)</c> operator returns same results if object on which the method is called
        ///     and a paraeter are swapped.
        /// </summary>
        /// <param name="sut">
        ///     Object under test.
        /// </param>
        /// <param name="other">
        ///     Other instance to compare.
        /// </param>
        [Theory]
        [AutoDomainData]
        public void OperatorInequality_TSut_TSut_ForSwappedInstances_ReturnsSameResult(TSut sut, TSut other)
        {
            // Fixture setup
            Type sutType = typeof(TSut);
            bool expectedResult = (bool)sutType.InvokePublicMethod("op_Inequality", other, sut);

            // Exercise system
            bool result = (bool)sutType.InvokePublicMethod("op_Inequality", sut, other);

            // Verify outcome
            Assert.Equal(expectedResult, result);

            // Teardown
        }
    }
}
