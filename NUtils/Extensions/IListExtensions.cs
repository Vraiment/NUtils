/*
 * Copyright (c) 2019 Vraiment
 *
 * This source code file is subject to the terms of the MIT license.
 * If a copy of the MIT license was not distributed with this file,
 * You can obtain one at https://opensource.org/licenses/MIT.
 */
using NUtils.Validations;
using System;
using System.Collections.Generic;

namespace NUtils.Extensions
{
    public static class IListExtensions
    {
        /// <summary>
        /// Gets the element at <paramref name="index"/> from <paramref name="list"/>.
        /// </summary>
        /// 
        /// <typeparam name="T">The type of elements in the list.</typeparam>
        /// 
        /// <param name="list">The input list.</param>
        /// <param name="index">The index.</param>
        /// 
        /// <returns>
        /// The value at <paramref name="index"/> from <paramref name="list"/>.
        /// </returns>
        /// 
        /// <exception cref="ArgumentNullException">If <paramref name="list"/> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// If <paramref name="index"/> is out of the <paramref name="list"/> range.
        /// </exception>
        public static T Get<T>(this IList<T> list, int index)
        {
            Validate.ArgumentNotNull(list, nameof(list));
            Validate.ArgumentInRange(
                argument: index,
                rangeStart: 0,
                rangeEnd: list.Count - 1,
                argumentName: nameof(index)
            );

            return list[index];
        }

        /// <summary>
        /// Gets the element at <paramref name="index"/> from <paramref name="list"/>,
        /// if <paramref name="index"/> is out of range in the list then returns
        /// <paramref name="value"/>.
        /// </summary>
        /// 
        /// <typeparam name="T">The type of elements in the list.</typeparam>
        /// 
        /// <param name="list">The input list.</param>
        /// <param name="index">The index.</param>
        /// <param name="value">The value to return if index is out of range in the list.</param>
        /// 
        /// <returns>
        /// The value at <paramref name="index"/> from <paramref name="list"/> or
        /// <paramref name="value"/> if <paramref name="index"/> is out of range in the list.
        /// </returns>
        /// 
        /// <exception cref="ArgumentNullException">If <paramref name="list"/> is null.</exception>
        public static T GetOrElse<T>(this IList<T> list, int index, T value)
        {
            Validate.ArgumentNotNull(list, nameof(list));

            if (0 <= index && index < list.Count)
            {
                return list[index];
            }
            else
            {
                return value;
            }
        }
    }
}
