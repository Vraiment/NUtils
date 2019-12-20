/*
 * Copyright (c) 2019 Vraiment
 *
 * This source code file is subject to the terms of the MIT license.
 * If a copy of the MIT license was not distributed with this file,
 * You can obtain one at https://opensource.org/licenses/MIT.
 */
using FluentAssertions;
using NUnit.Framework;
using NUtils.Extensions;
using System;

namespace NUtils.Tests.Extensions
{
    class ArrayExtensionsTests
    {
        #region Get method
        [Test]
        public void Test_Get_With_Valid_Index()
        {
            char[] array = SampleArray();

            for (int n = 0; n < array.Length; ++n)
            {
                char expected = array[n];
                array.Get(n).Should().Be(expected);
            }
        }

        [Test]
        public void Test_Get_With_Null_Array()
        {
            char[] array = null;
            Action action = () => array.Get(0);

            action.Should()
                .ThrowExactly<ArgumentNullException>()
                .WithMessage("*array*");
        }

        [Test]
        public void Test_Get_With_Invalid_Index()
        {
            char[] array = SampleArray();
            Action action = default;

            action = () => array.Get(-1);
            action.Should()
                .ThrowExactly<ArgumentOutOfRangeException>()
                .WithMessage("*index*");

            action = () => array.Get(array.Length);
            action.Should()
                .ThrowExactly<ArgumentOutOfRangeException>()
                .WithMessage("*index*");
        }

        [Test]
        public void Test_Get_With_An_Empty_Array()
        {
            char[] array = Array.Empty<char>();
            Action action = () => array.Get(0);

            action.Should()
                .ThrowExactly<ArgumentOutOfRangeException>()
                .WithMessage("*index*");
        }
        #endregion

        #region GetOrElse method
        [Test]
        public void Test_GetOrElse_With_Valid_Index()
        {
            char[] array = SampleArray();

            for (int n = 0; n < array.Length; ++n)
            {
                char expected = array[n];
                array.GetOrElse(n, 'z').Should().Be(expected);
            }
        }

        [Test]
        public void Test_GetOrElse_With_Invalid_Index()
        {
            char[] array = SampleArray();
            char expected = 'z';

            array.GetOrElse(-1, 'z').Should().Be(expected);
            array.GetOrElse(array.Length, 'z').Should().Be(expected);
        }

        [Test]
        public void Test_GetOrElse_With_Empty_Array()
        {
            char[] array = Array.Empty<char>();
            char expected = 'z';

            array.GetOrElse(0, 'z').Should().Be(expected);
        }

        [Test]
        public void Test_GetOrElse_With_Null_Array()
        {
            char[] array = null;
            Action action = () => array.GetOrElse(0, 'z');

            action.Should()
                .ThrowExactly<ArgumentNullException>()
                .WithMessage("*array*");
        }
        #endregion

        #region IsEmpty
        [Test]
        public void Test_IsEmpty_With_Not_Empty_Array()
        {
            char[] array = SampleArray();

            array.IsEmpty().Should().BeFalse();
        }


        [Test]
        public void Test_IsEmpty_With_An_Empty_Array()
        {
            char[] array = Array.Empty<char>();

            array.IsEmpty().Should().BeTrue();
        }


        [Test]
        public void Test_IsEmpty_With_Null_Array()
        {
            char[] array = null;
            Action action = () => array.IsEmpty();

            action.Should()
                .ThrowExactly<ArgumentNullException>()
                .WithMessage("*array*");
        }
        #endregion

        #region IsNotEmpty
        [Test]
        public void Test_IsNotEmpty_With_Not_Empty_Array()
        {
            char[] array = SampleArray();

            array.IsNotEmpty().Should().BeTrue();
        }


        [Test]
        public void Test_IsNotEmpty_With_An_Empty_Array()
        {
            char[] array = Array.Empty<char>();

            array.IsNotEmpty().Should().BeFalse();
        }


        [Test]
        public void Test_IsNotEmpty_With_Null_Array()
        {
            char[] array = null;
            Action action = () => array.IsNotEmpty();

            action.Should()
                .ThrowExactly<ArgumentNullException>()
                .WithMessage("*array*");
        }
        #endregion

        private static char[] SampleArray()
            => new char[] { 'a', 'b', 'c', 'd' };
    }
}
