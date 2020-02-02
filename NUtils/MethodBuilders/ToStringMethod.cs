/*
 * Copyright (c) 2020 Vraiment
 *
 * This source code file is subject to the terms of the MIT license.
 * If a copy of the MIT license was not distributed with this file,
 * You can obtain one at https://opensource.org/licenses/MIT.
 */
using NUtils.MethodBuilders.ToString;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace NUtils.MethodBuilders
{
    /// <summary>
    /// The signature of the method to generate strings representing
    /// type <typeparamref name="T"/>.
    /// </summary>
    /// 
    /// <param name="object">
    /// The object to generate a string from.
    /// </param>
    /// 
    /// <returns>
    /// The string representing <param name="T"/>.
    /// </returns>
    public delegate string ToStringMethod<T>(T @object);

    /// <summary>
    /// Class to build a new build <see cref="ToStringMethod{T}"/> instances.
    /// </summary>
    /// 
    /// <typeparam name="T">
    /// The type of objects that will be used to generate strings.
    /// </typeparam>
    public sealed class ToStringMethodBuilder<T>
    {
        private bool useProperties = false;

        /// <summary>
        /// Signals the method builder should use the properties in the type.
        /// </summary>
        public ToStringMethodBuilder<T> UseProperties()
        {
            useProperties = true;

            return this;
        }

        /// <summary>
        /// Builds a new <see cref="ToStringMethod{T}"/>.
        /// </summary>
        /// 
        /// <returns>
        /// A new instance of <see cref="ToStringMethod{T}"/>.
        /// </returns>
        public ToStringMethod<T> Build()
        {
            List<IValue> values = new List<IValue>();

            if (useProperties)
            {
                values.AddRange(GetPropertiesAsValues());
            }

            Expression<ToStringMethod<T>> toStringExpression = new ToStringExpressionBuilder()
                .WithValues(values)
                .WithAppender(AppendExpressionOfType)
                .BuildForType<T>();

            return toStringExpression.Compile();
        }

        private static IEnumerable<IValue> GetPropertiesAsValues() => typeof(T)
            .GetProperties()
            .Select(property => new PropertyValue(property));

        private static Expression AppendExpressionOfType(Expression stringBuilder, Type type, Expression value)
        {
            return ObjectAppender.AppendExpression(stringBuilder, value);
        }
    }
}
