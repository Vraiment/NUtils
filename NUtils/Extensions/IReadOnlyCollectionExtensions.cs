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
    public static class IReadOnlyCollectionExtensions
    {
        /// <summary>
        /// Gets the element at <paramref name="index"/> from <paramref name="collection"/>
        /// enumerating each element to count them.
        /// </summary>
        /// 
        /// <typeparam name="T">The type of elements in the collection.</typeparam>
        /// 
        /// <param name="collection">The input collection.</param>
        /// <param name="index">The index.</param>
        /// 
        /// <returns>
        /// The value at <paramref name="index"/> from <paramref name="collection"/>.
        /// </returns>
        /// 
        /// <exception cref="ArgumentNullException">If <paramref name="collection"/> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// If <paramref name="index"/> is out of the <paramref name="collection"/> range.
        /// </exception>
        public static T Get<T>(this IReadOnlyCollection<T> collection, int index)
        {
            Validate.ArgumentNotNull(collection, nameof(collection));
            Validate.ArgumentInRange(
                argument: index,
                rangeStart: 0,
                rangeEnd: collection.Count - 1,
                argumentName: nameof(index)
            );

            T result = default;
            int currentIndex = 0;
            foreach (T value in collection)
            {
                if (currentIndex++ == index)
                {
                    result = value;
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// Gets the element at <paramref name="index"/> from <paramref name="collection"/>
        /// enumerating each element to count them, if <paramref name="index"/> is out of
        /// range in the enumerable then returns <paramref name="value"/>.
        /// </summary>
        /// 
        /// <typeparam name="T">The type of elements in the collection.</typeparam>
        /// 
        /// <param name="collection">The input collection.</param>
        /// <param name="index">The index.</param>
        /// <param name="value">The value to return in case if index is out of range in the collection.</param>
        /// 
        /// <returns>
        /// The value at <paramref name="index"/> from <paramref name="collection"/> or
        /// <paramref name="value"/> if <paramref name="index"/> is out of range in the collection.
        /// </returns>
        /// 
        /// <exception cref="ArgumentNullException">If <paramref name="collection"/> is null.</exception>
        public static T GetOrElse<T>(this IReadOnlyCollection<T> collection, int index, T value)
        {
            Validate.ArgumentNotNull(collection, nameof(collection));

            T result = value;
            if (0 <= index && index < collection.Count)
            {
                int currentIndex = 0;
                foreach (T currentValue in collection)
                {
                    if (currentIndex++ == index)
                    {
                        result = currentValue;
                        break;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Returns true if <paramref name="collection"/> is empty.
        /// </summary>
        /// 
        /// <typeparam name="T">The type of elements in the collection.</typeparam>
        /// 
        /// <param name="collection">The input collection.</param>
        /// 
        /// <returns>True if <paramref name="collection"/> is empty, false otherwise.</returns>
        /// 
        /// <exception cref="ArgumentNullException">If <paramref name="collection"/> is null.</exception>
        public static bool IsEmpty<T>(this IReadOnlyCollection<T> collection)
        {
            Validate.ArgumentNotNull(collection, nameof(collection));

            return collection.Count == 0;
        }

        /// <summary>
        /// Returns true if <paramref name="collection"/> is not empty.
        /// </summary>
        /// 
        /// <typeparam name="T">The type of elements in the collection.</typeparam>
        /// 
        /// <param name="collection">The input enumerable.</param>
        /// 
        /// <returns>True if <paramref name="collection"/> is not empty, false otherwise.</returns>
        /// 
        /// <exception cref="ArgumentNullException">If <paramref name="collection"/> is null.</exception>
        public static bool IsNotEmpty<T>(this IReadOnlyCollection<T> collection)
        {
            Validate.ArgumentNotNull(collection, nameof(collection));

            return collection.Count != 0;
        }
    }
}
