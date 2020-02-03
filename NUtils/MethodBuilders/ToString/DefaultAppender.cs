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
    internal static class DefaultAppender
    {
        public static Expression AppendExpression(Expression stringBuilder, Type type, Expression value)
        {
            if (PrimitiveAppender.CanAppendType(type))
            {
                return PrimitiveAppender.AppendExpressionOfType(stringBuilder, type, value);
            }
            else if (type == typeof(char))
            {
                return CharAppender.AppendExpression(stringBuilder, value);
            }
            else if (type == typeof(string))
            {
                return StringAppender.AppendExpression(stringBuilder, value);
            }
            else
            {
                return ObjectAppender.AppendExpression(stringBuilder, value);
            }
        }
    }
}
