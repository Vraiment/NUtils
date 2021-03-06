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
using System.Text;

namespace NUtils.MethodBuilders.ToString
{
    internal delegate Expression AppendExpressionOfType(Expression stringBuilder, Type type, Expression value);

    internal sealed class ToStringExpressionBuilder
    {
        private readonly List<IValue> values = new List<IValue>();

        private AppendExpressionOfType appendExpressionOfType;

        public ToStringExpressionBuilder WithValues(IEnumerable<IValue> values)
        {
            this.values.AddRange(values);

            return this;
        }

        public ToStringExpressionBuilder WithAppender(AppendExpressionOfType appender)
        {
            appendExpressionOfType = appender;

            return this;
        }

        public Expression<ToStringMethod<T>> BuildForType<T>()
        {
            if (values.Count == 0)
            {
                return _ => "{}";
            }
            else
            {
                return BuildNonEmptyExpression<T>();
            }
        }

        private Expression<ToStringMethod<T>> BuildNonEmptyExpression<T>()
        {
            ParameterExpression thisParameter = Expression.Parameter(typeof(T), "this");
            ParameterExpression stringBuilder = Expression.Parameter(typeof(StringBuilder), "stringBuilder");

            List<Expression> bodyExpressions = new List<Expression>();

            void addStringBuilderAppendString(Expression expression)
            {
                expression = Expression.Call(stringBuilder, Reflections.StringBuilder.AppendStringMethod, expression);

                bodyExpressions.Add(expression);
            }

            void addStringBuilderAppendValue(IValue value)
            {
                Expression valueExpression = value.BuildExpression(thisParameter);

                bodyExpressions.Add(appendExpressionOfType(stringBuilder, value.Type, valueExpression));
            }

            bodyExpressions.Add(Expression.Assign(stringBuilder, Expression.New(Reflections.StringBuilder.EmptyConstructor)));

            addStringBuilderAppendString(Expression.Constant("{", typeof(string)));

            addStringBuilderAppendString(Expression.Constant($"{values[0].Name}=", typeof(string)));
            addStringBuilderAppendValue(values[0]);

            for (int n = 1; n < values.Count; ++n)
            {
                addStringBuilderAppendString(Expression.Constant($", {values[n].Name}=", typeof(string)));
                addStringBuilderAppendValue(values[n]);
            }

            addStringBuilderAppendString(Expression.Constant("}"));

            bodyExpressions.Add(Expression.Call(stringBuilder, Reflections.Object.ToStringMethod));

            BlockExpression body = Expression.Block(
                typeof(string), // return type
                new ParameterExpression[] { stringBuilder },
                bodyExpressions
            );

            return Expression.Lambda<ToStringMethod<T>>(body, thisParameter);
        }
    }
}
