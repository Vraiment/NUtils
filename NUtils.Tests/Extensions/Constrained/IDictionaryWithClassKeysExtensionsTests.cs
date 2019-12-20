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
    class IDictionaryWithClassKeysExtensionsTests
    {
        #region Get method
        [Test]
        public void Test_Get_With_A_Dictionary_With_Class_Keys()
        {
            IDictionary<string, string> dictionary = SampleDictionaryWithClassKeys();
            IDictionary<string, string> expected = SampleDictionaryWithClassKeys();

            foreach (KeyValuePair<string, string> pair in expected)
            {
                dictionary.Get(pair.Key).Should().Be(pair.Value);
            }
        }

        [Test]
        public void Test_Get_With_A_Null_Dictionary_With_Class_Keys()
        {
            IDictionary<string, string> dictionary = null;
            Action action = () => dictionary.Get(string.Empty);

            action.Should()
                .ThrowExactly<ArgumentNullException>()
                .WithMessage("*dictionary*");
        }

        [Test]
        public void Test_Get_With_A_Null_Key()
        {
            IDictionary<string, string> dictionary = SampleDictionaryWithClassKeys();
            Action action = () => dictionary.Get(null);

            action.Should()
                .ThrowExactly<ArgumentNullException>()
                .WithMessage("*key*");
        }
        #endregion

        #region GetOrElse method
        [Test]
        public void Test_GetOrElse_With_A_Dictionary_With_Class_Keys()
        {
            IDictionary<string, string> dictionary = SampleDictionaryWithClassKeys();
            IDictionary<string, string> expected = SampleDictionaryWithClassKeys();

            foreach (KeyValuePair<string, string> pair in expected)
            {
                dictionary.GetOrElse(pair.Key, string.Empty).Should().Be(pair.Value);
            }
        }

        [Test]
        public void Test_GetOrElse_With_Key_Not_In_Dictionary_With_Class_Keys()
        {
            IDictionary<string, string> dictionary = SampleDictionaryWithClassKeys();
            string expected = "Not found";

            dictionary.GetOrElse(string.Empty, expected).Should().Be(expected);
        }

        [Test]
        public void Test_GetOrElse_With_A_Null_Dictionary_With_Class_Keys()
        {
            IDictionary<string, string> dictionary = null;
            Action action = () => dictionary.GetOrElse(string.Empty, string.Empty);

            action.Should()
                .ThrowExactly<ArgumentNullException>()
                .WithMessage("*dictionary*");
        }

        [Test]
        public void Test_GetOrElse_With_A_Null_Key()
        {
            IDictionary<string, string> dictionary = SampleDictionaryWithClassKeys();
            Action action = () => dictionary.GetOrElse(null, string.Empty);

            action.Should()
                .ThrowExactly<ArgumentNullException>()
                .WithMessage("*key*");
        }
        #endregion

        private static IDictionary<string, string> SampleDictionaryWithClassKeys() => new Dictionary<string, string>
        {
            ["key1"] = "value1",
            ["key2"] = "value2",
            ["key3"] = "value3"
        };
    }
}
