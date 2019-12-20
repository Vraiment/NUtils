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
            IDictionary<string, string> dictionary = SampleDictionary();
            IDictionary<string, string> expected = SampleDictionary();
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
            IDictionary<string, string> dictionary = SampleDictionary();
            Action action = () => dictionary.ForEach(null);

            action.Should()
                .ThrowExactly<ArgumentNullException>()
                .WithMessage("*action*");
        }
        #endregion

        private static IDictionary<string, string> SampleDictionary() => new Dictionary<string, string>
        {
            ["key1"] = "value1",
            ["key2"] = "value2",
            ["key3"] = "value3"
        };
    }
}
