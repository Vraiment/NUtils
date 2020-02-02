/*
 * Copyright (c) 2020 Vraiment
 *
 * This source code file is subject to the terms of the MIT license.
 * If a copy of the MIT license was not distributed with this file,
 * You can obtain one at https://opensource.org/licenses/MIT.
 */
using System.Linq.Expressions;

namespace NUtils.MethodBuilders.ToString
{
    internal static class ObjectAppender
    {
        public static Expression AppendExpression(Expression stringBuilder, Expression value)
        {
            UnaryExpression valueAsObject = Expression.Convert(value, typeof(object));
            MethodCallExpression appendExpression = Expression.Call(
                stringBuilder,
                Reflections.StringBuilder.AppendObjectMethod,
                valueAsObject
            );

            return appendExpression;
        }
    }
}
