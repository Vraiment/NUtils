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
    class StructExtensionsTests
    {
        #region TakeIf method
        [Test]
        public void Test_Using_TakeIf_With_A_True_Predicate()
        {
            DummyStruct instance = new DummyStruct()
            {
                Value = nameof(instance)
            };

            DummyStruct? result = instance.TakeIf(inst => inst.Value.Length != 0);

            result.HasValue.Should().BeTrue();
            result.Value.Should().Be(instance);
        }

        [Test]
        public void Test_Using_TakeIf_With_A_False_Predicate()
        {
            DummyStruct instance = new DummyStruct()
            {
                Value = nameof(instance)
            };

            DummyStruct? result = instance.TakeIf(inst => inst.Value.Length == 0);

            result.HasValue.Should().BeFalse();
        }

        [Test]
        public void Test_Using_TakeIf_With_A_Null_Predicate()
        {
            DummyStruct instance = new DummyStruct();
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
            DummyStruct instance = new DummyStruct()
            {
                Value = nameof(instance)
            };

            DummyStruct? result = instance.TakeUnless(inst => inst.Value.Length == 0);

            result.HasValue.Should().BeTrue();
            result.Value.Should().Be(instance);
        }

        [Test]
        public void Test_Using_TakeUnless_With_A_False_Predicate()
        {
            DummyStruct instance = new DummyStruct()
            {
                Value = nameof(instance)
            };

            DummyStruct? result = instance.TakeUnless(inst => inst.Value.Length != 0);

            result.HasValue.Should().BeFalse();
        }

        [Test]
        public void Test_Using_TakeUnless_With_A_Null_Predicate()
        {
            DummyStruct instance = new DummyStruct();
            Action action = () => instance.TakeUnless(null);

            action.Should()
                .ThrowExactly<ArgumentNullException>()
                .WithMessage($"*predicate*");
        }
        #endregion

        struct DummyStruct
        {
            public string Value { get; set; }
        }
    }
}
