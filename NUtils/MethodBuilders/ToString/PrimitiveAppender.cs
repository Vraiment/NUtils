/*
 * Copyright (c) 2020 Vraiment
 *
 * This source code file is subject to the terms of the MIT license.
 * If a copy of the MIT license was not distributed with this file,
 * You can obtain one at https://opensource.org/licenses/MIT.
 */
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace NUtils.MethodBuilders.ToString
{
    internal static class PrimitiveAppender
    {
        private static readonly IReadOnlyDictionary<Type, MethodInfo> AppendPrimitiveMethods
            = new Dictionary<Type, MethodInfo>
            {
                { typeof(bool), Reflections.StringBuilder.AppendBoolMethod },
                { typeof(byte), Reflections.StringBuilder.AppendByteMethod },
                { typeof(short), Reflections.StringBuilder.AppendShortMethod },
                { typeof(ushort), Reflections.StringBuilder.AppendUnsignedShortMethod },
                { typeof(int), Reflections.StringBuilder.AppendIntMethod },
                { typeof(uint), Reflections.StringBuilder.AppendUnsignedIntMethod },
                { typeof(long), Reflections.StringBuilder.AppendLongMethod },
                { typeof(ulong), Reflections.StringBuilder.AppendUnsignedLongMethod },
                { typeof(float), Reflections.StringBuilder.AppendFloatMethod },
                { typeof(double), Reflections.StringBuilder.AppendDoubleMethod }
            };

        public static bool CanAppendType(Type type) => AppendPrimitiveMethods.ContainsKey(type);

        public static Expression AppendExpressionOfType(Expression stringBuilder, Type type, Expression value)
        {
            MethodCallExpression appendExpression = Expression.Call(
                stringBuilder,
                AppendPrimitiveMethods[type],
                value
            );

            return appendExpression;
        }
    }
}
