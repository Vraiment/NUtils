/*
 * Copyright (c) 2020 Vraiment
 *
 * This source code file is subject to the terms of the MIT license.
 * If a copy of the MIT license was not distributed with this file,
 * You can obtain one at https://opensource.org/licenses/MIT.
 */
using System;
using System.Linq.Expressions;

namespace NUtils.MethodBuilders.ToString
{
    internal static class ObjectAppender
    {
        public static Expression AppendExpression(Expression stringBuilder, Type type, Expression value)
        {
            if (type.IsValueType)
            {
                return AppendValueType(stringBuilder, value);
            }
            else
            {
                return AppendReferenceType(stringBuilder, value);
            }
        }

        private static MethodCallExpression AppendValueType(Expression stringBuilder, Expression value)
        {
            MethodCallExpression valueAsString = Expression.Call(value, Reflections.Object.ToStringMethod);
            MethodCallExpression result = Expression.Call(stringBuilder, Reflections.StringBuilder.AppendStringMethod, valueAsString);

            return result;
        }

        private static MethodCallExpression AppendReferenceType(Expression stringBuilder, Expression value)
        {
            UnaryExpression valueAsObject = Expression.Convert(value, typeof(object));
            MethodCallExpression result = Expression.Call(stringBuilder, Reflections.StringBuilder.AppendObjectMethod, valueAsObject);

            return result;
        }
    }
}
