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
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Executes <paramref name="action"/> for each element in
        /// the input <paramref name="enumerable"/> in order.
        /// </summary>
        /// 
        /// <typeparam name="T">The type of elements in the enumerable.</typeparam>
        /// 
        /// <param name="enumerable">The input enumerable.</param>
        /// <param name="action">The input action to execute.</param>
        /// 
        /// <exception cref="ArgumentNullException">
        /// If either <paramref name="enumerable"/> or <paramref name="action"/> are null.
        /// </exception>
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            Validate.ArgumentNotNull(enumerable, nameof(enumerable));
            Validate.ArgumentNotNull(action, nameof(action));

            foreach (T value in enumerable)
            {
                action(value);
            }
        }

        /// <summary>
        /// Executes <paramref name="action"/> for each element in
        /// the input <paramref name="enumerable"/> in order and its 
        /// corresponding index.
        /// </summary>
        /// 
        /// <typeparam name="T">The type of elements in the enumerable.</typeparam>
        /// 
        /// <param name="enumerable">The input enumerable.</param>
        /// <param name="action">The input action to execute.</param>
        /// 
        /// <exception cref="ArgumentNullException">
        /// If either <paramref name="enumerable"/> or <paramref name="action"/> are null.
        /// </exception>
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T, int> action)
        {
            Validate.ArgumentNotNull(enumerable, nameof(enumerable));
            Validate.ArgumentNotNull(action, nameof(action));

            int index = 0;
            foreach (T value in enumerable)
            {
                action(value, index++);
            }
        }

        /// <summary>
        /// Gets the element at <paramref name="index"/> from <paramref name="enumerable"/>.
        /// </summary>
        /// 
        /// <remarks>
        /// This may be useful to chain calls:
        /// 
        /// <code>
        /// GetArray().Get(n).Let(Process);
        /// </code>
        /// </remarks>
        /// 
        /// <typeparam name="T">The type of elements in the enumerable.</typeparam>
        /// 
        /// <param name="enumerable">The input enumerable.</param>
        /// <param name="index">The index.</param>
        /// 
        /// <returns>
        /// The value at <paramref name="index"/> from <paramref name="enumerable"/>.
        /// </returns>
        /// 
        /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// If <paramref name="index"/> is out of the <paramref name="enumerable"/> range.
        /// </exception>
        public static T Get<T>(this IEnumerable<T> enumerable, int index)
        {
            Validate.ArgumentNotNull(enumerable, nameof(enumerable));
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            int currentIndex = 0;
            foreach (T currentValue in enumerable)
            {
                if (currentIndex++ == index)
                {
                    return currentValue;
                }
            }

            throw new ArgumentOutOfRangeException(nameof(index));
        }

        /// <summary>
        /// Gets the element at <paramref name="index"/> from <paramref name="enumerable"/>,
        /// if <paramref name="index"/> is out of range in the enumerable then returns <paramref name="value"/>.
        /// </summary>
        /// 
        /// <typeparam name="T">The type of elements in the enumerable.</typeparam>
        /// 
        /// <param name="enumerable">The input enumerable.</param>
        /// <param name="index">The index.</param>
        /// <param name="value">The value to return in case if index is out of range in the enumerable.</param>
        /// 
        /// <returns>
        /// The value at <paramref name="index"/> from <paramref name="enumerable"/> or
        /// <paramref name="value"/> if <paramref name="index"/> is out of range in the enumerable.
        /// </returns>
        /// 
        /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is null.</exception>
        public static T GetOrElse<T>(this IEnumerable<T> enumerable, int index, T value)
        {
            Validate.ArgumentNotNull(enumerable, nameof(enumerable));
            if (index < 0)
            {
                return value;
            }

            int currentIndex = 0;
            foreach (T currentValue in enumerable)
            {
                if (currentIndex++ == index)
                {
                    return currentValue;
                }
            }

            return value;
        }

        /// <summary>
        /// Returns true if <paramref name="enumerable"/> is empty.
        /// </summary>
        /// 
        /// <typeparam name="T">The type of elements in the enumerable.</typeparam>
        /// 
        /// <param name="enumerable">The input enumerable.</param>
        /// 
        /// <returns>True if <paramref name="enumerable"/> is empty, false otherwise.</returns>
        /// 
        /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is null.</exception>
        public static bool IsEmpty<T>(this IEnumerable<T> enumerable)
        {
            Validate.ArgumentNotNull(enumerable, nameof(enumerable));

            return !enumerable.GetEnumerator().MoveNext();
        }

        /// <summary>
        /// Returns true if <paramref name="enumerable"/> is not empty.
        /// </summary>
        /// 
        /// <typeparam name="T">The type of elements in the enumerable.</typeparam>
        /// 
        /// <param name="enumerable">The input enumerable.</param>
        /// 
        /// <returns>True if <paramref name="enumerable"/> is not empty, false otherwise.</returns>
        /// 
        /// <exception cref="ArgumentNullException">If <paramref name="enumerable"/> is null.</exception>
        public static bool IsNotEmpty<T>(this IEnumerable<T> enumerable)
        {
            Validate.ArgumentNotNull(enumerable, nameof(enumerable));

            return enumerable.GetEnumerator().MoveNext();
        }
    }
}
