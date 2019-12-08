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
    public static class StructExtensions
    {
        /// <summary>
        /// Executes the specified action in the input object.
        /// </summary>
        /// 
        /// <example>
        /// One case where this is useful is for example complicated initializations
        /// that require methods other than properties:
        /// 
        /// <code>
        /// var myObject = new MyClass().also(obj =>
        /// {
        ///     obj.MyProperty = 32;
        ///     obj.Setup();
        ///     obj.Configure();
        /// });
        /// </code>
        /// </example>
        /// 
        /// <typeparam name="T">The type of the input object, must be a value type.</typeparam>
        /// 
        /// <param name="object">The input object.</param>
        /// <param name="action">The action to be executed.</param>
        /// 
        /// <returns>The input object.</returns>
        /// 
        /// <exception cref="ArgumentNullException">If <paramref name="action"/> is null.</exception>
        public static T Also<T>(this T @object, Action<T> action) where T : struct
        {
            Validate.ArgumentNotNull(action, nameof(action));

            action(@object);

            return @object;
        }

        /// <summary>
        /// Returns the result of executing the input function with the input object.
        /// </summary>
        /// 
        /// <typeparam name="T">The type of the input object, must be a value type.</typeparam>
        /// <typeparam name="TResult">The type of the object to be returned.</typeparam>
        /// 
        /// <param name="object">The input object.</param>
        /// <param name="function">The function to be executed.</param>
        /// 
        /// <returns>The result of executing the function with the input object.</returns>
        /// 
        /// <exception cref="ArgumentNullException">If <paramref name="function"/> are null.</exception>
        public static TResult Let<T, TResult>(this T @object, Func<T, TResult> function) where T : struct
        {
            Validate.ArgumentNotNull(function, nameof(function));

            return function(@object);
        }

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
