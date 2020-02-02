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
    internal static class CharAppender
    {
        private static readonly ConstantExpression Quote
            = Expression.Constant('\'', typeof(char));

        private static readonly ConstantExpression Backslash
            = Expression.Constant('\\', typeof(char));

        public static Expression AppendExpression(Expression stringBuilder, Expression value)
        {
            ParameterExpression valueVariable = Expression.Variable(typeof(char), "value");

            BlockExpression block = Expression.Block(
                new ParameterExpression[] { valueVariable },
                Expression.Assign(valueVariable, value),
                AppendCharExpression(stringBuilder, Quote),
                EscapeCharacter(stringBuilder, valueVariable),
                AppendCharExpression(stringBuilder, valueVariable),
                AppendCharExpression(stringBuilder, Quote)
            );

            return block;
        }

        private static ConditionalExpression EscapeCharacter(Expression stringBuilder, ParameterExpression value)
        {
            BinaryExpression isQuote = Expression.Equal(value, Quote);
            BinaryExpression isBackslash = Expression.Equal(value, Backslash);
            BinaryExpression isEscapableCharacter = Expression.Or(isQuote, isBackslash);

            ConditionalExpression result = Expression.IfThen(
                isEscapableCharacter,
                AppendCharExpression(stringBuilder, Backslash)
            );

            return result;
        }

        private static MethodCallExpression AppendCharExpression(Expression stringBuilder, Expression charExpression)
        {
            return Expression.Call(stringBuilder, Reflections.StringBuilder.AppendCharMethod, charExpression);
        }
    }
}
