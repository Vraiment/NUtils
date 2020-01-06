/*
 * Copyright (c) 2019 Vraiment
 *
 * This source code file is subject to the terms of the MIT license.
 * If a copy of the MIT license was not distributed with this file,
 * You can obtain one at https://opensource.org/licenses/MIT.
 */
using NUtils.Validations;
using System;

namespace NUtils.Extensions.Unconstrained
{
    public static class ObjectExtensions
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
        ///     argument = argument?.TakeReferenceIf(arg => arg.Length == 0) ?? "Default Value";
        ///     // Argument will never be null or empty...
        /// }
        /// </example>
        /// 
        /// <typeparam name="T">The type of the input object, must be a reference type.</typeparam>
        /// 
        /// <param name="object">The input object.</param>
        /// <param name="predicate">The predicate to be execute.</param>
        /// 
        /// <returns>The input object if the predicate is true for it, null otherwise.</returns>
        /// 
        /// <exception cref="ArgumentNullException">If <paramref name="object"/> or <paramref name="predicate"/> are null.</exception>
        public static T TakeReferenceIf<T>(this T @object, Predicate<T> predicate) where T : class
        {
            Validate.ArgumentNotNull(@object, nameof(@object));
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
        /// 
        /// This is an inverse version of <see cref="TakeReferenceIf{T}(T, Predicate{T})"/>.
        /// </summary>
        /// 
        /// <typeparam name="T">The type of the input object, must be a reference type.</typeparam>
        /// 
        /// <param name="object">The input object.</param>
        /// <param name="predicate">The predicate to be execute.</param>
        /// 
        /// <returns>The input object if the predicate is false for it, null otherwise.</returns>
        /// 
        /// <exception cref="ArgumentNullException">If <paramref name="object"/> or <paramref name="predicate"/> are null.</exception>
        public static T TakeReferenceUnless<T>(this T @object, Predicate<T> predicate) where T : class
        {
            Validate.ArgumentNotNull(@object, nameof(@object));
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

        /// <summary>
        /// Returns the input object if the predicate is true for that object, null otherwise.
        /// </summary>
        /// 
        /// <example>
        /// One case where this is useful is if you want a default value:
        /// 
        /// void MyMethod(Vector argument)
        /// {
        ///     argument = argument.TakeValueIf(arg => arg.X &lt; 0 || arg.Y &lt; 0) ?? DEFAULT_VECTOR;
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
        /// <exception cref="ArgumentNullException">If <paramref name="predicate"/> is null.</exception>
        public static T? TakeValueIf<T>(this T @object, Predicate<T> predicate) where T : struct
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
        /// 
        /// This is an inverse version of <see cref="TakeValueIf{T}(T, Predicate{T})"/>.
        /// </summary>
        /// 
        /// <typeparam name="T">The type of the input object, must be a value type.</typeparam>
        /// 
        /// <param name="object">The input object.</param>
        /// <param name="predicate">The predicate to be execute.</param>
        /// 
        /// <returns>The input object if the predicate is false for it, null otherwise.</returns>
        /// 
        /// <exception cref="ArgumentNullException">If <paramref name="predicate"/> is null.</exception>
        public static T? TakeValueUnless<T>(this T @object, Predicate<T> predicate) where T : struct
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
