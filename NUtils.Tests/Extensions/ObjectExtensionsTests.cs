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
    class ObjectExtensionsTests
    {
        #region Also method
        [Test]
        public void Test_Using_Also_In_An_Object()
        {
            DummyClass instance = new DummyClass()
            {
                Value = nameof(DummyClass)
            };
            string actionResult = null;

            DummyClass result = instance.Also(argument =>
            {
                argument.Should().BeSameAs(instance);
                actionResult = argument.Value;
            });

            result.Should().BeSameAs(instance);
            actionResult.Should().BeSameAs(instance.Value);
        }

        public void Test_Using_Also_In_A_Null_Object()
        {
            DummyClass instance = null;
            string expected = nameof(DummyClass);
            string actionResult = null;

            DummyClass result = instance.Also(argument =>
            {
                argument.Should().BeNull();
                actionResult = expected;
            });

            result.Should().BeNull();
            actionResult.Should().Be(expected);
        }

        [Test]
        public void Test_Using_Also_With_A_Null_Action()
        {
            DummyClass instance = new DummyClass();
            Action action = () => instance.Also(null);

            action.Should()
                .ThrowExactly<ArgumentNullException>()
                .WithMessage($"*action*");
        }
        #endregion

        #region Let method
        [Test]
        public void Test_Using_Let_In_An_Object()
        {
            DummyClass instance = new DummyClass()
            {
                Value = nameof(DummyClass)
            };

            string result = instance.Let(argument =>
            {
                argument.Should().BeSameAs(instance);

                return argument.Value;
            });

            result.Should().BeSameAs(instance.Value);
        }

        [Test]
        public void Test_Using_Let_In_A_Null_Object()
        {
            DummyClass instance = null;
            string expected = nameof(DummyClass);

            string result = instance.Let(argument =>
            {
                argument.Should().BeNull();

                return expected;
            });

            result.Should().Be(expected);
        }

        [Test]
        public void Test_Using_Let_With_A_Null_Function()
        {
            DummyClass instance = new DummyClass();
            Action action = () => instance.Let<DummyClass, object>(null);

            action.Should()
                .ThrowExactly<ArgumentNullException>()
                .WithMessage($"*function*");
        }
        #endregion

        class DummyClass
        {
            public string Value { get; set; }
        }
    }
}
