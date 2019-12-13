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
    class IReadOnlyListExtensionsTests
    {
        #region Get method
        [Test]
        public void Test_Get_With_Valid_Index()
        {
            IReadOnlyList<char> list = SampleList();
            IReadOnlyList<char> expected = SampleList();

            for (int n = 0; n < list.Count; ++n)
            {
                char value = list.Get(n);
                char expectedValue = expected[n];

                value.Should().Be(
                    expected: expectedValue,
                    because: $"that's the value at {n}th position"
                );
            }
        }

        [Test]
        public void Test_Get_With_Null_List()
        {
            IReadOnlyList<char> list = null;
            Action action = () => list.Get(0);

            action.Should()
                .ThrowExactly<ArgumentNullException>()
                .WithMessage("*list*");
        }

        [Test]
        public void Test_Get_With_Index_Before_Start_Of_List()
        {
            IReadOnlyList<char> list = SampleList();
            Action action = () => list.Get(-1);

            action.Should()
                .ThrowExactly<ArgumentOutOfRangeException>()
                .WithMessage("*index*");
        }

        [Test]
        public void Test_Get_With_Index_After_End_Of_List()
        {
            IReadOnlyList<char> list = SampleList();
            Action action = () => list.Get(list.Count);

            action.Should()
                .ThrowExactly<ArgumentOutOfRangeException>()
                .WithMessage("*index*");
        }

        [Test]
        public void Test_Get_With_An_Empty_List()
        {
            IReadOnlyList<char> list = EmptyList();
            Action action = () => list.Get(0);

            action.Should()
                .ThrowExactly<ArgumentOutOfRangeException>()
                .WithMessage("*index*");
        }
        #endregion

        #region GetOrElse method
        [Test]
        public void Test_GetOrElse_With_Valid_Index()
        {
            IReadOnlyList<char> list = SampleList();
            IReadOnlyList<char> expected = SampleList();

            for (int n = 0; n < expected.Count; ++n)
            {
                char expectedValue = expected[n];
                list.GetOrElse(n, 'z').Should().Be(
                    expected: expectedValue,
                    because: $"that's the value at {n}th position"
                );
            }
        }

        [Test]
        public void Test_GetOrElse_With_Index_Before_Start_Of_List()
        {
            IReadOnlyList<char> list = SampleList();
            char expected = 'z';

            list.GetOrElse(-1, expected).Should().Be(expected);
        }

        [Test]
        public void Test_GetOrElse_With_Index_After_End_Of_List()
        {
            IReadOnlyList<char> list = SampleList();
            char expected = 'z';

            list.GetOrElse(list.Count(), expected).Should().Be(expected);
        }

        [Test]
        public void Test_GetOrElse_With_Empty_List()
        {
            IReadOnlyList<char> list = EmptyList();
            char expected = 'z';

            list.GetOrElse(0, expected).Should().Be(expected);
        }

        [Test]
        public void Test_GetOrElse_With_Null_List()
        {
            IReadOnlyList<char> list = null;
            Action action = () => list.GetOrElse(0, 'z');

            action.Should()
                .ThrowExactly<ArgumentNullException>()
                .WithMessage("*list*");
        }
        #endregion

        private static IReadOnlyList<char> EmptyList()
            => new List<char> { };

        private static IReadOnlyList<char> SampleList()
            => new List<char> { 'a', 'b', 'c', 'd' };
    }
}
