/*
 * Copyright (c) 2020 Vraiment
 *
 * This source code file is subject to the terms of the MIT license.
 * If a copy of the MIT license was not distributed with this file,
 * You can obtain one at https://opensource.org/licenses/MIT.
 */
using FluentAssertions;
using NUnit.Framework;
using NUtils.MethodBuilders;

namespace NUtils.Tests.MethodBuilders
{
    class ToStringTests
    {
        #region Class without values test cases
        [Test]
        public void Test_ToString_Method_With_A_Class_Without_Values()
        {
            ToStringMethod<EmptyClass> toStringMethod = new ToStringMethodBuilder<EmptyClass>()
                .UseProperties()
                .Build();

            string result = toStringMethod(new EmptyClass());

            result.Should().Be("{}");
        }

        class EmptyClass
        {
        }
        #endregion

        #region Boolean test cases
        [TestCaseSource(nameof(BooleanTestCases))]
        public void Test_ToString_Method_For_Type_With_Boolean_Property<T>(TestCase<T> testCase)
        {
            ToStringMethod<T> toStringMethod = new ToStringMethodBuilder<T>()
                .UseProperties()
                .Build();

            string result = toStringMethod(testCase.Instance);

            result.Should().Be(testCase.Expected);
        }

        static object[] BooleanTestCases() => new object[]
        {
            new TestCase<PropertyOfType<bool>>(
                instance: new PropertyOfType<bool>(true),
                expected: "{Value=True}",
                description: "Creating a string for a class with a bool property with value \"true\""
            ),
            new TestCase<PropertyOfType<bool>>(
                instance: new PropertyOfType<bool>(false),
                expected: "{Value=False}",
                description: "Creating a string for a class with a bool property with value \"false\""
            )
        };
        #endregion

        #region Class with multiple values test cases
        [Test]
        public void Test_ToString_MethodWith_A_Class_With_Multiple_Values()
        {
            ToStringMethod<ClassWithMultipleValues> toStringMethod = new ToStringMethodBuilder<ClassWithMultipleValues>()
                .UseProperties()
                .Build();

            ClassWithMultipleValues instance = new ClassWithMultipleValues
            {
                Value1 = true,
                Value2 = false
            };

            string result = toStringMethod(instance);

            result.Should().Be($"{{Value1={instance.Value1}, Value2={instance.Value2}}}");
        }

        class ClassWithMultipleValues
        {
            public bool Value1 { get; set; }

            public bool Value2 { get; set; }
        }
        #endregion

        #region Common types
        public class PropertyOfType<T>
        {
            public PropertyOfType(T value)
            {
                Value = value;
            }

            public T Value { get; }
        }

        public class TestCase<T>
        {
            private readonly string description;

            public TestCase(T instance, string expected, string description)
            {
                Instance = instance;
                Expected = expected;
                this.description = description;
            }

            public T Instance { get; }

            public string Expected { get; set; }

            public override string ToString() => description;
        }
        #endregion
    }
}
