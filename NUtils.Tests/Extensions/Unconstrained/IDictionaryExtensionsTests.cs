/*
 * Copyright (c) 2019 Vraiment
 *
 * This source code file is subject to the terms of the MIT license.
 * If a copy of the MIT license was not distributed with this file,
 * You can obtain one at https://opensource.org/licenses/MIT.
 */
using FluentAssertions;
using NUnit.Framework;
using NUtils.Extensions.Unconstrained;
using System;
using System.Collections.Generic;

namespace NUtils.Tests.Extensions.Unconstrained
{
    class IDictionaryExtensionsTests
    {
        #region Get method
        [Test]
        public void Test_Get_With_A_Dictionary()
        {
            IDictionary<int, string> dictionary = SampleDictionary();
            IDictionary<int, string> expected = SampleDictionary();

            foreach (KeyValuePair<int, string> pair in expected)
            {
                dictionary.Get(pair.Key).Should().Be(pair.Value);
            }
        }

        [Test]
        public void Test_Get_With_A_Null_Dictionary()
        {
            IDictionary<int, string> dictionary = null;
            Action action = () => dictionary.Get(default);

            action.Should()
                .ThrowExactly<ArgumentNullException>()
                .WithMessage("*dictionary*");
        }
        #endregion

        #region GetOrElse method
        [Test]
        public void Test_GetOrElse_With_A_Dictionary()
        {
            IDictionary<int, string> dictionary = SampleDictionary();
            IDictionary<int, string> expected = SampleDictionary();

            foreach (KeyValuePair<int, string> pair in expected)
            {
                dictionary.GetOrElse(pair.Key, string.Empty).Should().Be(pair.Value);
            }
        }

        [Test]
        public void Test_GetOrElse_With_Key_Not_In_Dictionary()
        {
            IDictionary<int, string> dictionary = SampleDictionary();
            string expected = "Not found";

            dictionary.GetOrElse(default, expected).Should().Be(expected);
        }

        [Test]
        public void Test_GetOrElse_With_A_Null_Dictionary()
        {
            IDictionary<int, string> dictionary = null;
            Action action = () => dictionary.GetOrElse(default, string.Empty);

            action.Should()
                .ThrowExactly<ArgumentNullException>()
                .WithMessage("*dictionary*");
        }
        #endregion

        private static IDictionary<int, string> SampleDictionary() => new Dictionary<int, string>
        {
            [10] = "value1",
            [11] = "value2",
            [12] = "value3"
        };
    }
}
