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
    class ICollectionExtensionsTests
    {
        #region Get method
        [Test]
        public void Test_Get_With_Valid_Index()
        {
            ICollection<char> collection = SampleCollection();
            char[] expected = SampleCollection().ToArray();

            for (int n = 0; n < collection.Count; ++n)
            {
                char value = collection.Get(n);
                char expectedValue = expected[n];

                value.Should().Be(
                    expected: expectedValue,
                    because: $"that's the value at {n}th position"
                );
            }
        }

        [Test]
        public void Test_Get_With_Null_Collection()
        {
            ICollection<char> collection = null;
            Action action = () => collection.Get(0);

            action.Should()
                .ThrowExactly<ArgumentNullException>()
                .WithMessage("*collection*");
        }

        [Test]
        public void Test_Get_With_Index_Before_Start_Of_Collection()
        {
            ICollection<char> collection = SampleCollection();
            Action action = () => collection.Get(-1);

            action.Should()
                .ThrowExactly<ArgumentOutOfRangeException>()
                .WithMessage("*index*");
        }

        [Test]
        public void Test_Get_With_Index_After_End_Of_Collection()
        {
            ICollection<char> collection = SampleCollection();
            Action action = () => collection.Get(collection.Count());

            action.Should()
                .ThrowExactly<ArgumentOutOfRangeException>()
                .WithMessage("*index*");
        }

        [Test]
        public void Test_Get_With_An_Empty_Collection()
        {
            ICollection<char> collection = EmptyCollection();
            Action action = () => collection.Get(0);

            action.Should()
                .ThrowExactly<ArgumentOutOfRangeException>()
                .WithMessage("*index*");
        }
        #endregion

        #region GetOrElse method
        [Test]
        public void Test_GetOrElse_With_Valid_Index()
        {
            ICollection<char> collection = SampleCollection();
            char[] expected = SampleCollection().ToArray();

            for (int n = 0; n < expected.Length; ++n)
            {
                char expectedValue = expected[n];
                collection.GetOrElse(n, 'z').Should().Be(
                    expected: expectedValue,
                    because: $"that's the value at {n}th position"
                );
            }
        }

        [Test]
        public void Test_GetOrElse_With_Index_Before_Start_Of_Collection()
        {
            ICollection<char> collection = SampleCollection();
            char expected = 'z';

            collection.GetOrElse(-1, expected).Should().Be(expected);
        }

        [Test]
        public void Test_GetOrElse_With_Index_After_End_Of_Collection()
        {
            ICollection<char> collection = SampleCollection();
            char expected = 'z';

            collection.GetOrElse(collection.Count(), expected).Should().Be(expected);
        }

        [Test]
        public void Test_GetOrElse_With_Empty_Collection()
        {
            ICollection<char> collection = EmptyCollection();
            char expected = 'z';

            collection.GetOrElse(0, expected).Should().Be(expected);
        }

        [Test]
        public void Test_GetOrElse_With_Null_Collection()
        {
            ICollection<char> collection = null;
            Action action = () => collection.GetOrElse(0, 'z');

            action.Should()
                .ThrowExactly<ArgumentNullException>()
                .WithMessage("*collection*");
        }
        #endregion

        #region IsEmpty
        [Test]
        public void Test_IsEmpty_With_Not_Empty_Collection()
        {
            ICollection<char> enumerable = SampleCollection();

            enumerable.IsEmpty().Should().BeFalse();
        }

        [Test]
        public void Test_IsEmpty_With_An_Empty_Collection()
        {
            ICollection<char> enumerable = EmptyCollection();

            enumerable.IsEmpty().Should().BeTrue();
        }

        [Test]
        public void Test_IsEmpty_With_Null_Collection()
        {
            ICollection<char> collection = null;
            Action action = () => collection.IsEmpty();

            action.Should()
                .ThrowExactly<ArgumentNullException>()
                .WithMessage("*collection*");
        }
        #endregion

        #region IsNotEmpty
        [Test]
        public void Test_IsNotEmpty_With_Not_Empty_Collection()
        {
            ICollection<char> collection = SampleCollection();

            collection.IsNotEmpty().Should().BeTrue();
        }

        [Test]
        public void Test_IsNotEmpty_With_An_Empty_Collection()
        {
            ICollection<char> collection = EmptyCollection();

            collection.IsNotEmpty().Should().BeFalse();
        }

        [Test]
        public void Test_IsNotEmpty_With_Null_Collection()
        {
            ICollection<char> collection = null;
            Action action = () => collection.IsNotEmpty();

            action.Should()
                .ThrowExactly<ArgumentNullException>()
                .WithMessage("*collection*");
        }
        #endregion

        private static ICollection<char> EmptyCollection()
            => Array.Empty<char>();

        private static ICollection<char> SampleCollection()
            => new char[] { 'a', 'b', 'c', 'd' };
    }
}
