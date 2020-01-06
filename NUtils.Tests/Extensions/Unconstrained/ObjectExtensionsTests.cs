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

namespace NUtils.Tests.Extensions.Unconstrained
{
    class ObjectExtensionsTests
    {
        #region TakeReferenceIf method
        [Test]
        public void Test_Using_TakeReferenceIf_With_A_True_Predicate()
        {
            DummyClass instance = new DummyClass()
            {
                Value = nameof(instance)
            };

            DummyClass result = instance.TakeReferenceIf(inst => inst.Value.Length != 0);

            result.Should().NotBeNull().And.BeSameAs(instance);
        }

        [Test]
        public void Test_Using_TakeReferenceIf_With_A_False_Predicate()
        {
            DummyClass instance = new DummyClass()
            {
                Value = nameof(instance)
            };

            DummyClass result = instance.TakeReferenceIf(inst => inst.Value.Length == 0);

            result.Should().BeNull();
        }

        [Test]
        public void Test_Using_TakeReferenceIf_With_A_Null_Object()
        {
            DummyClass instance = null;
            Action action = () => instance.TakeReferenceIf(_ => true);

            action.Should()
                .ThrowExactly<ArgumentNullException>()
                .WithMessage($"*object*");
        }

        [Test]
        public void Test_Using_TakeReferenceIf_With_A_Null_Predicate()
        {
            DummyClass instance = new DummyClass();
            Action action = () => instance.TakeReferenceIf(null);

            action.Should()
                .ThrowExactly<ArgumentNullException>()
                .WithMessage($"*predicate*");
        }
        #endregion

        #region TakeReferenceUnless method
        [Test]
        public void Test_Using_TakeReferenceUnless_With_A_True_Predicate()
        {
            DummyClass instance = new DummyClass()
            {
                Value = nameof(instance)
            };

            DummyClass result = instance.TakeReferenceUnless(inst => inst.Value.Length == 0);

            result.Should().NotBeNull().And.BeSameAs(instance);
        }

        [Test]
        public void Test_Using_TakeReferenceUnless_With_A_False_Predicate()
        {
            DummyClass instance = new DummyClass()
            {
                Value = nameof(instance)
            };

            DummyClass result = instance.TakeReferenceUnless(inst => inst.Value.Length != 0);

            result.Should().BeNull();
        }

        [Test]
        public void Test_Using_TakeReferenceUnless_With_A_Null_Object()
        {
            DummyClass instance = null;
            Action action = () => instance.TakeReferenceUnless(_ => false);

            action.Should()
                .ThrowExactly<ArgumentNullException>()
                .WithMessage($"*object*");
        }

        [Test]
        public void Test_Using_TakeUnless_With_A_Null_Predicate()
        {
            DummyClass instance = new DummyClass();
            Action action = () => instance.TakeReferenceUnless(null);

            action.Should()
                .ThrowExactly<ArgumentNullException>()
                .WithMessage($"*predicate*");
        }
        #endregion

        class DummyClass
        {
            public string Value { get; set; }
        }

        #region TakeValueIf method
        [Test]
        public void Test_Using_TakeValueIf_With_A_True_Predicate()
        {
            DummyStruct instance = new DummyStruct()
            {
                Value = nameof(instance)
            };

            DummyStruct? result = instance.TakeValueIf(inst => inst.Value.Length != 0);

            result.HasValue.Should().BeTrue();
            result.Value.Should().Be(instance);
        }

        [Test]
        public void Test_Using_TakeValueIf_With_A_False_Predicate()
        {
            DummyStruct instance = new DummyStruct()
            {
                Value = nameof(instance)
            };

            DummyStruct? result = instance.TakeValueIf(inst => inst.Value.Length == 0);

            result.HasValue.Should().BeFalse();
        }

        [Test]
        public void Test_Using_TakeValueIf_With_A_Null_Predicate()
        {
            DummyStruct instance = new DummyStruct();
            Action action = () => instance.TakeValueIf(null);

            action.Should()
                .ThrowExactly<ArgumentNullException>()
                .WithMessage($"*predicate*");
        }
        #endregion

        #region TakeValueUnless method
        [Test]
        public void Test_Using_TakeValueUnless_With_A_True_Predicate()
        {
            DummyStruct instance = new DummyStruct()
            {
                Value = nameof(instance)
            };

            DummyStruct? result = instance.TakeValueUnless(inst => inst.Value.Length == 0);

            result.HasValue.Should().BeTrue();
            result.Value.Should().Be(instance);
        }

        [Test]
        public void Test_Using_TakeValueUnless_With_A_False_Predicate()
        {
            DummyStruct instance = new DummyStruct()
            {
                Value = nameof(instance)
            };

            DummyStruct? result = instance.TakeValueUnless(inst => inst.Value.Length != 0);

            result.HasValue.Should().BeFalse();
        }

        [Test]
        public void Test_Using_TakeValueUnless_With_A_Null_Predicate()
        {
            DummyStruct instance = new DummyStruct();
            Action action = () => instance.TakeValueUnless(null);

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
