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

        /// <summary>
        /// Validates <paramref name="argument"/> is between <paramref name="rangeStart"/> and
        /// <paramref name="rangeEnd"/> (both inclusive) or else throws <see cref="ArgumentOutOfRangeException"/>
        /// with the tiven <paramref name="argumentName"/>.
        /// </summary>
        /// 
        /// <param name="argument">The argument to validate.</param>
        /// <param name="rangeStart">The start of the range to check (inclusive).</param>
        /// <param name="rangeEnd">The end of the range to check (inclusive).</param>
        /// <param name="argumentName">The name of the argument.</param>
        /// 
        /// <exception cref="ArgumentOutOfRangeException">
        /// If <paramref name="argument"/> is not between <paramref name="rangeStart"/> and <paramref name="rangeEnd"/>.
        /// </exception>
        public static void ArgumentInRange(int argument, int rangeStart, int rangeEnd, string argumentName)
        {
            if (rangeStart > argument || argument > rangeEnd)
            {
                throw new ArgumentOutOfRangeException(argumentName);
            }
        }
    }
}
