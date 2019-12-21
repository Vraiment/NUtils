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
    public static class IReadOnlyDictionaryExtensions
    {
        /// <summary>
        /// Executes the given <paramref name="action"/> for each pair in <paramref name="dictionary"/>.
        /// </summary>
        /// 
        /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
        /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
        /// 
        /// <param name="dictionary">The input dictionary.</param>
        /// <param name="action">The action to execute.</param>
        /// 
        /// <exception cref="ArgumentNullException">
        /// If either <paramref name="dictionary"/> or <paramref name="action"/> are null.
        /// </exception>
        public static void ForEach<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary, Action<TKey, TValue> action)
        {
            Validate.ArgumentNotNull(dictionary, nameof(dictionary));
            Validate.ArgumentNotNull(action, nameof(action));

            foreach (KeyValuePair<TKey, TValue> pair in dictionary)
            {
                action(pair.Key, pair.Value);
            }
        }
    }
}
