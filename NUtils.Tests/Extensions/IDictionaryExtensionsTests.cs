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

namespace NUtils.Tests.Extensions
{
    class IDictionaryExtensionsTests
    {
        #region ForEach method
        [Test]
        public void Test_For_Each_With_A_Dictionary()
        {
            IDictionary<string, string> dictionary = SampleDictionaryWithClassKeys();
            IDictionary<string, string> expected = SampleDictionaryWithClassKeys();
            ISet<string> keys = new HashSet<string>();

            dictionary.ForEach((key, value) => {
                expected[key].Should().Be(value);

                keys.Add(key);
            });

            expected.Keys.Should().Equal(keys);
        }

        [Test]
        public void Test_For_Each_With_An_Empty_Dictionary()
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();

            dictionary.ForEach((_, __) => Assert.Fail());
        }

        [Test]
        public void Test_For_Each_With_A_Null_Dictionary()
        {
            IDictionary<string, string> dictionary = null;
            Action action = () => dictionary.ForEach((_, __) => { });

            action.Should()
                .ThrowExactly<ArgumentNullException>()
                .WithMessage("*dictionary*");
        }

        [Test]
        public void Test_For_Each_With_A_Null_Action()
        {
            IDictionary<string, string> dictionary = SampleDictionaryWithClassKeys();
            Action action = () => dictionary.ForEach(null);

            action.Should()
                .ThrowExactly<ArgumentNullException>()
                .WithMessage("*action*");
        }
        #endregion

        #region Get method (keys with class types)
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

        #region GetOrElse method (keys with class types)
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

        #region Get method (keys with struct types)
        [Test]
        public void Test_Get_With_A_Dictionary_With_Struct_Keys()
        {
            IDictionary<int, string> dictionary = SampleDictionaryWithStructKeys();
            IDictionary<int, string> expected = SampleDictionaryWithStructKeys();

            foreach (KeyValuePair<int, string> pair in expected)
            {
                dictionary.Get(pair.Key).Should().Be(pair.Value);
            }
        }

        [Test]
        public void Test_Get_With_A_Null_Dictionary_With_Struct_Keys()
        {
            IDictionary<int, string> dictionary = null;
            Action action = () => dictionary.Get(default);

            action.Should()
                .ThrowExactly<ArgumentNullException>()
                .WithMessage("*dictionary*");
        }
        #endregion

        #region GetOrElse method (keys with class types)
        [Test]
        public void Test_GetOrElse_With_A_Dictionary_With_Struct_Keys()
        {
            IDictionary<int, string> dictionary = SampleDictionaryWithStructKeys();
            IDictionary<int, string> expected = SampleDictionaryWithStructKeys();

            foreach (KeyValuePair<int, string> pair in expected)
            {
                dictionary.GetOrElse(pair.Key, string.Empty).Should().Be(pair.Value);
            }
        }

        [Test]
        public void Test_GetOrElse_With_Key_Not_In_Dictionary_With_Struct_Keys()
        {
            IDictionary<int, string> dictionary = SampleDictionaryWithStructKeys();
            string expected = "Not found";

            dictionary.GetOrElse(default, expected).Should().Be(expected);
        }

        [Test]
        public void Test_GetOrElse_With_A_Null_Dictionary_With_Struct_Keys()
        {
            IDictionary<int, string> dictionary = null;
            Action action = () => dictionary.GetOrElse(default, string.Empty);

            action.Should()
                .ThrowExactly<ArgumentNullException>()
                .WithMessage("*dictionary*");
        }
        #endregion

        private static IDictionary<string, string> SampleDictionaryWithClassKeys() => new Dictionary<string, string>
        {
            ["key1"] = "value1",
            ["key2"] = "value2",
            ["key3"] = "value3"
        };

        private static IDictionary<int, string> SampleDictionaryWithStructKeys() => new Dictionary<int, string>
        {
            [10] = "value1",
            [11] = "value2",
            [12] = "value3"
        };
    }
}
