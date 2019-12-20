/*
 * Copyright (c) 2019 Vraiment
 *
 * This source code file is subject to the terms of the MIT license.
 * If a copy of the MIT license was not distributed with this file,
 * You can obtain one at https://opensource.org/licenses/MIT.
 */
using NUtils.Validations;
using System;

namespace NUtils.Extensions
{
    public static class ArrayExtensions
    {
        /// <summary>
        /// Gets the element at <paramref name="index"/> from <paramref name="array"/>.
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
        /// <typeparam name="T">The type of the array.</typeparam>
        /// 
        /// <param name="array">The input array.</param>
        /// <param name="index">The index.</param>
        /// 
        /// <returns>
        /// The value at <paramref name="index"/> from <paramref name="array"/>.
        /// </returns>
        /// 
        /// <exception cref="ArgumentNullException">If <paramref name="array"/> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// If <paramref name="index"/> is out of the <paramref name="array"/> range.
        /// </exception>
        public static T Get<T>(this T[] array, int index)
        {
            Validate.ArgumentNotNull(array, nameof(array));
            Validate.ArgumentInRange(index, 0, array.Length - 1, nameof(index));

            return array[index];
        }

        /// <summary>
        /// Gets the element at <paramref name="index"/> from <paramref name="array"/>,
        /// if <paramref name="index"/> is out of range in the array then returns <paramref name="value"/>.
        /// </summary>
        /// 
        /// <typeparam name="T">The type of the array.</typeparam>
        /// 
        /// <param name="array">The input array.</param>
        /// <param name="index">The index.</param>
        /// <param name="value">The value to return in case if index is out of the array.</param>
        /// 
        /// <returns>
        /// The value at <paramref name="index"/> from <paramref name="array"/> or
        /// <paramref name="value"/> if <paramref name="index"/> is out of range in the array.
        /// </returns>
        /// 
        /// <exception cref="ArgumentNullException">If <paramref name="array"/> is null.</exception>
        public static T GetOrElse<T>(this T[] array, int index, T value)
        {
            Validate.ArgumentNotNull(array, nameof(array));

            if (0 <= index && index < array.Length)
            {
                return array[index];
            }
            else
            {
                return value;
            }
        }

        /// <summary>
        /// Returns true if <paramref name="array"/> is empty.
        /// </summary>
        /// 
        /// <typeparam name="T">The type of the array.</typeparam>
        /// 
        /// <param name="array">The input array.</param>
        /// 
        /// <returns>True if <paramref name="array"/> is empty, false otherwise.</returns>
        /// 
        /// <exception cref="ArgumentNullException">If <paramref name="array"/> is null.</exception>
        public static bool IsEmpty<T>(this T[] array)
        {
            Validate.ArgumentNotNull(array, nameof(array));

            return array.Length == 0;
        }

        /// <summary>
        /// Returns true if <paramref name="array"/> is not empty.
        /// </summary>
        /// 
        /// <typeparam name="T">The type of the array.</typeparam>
        /// 
        /// <param name="array">The input array.</param>
        /// 
        /// <returns>True if <paramref name="array"/> is not empty, false otherwise.</returns>
        /// 
        /// <exception cref="ArgumentNullException">If <paramref name="array"/> is null.</exception>
        public static bool IsNotEmpty<T>(this T[] array)
        {
            Validate.ArgumentNotNull(array, nameof(array));

            return array.Length != 0;
        }
    }
}
