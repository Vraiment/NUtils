/*
 * Copyright (c) 2019 Vraiment
 *
 * This source code file is subject to the terms of the MIT license.
 * If a copy of the MIT license was not distributed with this file,
 * You can obtain one at https://opensource.org/licenses/MIT.
 */
using NUtils.Validations;
using System;

namespace NUtils.Extensions.Constrained
{
    public static class StructExtensions
    {
        /// <summary>
        /// Returns the input object if the predicate is true for that object, null otherwise.
        /// </summary>
        /// 
        /// <example>
        /// One case where this is useful is if you want a default value:
        /// 
        /// void MyMethod(String argument)
        /// {
        ///     argument = argument?.TakeIf(arg => arg.Length == 0) ?? "Default Value";
        ///     // Argument will never be null or empty...
        /// }
        /// </example>
        /// 
        /// <typeparam name="T">The type of the input object, must be a value type.</typeparam>
        /// 
        /// <param name="object">The input object.</param>
        /// <param name="predicate">The predicate to be execute.</param>
        /// 
        /// <returns>The input object if the predicate is true for it, null otherwise.</returns>
        /// 
        /// <exception cref="ArgumentNullException">If <paramref name="predicate"/> are null.</exception>
        public static T? TakeIf<T>(this T @object, Predicate<T> predicate) where T : struct
        {
            Validate.ArgumentNotNull(predicate, nameof(predicate));

            if (predicate(@object))
            {
                return @object;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Returns the input object if the predicate is false for that object, null otherwise.
        /// </summary>
        /// 
        /// This is an inverse version of <see cref="TakeReferenceIf"/>.
        /// 
        /// <typeparam name="T">The type of the input object, must be a value type.</typeparam>
        /// 
        /// <param name="object">The input object.</param>
        /// <param name="predicate">The predicate to be execute.</param>
        /// 
        /// <returns>The input object if the predicate is false for it, null otherwise.</returns>
        /// 
        /// <exception cref="ArgumentNullException">If <paramref name="predicate"/> are null.</exception>
        public static T? TakeUnless<T>(this T @object, Predicate<T> predicate) where T : struct
        {
            Validate.ArgumentNotNull(predicate, nameof(predicate));

            if (!predicate(@object))
            {
                return @object;
            }
            else
            {
                return null;
            }
        }
    }
}
