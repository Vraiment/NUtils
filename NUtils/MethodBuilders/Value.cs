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
}
