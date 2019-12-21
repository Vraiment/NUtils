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

namespace NUtils.Tests.Extensions.Constrained
{
    class ClassExtensionsTests
    {
        #region TakeIf method
        [Test]
        public void Test_Using_TakeIf_With_A_True_Predicate()
        {
            DummyClass instance = new DummyClass()
            {
                Value = nameof(instance)
            };

            DummyClass result = instance.TakeIf(inst => inst.Value.Length != 0);

            result.Should().NotBeNull().And.BeSameAs(instance);
        }

        [Test]
        public void Test_Using_TakeIf_With_A_False_Predicate()
        {
            DummyClass instance = new DummyClass()
            {
                Value = nameof(instance)
            };

            DummyClass result = instance.TakeIf(inst => inst.Value.Length == 0);

            result.Should().BeNull();
        }

        [Test]
        public void Test_Using_TakeIf_With_A_Null_Object()
        {
            DummyClass instance = null;
            Action action = () => instance.TakeIf(_ => true);

            action.Should()
                .ThrowExactly<ArgumentNullException>()
                .WithMessage($"*object*");
        }

        [Test]
        public void Test_Using_TakeIf_With_A_Null_Predicate()
        {
            DummyClass instance = new DummyClass();
            Action action = () => instance.TakeIf(null);

            action.Should()
                .ThrowExactly<ArgumentNullException>()
                .WithMessage($"*predicate*");
        }
        #endregion

        #region TakeUnless method
        [Test]
        public void Test_Using_TakeUnless_With_A_True_Predicate()
        {
            DummyClass instance = new DummyClass()
            {
                Value = nameof(instance)
            };

            DummyClass result = instance.TakeUnless(inst => inst.Value.Length == 0);

            result.Should().NotBeNull().And.BeSameAs(instance);
        }

        [Test]
        public void Test_Using_TakeUnless_With_A_False_Predicate()
        {
            DummyClass instance = new DummyClass()
            {
                Value = nameof(instance)
            };

            DummyClass result = instance.TakeUnless(inst => inst.Value.Length != 0);

            result.Should().BeNull();
        }

        [Test]
        public void Test_Using_TakeUnless_With_A_Null_Object()
        {
            DummyClass instance = null;
            Action action = () => instance.TakeUnless(_ => false);

            action.Should()
                .ThrowExactly<ArgumentNullException>()
                .WithMessage($"*object*");
        }

        [Test]
        public void Test_Using_TakeUnless_With_A_Null_Predicate()
        {
            DummyClass instance = new DummyClass();
            Action action = () => instance.TakeUnless(null);

            action.Should()
                .ThrowExactly<ArgumentNullException>()
                .WithMessage($"*predicate*");
        }
        #endregion

        class DummyClass
        {
            public string Value { get; set; }
        }
    }
}
