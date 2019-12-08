/*
 * Copyright (c) 2019 Vraiment
 *
 * This source code file is subject to the terms of the MIT license.
 * If a copy of the MIT license was not distributed with this file,
 * You can obtain one at https://opensource.org/licenses/MIT.
 */
using System;

namespace NUtils.Validations
{
    public static class Validate
    {
        /// <summary>
        /// Validates that <paramref name="argument"/> is not null,
        /// or else throws an <see cref="ArgumentNullException"/> with the given <paramref name="argumentName"/>.
        /// </summary>
        /// 
        /// <typeparam name="T">The type of the argument to check.</typeparam>
        /// 
        /// <param name="argument">The argument to check.</param>
        /// <param name="argumentName">The name of the argument to check.</param>
        public static void ArgumentNotNull<T>(T argument, string argumentName) where T : class
        {
            if (argument is null)
            {
                throw new ArgumentNullException(argumentName);
            }
        }

        /// <summary>
        /// Validates an argument based on <paramref name="condition"/>, if it's false then
        /// throws <see cref="ArgumentException"/> with the given <paramref name="message"/>.
        /// </summary>
        /// 
        /// <param name="condition">The condition to check.</param>
        /// <param name="message">The message for the <see cref="ArgumentException"/>.</param>
        /// 
        /// <exception cref="ArgumentException">If <paramref name="condition"/> is false.</exception>
        public static void Argument(bool condition, string message)
        {
            if (!condition)
            {
                throw new ArgumentException(message);
            }
        }
    }
}
