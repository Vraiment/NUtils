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
    internal static class StringAppender
    {
        private static readonly ConstantExpression NullString
            = Expression.Constant(null, typeof(string));

        private static readonly ConstantExpression CharDoubleQuote
            = Expression.Constant('"', typeof(char));

        private static readonly ConstantExpression DoubleQuote
            = Expression.Constant("\"", typeof(string));

        private static readonly ConstantExpression Backslash
            = Expression.Constant("\\", typeof(string));

        private static readonly ConstantExpression EscapedDoubleQuote
            = Expression.Constant("\\\"", typeof(string));

        private static readonly ConstantExpression EscapedBackslash
            = Expression.Constant("\\\\", typeof(string));

        public static Expression AppendExpression(Expression stringBuilder, Expression value)
        {
            ParameterExpression valueVariable = Expression.Variable(typeof(string), "value");

            BlockExpression block = Expression.Block(
                new ParameterExpression[] { valueVariable },
                Expression.Assign(valueVariable, value),
                Expression.IfThen(
                    Expression.NotEqual(valueVariable, NullString),
                    Expression.Block(
                        AppendCharExpression(stringBuilder, CharDoubleQuote),
                        AppendStringExpression(stringBuilder, EscapeCharactersInString(valueVariable)),
                        AppendCharExpression(stringBuilder, CharDoubleQuote)
                    )
                )
            );

            return block;
        }

        private static MethodCallExpression EscapeCharactersInString(ParameterExpression valueVariable)
        {
            MethodCallExpression stringWithoutBackslashes = Expression.Call(
                valueVariable,
                Reflections.String.ReplaceMethod,
                Backslash,
                EscapedBackslash
            );

            MethodCallExpression result = Expression.Call(
                stringWithoutBackslashes,
                Reflections.String.ReplaceMethod,
                DoubleQuote,
                EscapedDoubleQuote
            );

            return result;
        }

        private static MethodCallExpression AppendCharExpression(Expression stringBuilder, Expression charExpression)
            => Expression.Call(stringBuilder, Reflections.StringBuilder.AppendCharMethod, charExpression);

        private static MethodCallExpression AppendStringExpression(Expression stringBuilder, Expression value)
            => Expression.Call(stringBuilder, Reflections.StringBuilder.AppendStringMethod, value);
    }
}
