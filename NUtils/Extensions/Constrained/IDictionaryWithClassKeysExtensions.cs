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

namespace NUtils.Extensions.Constrained
{
    public static class IDictionaryWithClassKeysExtensions
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
        /// <exception cref="ArgumentNullException">
        /// If either <paramref name="dictionary"/> or <paramref name="key"/> are null.
        /// </exception>
        public static TValue Get<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key) where TKey : class
        {
            Validate.ArgumentNotNull(dictionary, nameof(dictionary));
            Validate.ArgumentNotNull(key, nameof(key));

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
        /// <exception cref="ArgumentNullException">
        /// If either <paramref name="dictionary"/> or <paramref name="key"/> are null.
        /// </exception>
        public static TValue GetOrElse<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue value) where TKey : class
        {
            Validate.ArgumentNotNull(dictionary, nameof(dictionary));
            Validate.ArgumentNotNull(key, nameof(key));

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
