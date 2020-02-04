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
using System;

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
            => testCase.Execute(builder => builder.UseProperties());

        static object[] PrimitiveTestCases() => new object[]
        {
            TestCase.ForProperty<bool>(
                instance: true,
                expected: "{Value=True}",
                description: "Creating a string for a class with a bool property with value \"true\""
            ),
            TestCase.ForProperty<bool>(
                instance: false,
                expected: "{Value=False}",
                description: "Creating a string for a class with a bool property with value \"false\""
            ),
            TestCase.ForProperty<byte>(
                instance: 34,
                expected: "{Value=34}",
                description: "Creating a string for a class with a byte property with value \"34\""
            ),
            TestCase.ForProperty<short>(
                instance: 32000,
                expected: "{Value=32000}",
                description: "Creating a string for a class with a short property with value \"32000\""
            ),
            TestCase.ForProperty<ushort>(
                instance: 65000,
                expected: "{Value=65000}",
                description: "Creating a string for a class with an ushort property with value \"65000\""
            ),
            TestCase.ForProperty<int>(
                instance: 2000000000,
                expected: "{Value=2000000000}",
                description: "Creating a string for a class with an int property with value \"2000000000\""
            ),
            TestCase.ForProperty<uint>(
                instance: 4000000000,
                expected: "{Value=4000000000}",
                description: "Creating a string for a class with an uint property with value \"4000000000\""
            ),
            TestCase.ForProperty<long>(
                instance: 9000000000000000000,
                expected: "{Value=9000000000000000000}",
                description: "Creating a string for a class with a long property with value \"9000000000000000000\""
            ),
            TestCase.ForProperty<ulong>(
                instance: 18000000000000000000,
                expected: "{Value=18000000000000000000}",
                description: "Creating a string for a class with an ulong property with value \"18000000000000000000\""
            ),
            TestCase.ForProperty<float>(
                instance: 1.5f,
                expected: "{Value=1.5}",
                description: "Creating a string for a class with a float property with value \"1.5\""
            ),
            TestCase.ForProperty<double>(
                instance: 5.6,
                expected: "{Value=5.6}",
                description: "Creating a string for a class with an double property with value \"5.6\""
            )
        };
        #endregion

        #region Char test cases
        [TestCaseSource(nameof(CharTestCases))]
        public void Test_ToString_Method_For_Type_With_Char_Property<T>(TestCase<T> testCase)
            => testCase.Execute(builder => builder.UseProperties());

        static object[] CharTestCases() => new object[]
        {
            TestCase.ForProperty<char>(
                instance: 'a',
                expected: "{Value='a'}",
                description: "Creating a string for a class with a char with value \"a\""
            ),
            TestCase.ForProperty<char>(
                instance: '\'',
                expected: "{Value='\\''}",
                description: "Creating a string for a class with a char with value \"'\""
            ),
            TestCase.ForProperty<char>(
                instance: '\\',
                expected: "{Value='\\\\'}",
                description: "Creating a string for a class with a char with value \"\\\""
            )
        };
        #endregion

        #region String test cases
        [TestCaseSource(nameof(StringTestCases))]
        public void Test_ToString_Method_For_Type_With_String_Property<T>(TestCase<T> testCase)
            => testCase.Execute(builder => builder.UseProperties());

        static object[] StringTestCases() => new object[]
        {
            TestCase.ForProperty<string>(
                instance: null,
                expected: "{Value=}",
                description: "Creating a string for a class with a string property with value null"
            ),
            TestCase.ForProperty<string>(
                instance: string.Empty,
                expected: "{Value=\"\"}",
                description: "Creating a string for a class with a string property with value \"\""
            ),
            TestCase.ForProperty<string>(
                instance: "any value",
                expected: "{Value=\"any value\"}",
                description: "Creating a string for a class with a string property with value \"any value\""
            )
        };
        #endregion

        #region Any objects test cases
        [TestCaseSource(nameof(StringTestCases))]
        public void Test_ToString_Method_For_Type_With_Any_Property<T>(TestCase<T> testCase)
            => testCase.Execute(builder => builder.UseProperties());

        static object[] AnyTestCases() => new object[]
        {
            TestCase.ForProperty<StructWithToString>(
                instance: new StructWithToString(),
                expected: "{Value=This is a struct}",
                description: "Creating a string for a class with a struct property"
            ),
            TestCase.ForProperty<ClassWithToString>(
                instance: new ClassWithToString(),
                expected: "{Value=This is a class}",
                description: "Creating a string for a class with a class property"
            ),
            TestCase.ForProperty<object>(
                instance: new ClassWithToString(),
                expected: "{Value=This is a class}",
                description: "Creating a string for a class with an object property with with some value"
            ),
            TestCase.ForProperty<object>(
                instance: null,
                expected: "{Value=}",
                description: "Creating a string for a class with an object property with null value"
            )
        };

        struct StructWithToString
        {
            public override string ToString() => "This is a struct";
        }

        class ClassWithToString
        {
            public override string ToString() => "This is a class";
        }
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
        class PropertyOfType<T>
        {
            public PropertyOfType(T value)
            {
                Value = value;
            }

            public T Value { get; }

            public static implicit operator PropertyOfType<T>(T value)
                => new PropertyOfType<T>(value);
        }

        class TestCase
        {
            public static TestCase<PropertyOfType<T>> ForProperty<T>(T instance, string expected, string description)
                => new TestCase<PropertyOfType<T>>(instance, expected, description);
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

            public void Execute(Action<ToStringMethodBuilder<T>> configureToStringMethod)
            {
                ToStringMethodBuilder<T> builder = new ToStringMethodBuilder<T>();

                configureToStringMethod(builder);


                ToStringMethod<T> toStringMethod = builder.Build();

                string result = toStringMethod(Instance);

                result.Should().Be(Expected);
            }
        }
        #endregion
    }
}
