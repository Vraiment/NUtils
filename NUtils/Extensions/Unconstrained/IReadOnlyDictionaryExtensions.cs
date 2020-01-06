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

namespace NUtils.Extensions.Unconstrained
{
    public static class IReadOnlyDictionaryExtensions
    {
        /// <summary>
        /// Retrieves the value with key <paramref name="key"/> from <paramref name="dictionary"/>.
        /// </summary>
        /// 
        /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
        /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
        /// 
        /// <param name="dictionary">The input dictionary.</param>
        /// <param name="key">The input key.</param>
        /// 
        /// <returns>The value of <paramref name="key"/> in <paramref name="dictionary"/>.</returns>
        /// 
        /// <exception cref="ArgumentNullException">If either <paramref name="dictionary"/> is null.</exception>
        public static TValue Get<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary, TKey key)
        {
            Validate.ArgumentNotNull(dictionary, nameof(dictionary));

            return dictionary[key];
        }

        /// <summary>
        /// Retrieves the value with key <paramref name="key"/> from <paramref name="dictionary"/> if present,
        /// otherwise <paramref name="value"/> is returned.
        /// </summary>
        /// 
        /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
        /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
        /// 
        /// <param name="dictionary">The input dictionary.</param>
        /// <param name="key">The input key.</param>
        /// <param name="value">The value to return if the key is not found.</param>
        /// 
        /// <returns>
        /// The value for <paramref name="key"/> in <paramref name="dictionary"/> or
        /// <paramref name="value"/> if not found.
        /// </returns>
        /// 
        /// <exception cref="ArgumentNullException">If <paramref name="dictionary"/> is null.</exception>
        public static TValue GetOrElse<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            Validate.ArgumentNotNull(dictionary, nameof(dictionary));

            if (dictionary.ContainsKey(key))
            {
                return dictionary[key];
            }
            else
            {
                return value;
            }
        }
    }
}
