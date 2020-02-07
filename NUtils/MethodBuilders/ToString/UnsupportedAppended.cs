/*
 * Copyright (c) 2020 Vraiment
 *
 * This source code file is subject to the terms of the MIT license.
 * If a copy of the MIT license was not distributed with this file,
 * You can obtain one at https://opensource.org/licenses/MIT.
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace NUtils.MethodBuilders.ToString
{
    internal static class UnsupportedAppended
    {
        public static bool CanAppendType(Type type)
        {
            return type == typeof(object) || IsEnumerable(type) || type.GetInterfaces().Any(IsEnumerable);
        }

        public static Expression AppendExpression(Expression stringBuilder, Type type, Expression value)
        {
            throw new NotSupportedException(
                $"Cannot generate ToString() for type {typeof(object).Name} or for " +
                $"any implementation of {typeof(IEnumerable<>).Name} or {typeof(IEnumerable)}"
            );
        }

        private static bool IsEnumerable(Type type)
            => type != typeof(string) && (IsGenericEnumerable(type) || type == typeof(IEnumerable));

        private static bool IsGenericEnumerable(Type type)
            => type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IEnumerable<>);
    }
}
