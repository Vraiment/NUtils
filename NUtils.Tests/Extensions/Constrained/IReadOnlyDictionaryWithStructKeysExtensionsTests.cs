/*
 * Copyright (c) 2019 Vraiment
 *
 * This source code file is subject to the terms of the MIT license.
 * If a copy of the MIT license was not distributed with this file,
 * You can obtain one at https://opensource.org/licenses/MIT.
 */
using FluentAssertions;
using NUnit.Framework;
using NUtils.Extensions.Constrained;
using System;
using System.Collections.Generic;

namespace NUtils.Tests.Extensions.Constrained
{
    class IReadOnlyDictionaryWithStructKeysExtensionsTests
    {
        #region Get method
        [Test]
        public void Test_Get_With_A_Dictionary_With_Struct_Keys()
        {
            IReadOnlyDictionary<int, string> dictionary = SampleDictionaryWithStructKeys();
            IReadOnlyDictionary<int, string> expected = SampleDictionaryWithStructKeys();

            foreach (KeyValuePair<int, string> pair in expected)
            {
                dictionary.Get(pair.Key).Should().Be(pair.Value);
            }
        }

        [Test]
        public void Test_Get_With_A_Null_Dictionary_With_Struct_Keys()
        {
            IReadOnlyDictionary<int, string> dictionary = null;
            Action action = () => dictionary.Get(default);

            action.Should()
                .ThrowExactly<ArgumentNullException>()
                .WithMessage("*dictionary*");
        }
        #endregion

        #region GetOrElse method
        [Test]
        public void Test_GetOrElse_With_A_Dictionary_With_Struct_Keys()
        {
            IReadOnlyDictionary<int, string> dictionary = SampleDictionaryWithStructKeys();
            IReadOnlyDictionary<int, string> expected = SampleDictionaryWithStructKeys();

            foreach (KeyValuePair<int, string> pair in expected)
            {
                dictionary.GetOrElse(pair.Key, string.Empty).Should().Be(pair.Value);
            }
        }

        [Test]
        public void Test_GetOrElse_With_Key_Not_In_Dictionary_With_Struct_Keys()
        {
            IReadOnlyDictionary<int, string> dictionary = SampleDictionaryWithStructKeys();
            string expected = "Not found";

            dictionary.GetOrElse(default, expected).Should().Be(expected);
        }

        [Test]
        public void Test_GetOrElse_With_A_Null_Dictionary_With_Struct_Keys()
        {
            IReadOnlyDictionary<int, string> dictionary = null;
            Action action = () => dictionary.GetOrElse(default, string.Empty);

            action.Should()
                .ThrowExactly<ArgumentNullException>()
                .WithMessage("*dictionary*");
        }
        #endregion

        private static IReadOnlyDictionary<int, string> SampleDictionaryWithStructKeys() => new Dictionary<int, string>
        {
            [10] = "value1",
            [11] = "value2",
            [12] = "value3"
        };
    }
}
