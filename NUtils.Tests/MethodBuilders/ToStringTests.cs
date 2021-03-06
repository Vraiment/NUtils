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
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NUtils.Tests.MethodBuilders
{
    class ToStringTests
    {
        #region Use nothing test cases
        [Test]
        public void Test_ToString_Method_Using_At_Least_Fields()
        {
            Action action = () => new ToStringMethodBuilder<object>()
                .UseFields()
                .Build();

            action.Should().NotThrow();
        }

        [Test]
        public void Test_ToString_Method_Using_At_Least_Properties()
        {
            Action action = () => new ToStringMethodBuilder<object>()
                .UseProperties()
                .Build();

            action.Should().NotThrow();
        }

        [Test]
        public void Test_ToString_Method_Without_Using_Anything()
        {
            Action action = () => new ToStringMethodBuilder<object>()
                .Build();

            action.Should()
                .ThrowExactly<InvalidOperationException>()
                .WithMessage($"No member to use in {typeof(ToStringMethod<object>).Name}");
        }
        #endregion

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

        #region Fields test cases
        [TestCaseSource(nameof(FieldTestCases))]
        public void Test_ToString_Method_For_Type_With_Fields<T>(TestCase<T> testCase)
            => testCase.Execute(builder => builder.UseFields());

        static object[] FieldTestCases() => new object[]
        {
            TestCase.ForField<int>(
                instance: 91,
                expected: "{value=91}",
                description: "Creating a string for a class with an int field with value \"91\""
            ),
            TestCase.ForField<char>(
                instance: '\'',
                expected: "{value='\\''}",
                description: "Creating a string for a class with a char field with value \"\\'\""
            ),
            TestCase.ForField<string>(
                instance: "a string",
                expected: "{value=\"a string\"}",
                description: "Creating a string for a class with a string field with value \"a string\""
            )
        };
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

        #region Substitution tests
        [TestCaseSource(nameof(SubstitutionTestCases))]
        public void Test_ToString_Method_With_A_Property_That_Gets_Replaced<T>(TestCase<T> testCase)
            => testCase.Execute(builder => builder.UseProperties());

        static object[] SubstitutionTestCases() => new object[]
        {
            TestCase.ForProperty<int>(
                instance: 99,
                expected: "{Value=Replaced value 99}",
                description: "Creating a string for a class with an int property that gets replaced with a function that takes an int",
                configuration: builder => builder.Substitute<int>(
                    nameof(PropertyOfType<int>.Value),
                    value => $"Replaced value {value}"
                )
            ),
            TestCase.ForProperty<Type>(
                instance: typeof(object),
                expected: $"{{Value=The type name is: {typeof(object).Name}}}",
                description: "Creating a string for a class with a Type property that gets replaced with a function that takes an object",
                configuration: builder => builder.Substitute<object>(
                    nameof(PropertyOfType<Type>.Value),
                    value => $"The type name is: {(value as Type)?.Name}"
                )
            )
        };

        [Test]
        public void Test_ToString_Method_Substituting_A_Value_That_Doesnt_Exists()
        {
            string falseName = nameof(falseName);
            Action action = () => new ToStringMethodBuilder<object>()
                .UseProperties()
                .Substitute<object>(falseName, _ => string.Empty)
                .Build();

            action.Should()
                .ThrowExactly<InvalidOperationException>()
                .WithMessage(
                    $"Cannot substitute member named \"{falseName}\" because " +
                    $"it doesn't exists in type \"{typeof(object).Name}\" or was ignored"
                );
        }

        [Test]
        public void Test_Substituting_An_Ignored_Member()
        {
            string name = nameof(PropertyOfType<int>.Value);
            Action action = () => new ToStringMethodBuilder<PropertyOfType<int>>()
                .UseProperties()
                .Ignore(name)
                .Substitute<int>(name, _ => string.Empty)
                .Build();

            action.Should()
                .ThrowExactly<InvalidOperationException>()
                .WithMessage(
                    $"Cannot substitute member named \"{name}\" because " +
                    $"it doesn't exists in type \"{typeof(PropertyOfType<int>).Name}\" or was ignored"
                );
        }

        [Test]
        public void Test_ToString_Method_Substituting_A_Value_With_Incorrect_Type()
        {
            string name = nameof(PropertyOfType<string>.Value);
            Action action = () => new ToStringMethodBuilder<PropertyOfType<string>>()
                .UseProperties()
                .Substitute<int>(name, _ => string.Empty)
                .Build();

            action.Should()
                .ThrowExactly<InvalidOperationException>()
                .WithMessage(
                    $"Cannot substitute member named \"{name}\" because can't be converted from " +
                    $"type \"{typeof(string).Name}\" to type \"{typeof(int).Name}\""
                );
        }
        #endregion

        #region Ignore tests
        [TestCaseSource(nameof(IgnoreTestsCases))]
        public void Test_ToString_With_Ignored_Properties<T>(TestCase<T> testCase)
            => testCase.Execute(builder => builder.UseProperties());

        static object[] IgnoreTestsCases() => new object[]
        {
            new TestCase<ClassWithIgnoredProperty>(
                instance: new ClassWithIgnoredProperty
                {
                    Value = "my value"
                },
                expected: "{Value=\"my value\"}",
                description: "Creating a string for a class with a property marked to be ignored"
            ),
            TestCase.ForProperty<int>(
                instance: 99,
                expected: "{}",
                description: "Creating a string for a class with a property configured to be ignored",
                configuration: builder => builder.Ignore(nameof(PropertyOfType<int>.Value))
            )
        };

        class ClassWithIgnoredProperty
        {
            public string Value { get; set; }

            [ToStringMethodIgnore]
            public int Ignored => throw new InvalidOperationException();
        }

        [Test]
        public void Test_ToString_Method_Ignoring_Unexisting_Property()
        {
            Action action = () => new ToStringMethodBuilder<object>()
                .UseProperties()
                .Ignore(nameof(action))
                .Build();

            action.Should()
                .ThrowExactly<InvalidOperationException>()
                .WithMessage(
                    $"Cannot ignore member named \"{nameof(action)}\" because it doesn't " +
                    $"exist in type \"{typeof(object).Name}\""
                );
        }

        [Test]
        public void Test_ToString_Method_Ignoring_Null_List_Of_Names()
        {
            Action action = () => new ToStringMethodBuilder<object>()
                .Ignore(null)
                .Build();

            action.Should()
                .ThrowExactly<ArgumentNullException>()
                .WithMessage("*names*");
        }

        [Test]
        public void Test_ToString_Method_Ignoring_Null_Name()
        {
            Action action = () => new ToStringMethodBuilder<object>()
                .Ignore(new string[] { null })
                .Build();

            action.Should()
                .ThrowExactly<ArgumentException>()
                .WithMessage("A name cannot be null");
        }
        #endregion

        #region Unsupposed tests
        [TestCaseSource(nameof(UnsupportedTestsCases))]
        public void Test_ToString_Method_With_A_Type_That_Has_Unsupported_Properties<T>(TestCase<T> testCase)
        {
            Action action = () => testCase.Execute(builder => builder.UseProperties());

            action.Should()
                .ThrowExactly<NotSupportedException>()
                .WithMessage(testCase.Expected);
        }

        static readonly string UnsupportedMessage = $"Cannot generate ToString() for type {typeof(object).Name} or for " +
            $"any implementation of {typeof(IEnumerable<>).Name} or {typeof(IEnumerable)}";

        static object[] UnsupportedTestsCases() => new object[]
        {
            TestCase.ForProperty<object>(
                instance: new object(),
                expected: UnsupportedMessage,
                description: "Creating a string for a class with a property of type object"
            ),
            TestCase.ForProperty<object>(
                instance: null,
                expected: UnsupportedMessage,
                description: "Creating a string for a class with a property of type object with value null"
            ),
            TestCase.ForProperty<IEnumerable<int>>(
                instance: new int[] { },
                expected: UnsupportedMessage,
                description: "Creating a string for a class with a property of type IEnumerable<>"
            ),
            TestCase.ForProperty<IEnumerable>(
                instance: new int[] { },
                expected: UnsupportedMessage,
                description: "Creating a string for a class with a property of type IEnumerable<>"
            )
        };
        #endregion

        #region Visiblity tests cases
        [Test]
        public void Test_ToString_Method_With_A_Class_With_Private_And_Internal_Members()
        {
            var toString = new ToStringMethodBuilder<ClassWithMembersWithDifferentAccess>()
                .UseFields()
                .UseProperties()
                .Build();

            string result = toString(new ClassWithMembersWithDifferentAccess());

            result.Should().Be("{}");
        }

        class ClassWithMembersWithDifferentAccess
        {
            private string privateField => nameof(privateField);

            internal string internalField => nameof(internalField);

            internal string InternalProperty { get => nameof(InternalProperty); set { } }

            private string PrivateProperty { get => nameof(PrivateProperty); set { } }

            public string InternalGetter { internal get => nameof(InternalGetter); set { } }

            public string PrivateGetter { private get => nameof(PrivateGetter); set { } }
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
        class FieldOfType<T>
        {
            public T value;

            public FieldOfType(T value)
            {
                this.value = value;
            }

            public static implicit operator FieldOfType<T>(T value)
                => new FieldOfType<T>(value);
        }

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
            public static TestCase<FieldOfType<T>> ForField<T>(T instance, string expected, string description)
                => new TestCase<FieldOfType<T>>(instance, expected, description);

            public static TestCase<FieldOfType<T>> ForField<T>(
                T instance, string expected, string description, Action<ToStringMethodBuilder<FieldOfType<T>>> configuration
            ) => new TestCase<FieldOfType<T>>(instance, expected, description, configuration);

            public static TestCase<PropertyOfType<T>> ForProperty<T>(T instance, string expected, string description)
                => new TestCase<PropertyOfType<T>>(instance, expected, description);

            public static TestCase<PropertyOfType<T>> ForProperty<T>(
                T instance, string expected, string description, Action<ToStringMethodBuilder<PropertyOfType<T>>> configuration
            ) => new TestCase<PropertyOfType<T>>(instance, expected, description, configuration);
        }

        public class TestCase<T>
        {
            private readonly string description;

            private readonly Action<ToStringMethodBuilder<T>> configureToStringMethod;

            public TestCase(T instance, string expected, string description)
            {
                Instance = instance;
                Expected = expected;
                this.description = description;
            }

            public TestCase(T instance, string expected, string description, Action<ToStringMethodBuilder<T>> configuration)
                : this(instance, expected, description)
            {
                configureToStringMethod = configuration;
            }

            public T Instance { get; }

            public string Expected { get; set; }

            public override string ToString() => description;

            public void Execute(Action<ToStringMethodBuilder<T>> configureToStringMethod)
            {
                ToStringMethodBuilder<T> builder = new ToStringMethodBuilder<T>();

                this.configureToStringMethod?.Invoke(builder);
                configureToStringMethod(builder);

                ToStringMethod<T> toStringMethod = builder.Build();

                string result = toStringMethod(Instance);

                result.Should().Be(Expected);
            }
        }
        #endregion
    }
}
