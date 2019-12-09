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
using System.Collections.Generic;
using System.Linq;

namespace NUtils.Tests.Extensions
{
    class IEnumerableExtensionsTests
    {
        #region ForEach method
        [Test]
        public void Test_ForEach()
        {
            IEnumerable<char> enumerable = SampleEnumerable();
            char[] expected = SampleEnumerable().ToArray();
            int index = 0;

            enumerable.ForEach(value =>
            {
                char expectedValue = expected[index++];
                value.Should().Be(expectedValue);
            });
        }

        [Test]
        public void Test_ForEach_With_Empty_Enumerable()
        {
            IEnumerable<char> enumerable = EmptyEnumerable();

            enumerable.ForEach(value => Assert.Fail());
        }

        [Test]
        public void Test_ForEach_With_Null_Enumerable()
        {
            IEnumerable<char> enumerable = null;
            Action action = () => enumerable.ForEach(_ => { });

            action.Should()
                .ThrowExactly<ArgumentNullException>()
                .WithMessage("*enumerable*");
        }

        [Test]
        public void Test_ForEach_With_Null_Action()
        {
            IEnumerable<char> enumerable = SampleEnumerable();
            Action action = () => enumerable.ForEach((Action<char>)null);

            action.Should()
                .ThrowExactly<ArgumentNullException>()
                .WithMessage("*action*");
        }
        #endregion

        #region ForEach with index method
        [Test]
        public void Test_ForEach_With_Index()
        {
            IEnumerable<char> enumerable = SampleEnumerable();
            char[] expected = SampleEnumerable().ToArray();
            int expectedIndex = 0;

            enumerable.ForEach((value, index) =>
            {
                index.Should().Be(expectedIndex);

                char expectedValue = expected[expectedIndex++];
                value.Should().Be(expectedValue);
            });
        }

        [Test]
        public void Test_ForEach_With_Index_Empty_Enumerable()
        {
            IEnumerable<char> enumerable = EmptyEnumerable();

            enumerable.ForEach((value, index) => Assert.Fail());
        }

        [Test]
        public void Test_ForEach_With_Index_With_Null_Enumerable()
        {
            IEnumerable<char> enumerable = null;
            Action action = () => enumerable.ForEach((_, __) => { });

            action.Should()
                .ThrowExactly<ArgumentNullException>()
                .WithMessage("*enumerable*");
        }

        [Test]
        public void Test_ForEach_With_Index_Null_Action()
        {
            IEnumerable<char> enumerable = SampleEnumerable();
            Action action = () => enumerable.ForEach((Action<char, int>)null);

            action.Should()
                .ThrowExactly<ArgumentNullException>()
                .WithMessage("*action*");
        }
        #endregion

        #region Get method
        [Test]
        public void Test_Get_With_Valid_Index()
        {
            IEnumerable<char> enumerable = SampleEnumerable();
            char[] expected = SampleEnumerable().ToArray();

            for (int n = 0; n < enumerable.Count(); ++n)
            {
                char expectedValue = expected[n];
                enumerable.Get(n).Should().Be(expectedValue);
            }
        }

        [Test]
        public void Test_Get_With_Null_Enumerable()
        {
            IEnumerable<char> enumerable = null;
            Action action = () => enumerable.Get(0);

            action.Should()
                .ThrowExactly<ArgumentNullException>()
                .WithMessage("*enumerable*");
        }

        [Test]
        public void Test_Get_With_Invalid_Index()
        {
            IEnumerable<char> enumerable = SampleEnumerable();
            Action action = default;

            action = () => enumerable.Get(-1);
            action.Should()
                .ThrowExactly<ArgumentOutOfRangeException>()
                .WithMessage("*index*");

            action = () => enumerable.Get(enumerable.Count());
            action.Should()
                .ThrowExactly<ArgumentOutOfRangeException>()
                .WithMessage("*index*");
        }

        [Test]
        public void Test_Get_With_An_Empty_Enumerable()
        {
            IEnumerable<char> enumerable = EmptyEnumerable();
            Action action = () => enumerable.Get(0);

            action.Should()
                .ThrowExactly<ArgumentOutOfRangeException>()
                .WithMessage("*index*");
        }
        #endregion

        #region GetOrElse method
        [Test]
        public void Test_GetOrElse_With_Valid_Index()
        {
            IEnumerable<char> enumerable = SampleEnumerable();
            char[] expected = SampleEnumerable().ToArray();

            for (int n = 0; n < expected.Length; ++n)
            {
                char expectedValue = expected[n];
                enumerable.GetOrElse(n, 'z').Should().Be(expectedValue);
            }
        }

        [Test]
        public void Test_GetOrElse_With_Invalid_Index()
        {
            IEnumerable<char> enumerable = SampleEnumerable();
            char expected = 'z';

            enumerable.GetOrElse(-1, expected).Should().Be(expected);
            enumerable.GetOrElse(enumerable.Count(), expected).Should().Be(expected);
        }

        [Test]
        public void Test_GetOrElse_With_Empty_Enumerable()
        {
            IEnumerable<char> enumerable = EmptyEnumerable();
            char expected = 'z';

            enumerable.GetOrElse(0, expected).Should().Be(expected);
        }

        [Test]
        public void Test_GetOrElse_With_Null_Enumerable()
        {
            IEnumerable<char> enumerable = null;
            Action action = () => enumerable.GetOrElse(0, 'z');

            action.Should()
                .ThrowExactly<ArgumentNullException>()
                .WithMessage("*enumerable*");
        }
        #endregion

        #region IsEmpty
        [Test]
        public void Test_IsEmpty_With_Not_Empty_Enumerable()
        {
            IEnumerable<char> enumerable = SampleEnumerable();

            enumerable.IsEmpty().Should().BeFalse();
        }


        [Test]
        public void Test_IsEmpty_With_An_Empty_Enumerable()
        {
            IEnumerable<char> enumerable = EmptyEnumerable();

            enumerable.IsEmpty().Should().BeTrue();
        }


        [Test]
        public void Test_IsEmpty_With_Null_Enumerable()
        {
            IEnumerable<char> enumerable = null;
            Action action = () => enumerable.IsEmpty();

            action.Should()
                .ThrowExactly<ArgumentNullException>()
                .WithMessage("*enumerable*");
        }
        #endregion

        #region IsNotEmpty
        [Test]
        public void Test_IsNotEmpty_With_Not_Empty_Enumerable()
        {
            IEnumerable<char> enumerable = SampleEnumerable();

            enumerable.IsNotEmpty().Should().BeTrue();
        }


        [Test]
        public void Test_IsNotEmpty_With_An_Empty_Enumerable()
        {
            IEnumerable<char> enumerable = EmptyEnumerable();

            enumerable.IsNotEmpty().Should().BeFalse();
        }


        [Test]
        public void Test_IsNotEmpty_With_Null_Enumerable()
        {
            IEnumerable<char> enumerable = null;
            Action action = () => enumerable.IsNotEmpty();

            action.Should()
                .ThrowExactly<ArgumentNullException>()
                .WithMessage("*enumerable*");
        }
        #endregion

        private static IEnumerable<char> EmptyEnumerable()
            => Array.Empty<char>();

        private static IEnumerable<char> SampleEnumerable()
            => new char[] { 'a', 'b', 'c', 'd' };
    }
}
