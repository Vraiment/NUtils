/*
 * Copyright (c) 2019 Vraiment
 *
 * This source code file is subject to the terms of the MIT license.
 * If a copy of the MIT license was not distributed with this file,
 * You can obtain one at https://opensource.org/licenses/MIT.
 */
using FluentAssertions;
using NUnit.Framework;
using NUtils.Validations;
using System;

namespace NUtils.Tests.Validations
{
    class ValidateTests
    {
        [Test]
        public void Test_ArgumentNotNull_Does_Not_Throw_With_Non_Null_Argument()
        {
            string argument = string.Empty;

            Action action = () => Validate.ArgumentNotNull(argument, nameof(argument));

            action.Should().NotThrow();
        }

        [Test]
        public void Test_ArgumentNotNull_Does_Throw_With_Null_Argument()
        {
            string argument = null;
            string argumentName = nameof(argument);

            Action action = () => Validate.ArgumentNotNull(argument, argumentName);

            action.Should()
                .ThrowExactly<ArgumentNullException>()
                .WithMessage($"*{argumentName}*");
        }

        [Test]
        public void Test_Argument_With_A_True_Condition()
        {
            Action action = () => Validate.Argument(true, string.Empty);

            action.Should().NotThrow();
        }

        [Test]
        public void Test_Argument_With_A_False_Condition()
        {
            string message = "The condition wasn't meet";
            Action action = () => Validate.Argument(false, message);

            action.Should()
                .ThrowExactly<ArgumentException>()
                .WithMessage(message);
        }
    }
}
