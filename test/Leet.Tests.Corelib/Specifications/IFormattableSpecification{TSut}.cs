// -----------------------------------------------------------------------
// <copyright file="IFormattableSpecification{TSut}.cs" company="Leet">
//     Copyright (c) Leet. All rights reserved.
//     Licensed under the MIT License.
//     See License.txt in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Leet.Specifications
{
    using System;
    using System.Globalization;
    using System.Reflection;
    using NSubstitute;
    using Ploeh.AutoFixture;
    using Xunit;

    /// <summary>
    ///     A class that specifies behavior for <see cref="IFormattable"/> interface.
    /// </summary>
    /// <typeparam name="TSut">
    ///     Type which shall be tested for conformance with behavior defined for <see cref="IFormattable"/> interface.
    /// </typeparam>
    public abstract class IFormattableSpecification<TSut>
        where TSut : IFormattable
    {
        /// <summary>
        ///     Checks whether <see cref="IFormattable.ToString(string, IFormatProvider)"/> method accepts
        ///     <see langword="null"/> format provider parameter.
        /// </summary>
        /// <param name="format">
        ///     The format to use.
        /// </param>
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("G")]
        public void ToString_String_IFormatProvider_WithNullFormatProvider_ReturnsResult(string format)
        {
            // Fixture setup
            IFixture fixture = DomainFixture.CreateFor(this);
            var sut = fixture.Create<TSut>();

            // Exercise system
            // Verify outcome
            Assert.NotNull(sut.ToString(format, null));

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="IFormattable.ToString(string, IFormatProvider)"/> method accepts
        ///     format provider parameter that always returns <see langword="null"/>.
        /// </summary>
        /// <param name="format">
        ///     The format to use.
        /// </param>
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("G")]
        public void ToString_String_IFormatProvider_WithFormatProviderReturningNull_ReturnsResult(string format)
        {
            // Fixture setup
            IFixture fixture = DomainFixture.CreateFor(this);
            IFormatProvider formatProvider = fixture.Create<IFormatProvider>();
            formatProvider.GetFormat(Arg.Any<Type>()).Returns(null);
            var sut = fixture.Create<TSut>();

            // Exercise system
            // Verify outcome
            Assert.NotNull(sut.ToString(format, formatProvider));

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="IFormattable.ToString(string, IFormatProvider)"/> method accepts
        ///     <see cref="CultureInfo.CurrentCulture"/> as format provider parameter.
        /// </summary>
        /// <param name="format">
        ///     The format to use.
        /// </param>
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("G")]
        public void ToString_String_IFormatProvider_WithCurrentCulture_ReturnsResult(string format)
        {
            // Fixture setup
            IFixture fixture = DomainFixture.CreateFor(this);
            IFormatProvider formatProvider = CultureInfo.CurrentCulture;
            var sut = fixture.Create<TSut>();

            // Exercise system
            // Verify outcome
            Assert.NotNull(sut.ToString(format, formatProvider));

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="IFormattable.ToString(string, IFormatProvider)"/> method called with
        ///     standard format returns same value as called with <c>"G"</c>.
        /// </summary>
        /// <param name="format">
        ///     The format to use.
        /// </param>
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("G")]
        public void ToString_String_IFormatProvider_WithDefaultFormat_BehavesAsG(string format)
        {
            // Fixture setup
            IFixture fixture = DomainFixture.CreateFor(this);
            IFormatProvider formatProvider = fixture.Create<IFormatProvider>();
            formatProvider.GetFormat(Arg.Any<Type>()).Returns(null);
            string secondFormat = "G";
            var sut = fixture.Create<TSut>();

            // Exercise system
            // Verify outcome
            Assert.Equal(sut.ToString(secondFormat, formatProvider), sut.ToString(format, formatProvider));

            // Teardown
        }

        /// <summary>
        ///     Checks whether <typeparamref name="TSut"/> type has defined <see langword="static"/> method
        ///     <c>Parse</c> that accepts one parameter, <see cref="string"/>.
        /// </summary>
        [Fact]
        public void Parse_String_Is_Defined()
        {
            // Fixture setup
            Type type = typeof(TSut);

            // Exercise system
            MethodInfo method = type.GetMethod("Parse", BindingFlags.Public | BindingFlags.Static, Type.DefaultBinder, new Type[] { typeof(string) }, null);

            // Verify outcome
            Assert.NotNull(method);

            // Teardown
        }

        /// <summary>
        ///     Checks whether <typeparamref name="TSut"/> type has defined <see langword="static"/> method
        ///     <c>TryParse</c> that accepts one parameter, <see cref="string"/>.
        /// </summary>
        [Fact]
        public void TryParse_String_OutTSut_Is_Defined()
        {
            // Fixture setup
            Type type = typeof(TSut);

            // Exercise system
            MethodInfo method = type.GetMethod("TryParse", BindingFlags.Public | BindingFlags.Static, Type.DefaultBinder, new Type[] { typeof(string), typeof(TSut).MakeByRefType() }, null);
            ParameterInfo parameter = method.GetParameters()[1];

            // Verify outcome
            Assert.True(parameter.IsOut);

            // Teardown
        }

        /// <summary>
        ///     Checks whether a static method <c>Parse</c> defined on <typeparamref name="TSut"/> type throws
        ///     an <see cref="ArgumentNullException"/> when called with <see langword="null"/> <see cref="string"/> parameter.
        /// </summary>
        [Fact]
        public void Parse_String_WithNullString_Throws()
        {
            // Fixture setup
            Type type = typeof(TSut);

            // Exercise system
            // Verify outcome
            Assert.Throws<ArgumentNullException>((Action)(() =>
            {
                throw type.InvokePublicMethodWithException("Parse", new object[] { null });
            }));

            // Teardown
        }

        /// <summary>
        ///     Checks whether a static method <c>TryParse</c> defined on <typeparamref name="TSut"/> type returns
        ///     <see langword="false"/> when called with <see langword="null"/> <see cref="string"/> parameter.
        /// </summary>
        [Fact]
        public void TryParse_String_WithNullString_ReturnsFalse()
        {
            // Fixture setup
            Type type = typeof(TSut);
            TSut result = default(TSut);

            // Exercise system
            bool returnValue = (bool)type.InvokeMember(
                "TryParse",
                BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Static,
                Type.DefaultBinder,
                null,
                new object[] { null, result });

            // Verify outcome
            Assert.False(returnValue);

            // Teardown
        }

        /// <summary>
        ///     Checks whether a static method <c>TryParse</c> defined on <typeparamref name="TSut"/> type returns
        ///     default instance of the <typeparamref name="TSut"/> for <see langword="null"/> <see cref="string"/> parameter.
        /// </summary>
        [Fact]
        public void TryParse_String_WithNullString_ReturnsDefaultvalue()
        {
            // Fixture setup
            Type type = typeof(TSut);
            TSut result = default(TSut);

            // Exercise system
            type.InvokeMember(
                "TryParse",
                BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Static,
                Type.DefaultBinder,
                null,
                new object[] { null, result });

            // Verify outcome
            Assert.Equal(default(TSut), result);

            // Teardown
        }
    }
}
