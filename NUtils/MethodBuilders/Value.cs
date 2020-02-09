/*
 * Copyright (c) 2020 Vraiment
 *
 * This source code file is subject to the terms of the MIT license.
 * If a copy of the MIT license was not distributed with this file,
 * You can obtain one at https://opensource.org/licenses/MIT.
 */
using System;
using System.Linq.Expressions;
using System.Reflection;

namespace NUtils.MethodBuilders
{
    internal interface IValue
    {
        string Name { get; }

        Type Type { get; }

        Expression BuildExpression(Expression source);
    }

    internal sealed class PropertyValue : IValue
    {
        private readonly PropertyInfo property;

        public PropertyValue(PropertyInfo property)
        {
            this.property = property;
        }

        public string Name => property.Name;

        public Type Type => property.PropertyType;

        public Expression BuildExpression(Expression source)
            => Expression.Call(source, property.GetMethod);
    }

    internal static class SubstituteValue
    {
        public static IValue WithDelegate(IValue value, Delegate substituteMethod)
        {
            Type toStringMethodType = substituteMethod.GetType();
            Type substitutedType = toStringMethodType.GenericTypeArguments[0];

            ValidateDelegate(name: value.Name, actualType: value.Type, substitutedType: substitutedType);

            Type substituteValueType = typeof(SubstituteValue<>)
                .MakeGenericType(new Type[] { substitutedType });
            ConstructorInfo constructor = substituteValueType
                .GetConstructor(new Type[] { typeof(IValue), toStringMethodType });

            return (IValue)constructor.Invoke(new object[] { value, substituteMethod });
        }

        private static void ValidateDelegate(string name, Type actualType, Type substitutedType)
        {
            if (!substitutedType.IsAssignableFrom(actualType))
            {
                throw new InvalidOperationException(
                    $"Cannot substitute member named \"{name}\" because can't be converted from " +
                    $"type \"{actualType.Name}\" to type \"{substitutedType.Name}\""
                );
            }
        }
    }

    internal sealed class SubstituteValue<T> : IValue
    {
        private readonly IValue originalValue;

        private readonly ToStringMethod<T> substituteMethod;

        public SubstituteValue(IValue originalValue, ToStringMethod<T> substituteMethod)
        {
            this.originalValue = originalValue;
            this.substituteMethod = substituteMethod;
        }

        public string Name => originalValue.Name;

        public Type Type => GetType();

        public Expression BuildExpression(Expression source)
        {
            MethodInfo invokeMethod = substituteMethod.GetType()
                .GetMethod(nameof(substituteMethod.Invoke));
            ConstantExpression substituteMethodExpression = Expression.Constant(substituteMethod, substituteMethod.GetType());
            Expression valueExpression = originalValue.BuildExpression(source);
            MethodCallExpression result = Expression.Call(substituteMethodExpression, invokeMethod, valueExpression);

            return result;
        }
    }
}
