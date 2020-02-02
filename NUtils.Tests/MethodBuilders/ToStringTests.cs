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

        #region Primitive test cases
        [TestCaseSource(nameof(PrimitiveTestCases))]
        public void Test_ToString_Method_For_Type_With_Primitive_Property<T>(TestCase<T> testCase)
        {
            ToStringMethod<T> toStringMethod = new ToStringMethodBuilder<T>()
                .UseProperties()
                .Build();

            string result = toStringMethod(testCase.Instance);

            result.Should().Be(testCase.Expected);
        }

        static object[] PrimitiveTestCases() => new object[]
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
            ),
            new TestCase<PropertyOfType<byte>>(
                instance: new PropertyOfType<byte>(34),
                expected: "{Value=34}",
                description: "Creating a string for a class with a byte property with value \"34\""
            ),
            new TestCase<PropertyOfType<short>>(
                instance: new PropertyOfType<short>(32000),
                expected: "{Value=32000}",
                description: "Creating a string for a class with a short property with value \"32000\""
            ),
            new TestCase<PropertyOfType<ushort>>(
                instance: new PropertyOfType<ushort>(65000),
                expected: "{Value=65000}",
                description: "Creating a string for a class with an ushort property with value \"65000\""
            ),
            new TestCase<PropertyOfType<int>>(
                instance: new PropertyOfType<int>(2000000000),
                expected: "{Value=2000000000}",
                description: "Creating a string for a class with an int property with value \"2000000000\""
            ),
            new TestCase<PropertyOfType<uint>>(
                instance: new PropertyOfType<uint>(4000000000),
                expected: "{Value=4000000000}",
                description: "Creating a string for a class with an uint property with value \"4000000000\""
            ),
            new TestCase<PropertyOfType<long>>(
                instance: new PropertyOfType<long>(9000000000000000000),
                expected: "{Value=9000000000000000000}",
                description: "Creating a string for a class with a long property with value \"9000000000000000000\""
            ),
            new TestCase<PropertyOfType<ulong>>(
                instance: new PropertyOfType<ulong>(18000000000000000000),
                expected: "{Value=18000000000000000000}",
                description: "Creating a string for a class with an ulong property with value \"18000000000000000000\""
            ),
            new TestCase<PropertyOfType<float>>(
                instance: new PropertyOfType<float>(1.5f),
                expected: "{Value=1.5}",
                description: "Creating a string for a class with a float property with value \"1.5\""
            ),
            new TestCase<PropertyOfType<double>>(
                instance: new PropertyOfType<double>(5.6),
                expected: "{Value=5.6}",
                description: "Creating a string for a class with an double property with value \"5.6\""
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
