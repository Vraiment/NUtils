/*
 * Copyright (c) 2020 Vraiment
 *
 * This source code file is subject to the terms of the MIT license.
 * If a copy of the MIT license was not distributed with this file,
 * You can obtain one at https://opensource.org/licenses/MIT.
 */
using NUtils.MethodBuilders.ToString;
using NUtils.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace NUtils.MethodBuilders
{
    /// <summary>
    /// The signature of the method to generate strings representing
    /// type <typeparamref name="T"/>.
    /// </summary>
    /// 
    /// <param name="object">
    /// The object to generate a string from.
    /// </param>
    /// 
    /// <returns>
    /// The string representing <param name="T"/>.
    /// </returns>
    public delegate string ToStringMethod<T>(T @object);

    /// <summary>
    /// Class to build a new build <see cref="ToStringMethod{T}"/> instances.
    /// </summary>
    /// 
    /// <typeparam name="T">
    /// The type of objects that will be used to generate strings.
    /// </typeparam>
    public sealed class ToStringMethodBuilder<T>
    {
        private bool useProperties = false;

        private readonly Dictionary<string, Delegate> namedSubstitutes = new Dictionary<string, Delegate>();

        /// <summary>
        /// Signals the method builder should use the properties in the type.
        /// </summary>
        /// 
        /// <returns>The same instance of <see cref="ToStringMethodBuilder{T}"/>.</returns>
        public ToStringMethodBuilder<T> UseProperties()
        {
            useProperties = true;

            return this;
        }

        /// <summary>
        /// Substitutes the logic to generate a string for <paramref name="name"/>
        /// with <paramref name="substituteMethod"/>.
        /// </summary>
        /// 
        /// <typeparam name="U">
        /// The type of the member named <paramref name="name"/> in type <typeparamref name="T"/>.
        /// </typeparam>
        /// 
        /// <param name="name">The name of the member in <typeparamref name="T"/>.</param>
        /// <param name="substituteMethod">The method that will be used for replacement.</param>
        /// 
        /// <returns>The same instance of <see cref="ToStringMethodBuilder{T}"/>.</returns>
        /// 
        /// <exception cref="ArgumentNullException">
        /// If either <paramref name="name"/> or <paramref name="substituteMethod"/> are null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// If <typeparamref name="T"/> doesn't have a member named <paramref name="name"/>
        /// or if that member is not compatible with <typeparamref name="U"/>.
        /// </exception>
        public ToStringMethodBuilder<T> Substitute<U>(string name, ToStringMethod<U> substituteMethod)
        {
            Validate.ArgumentNotNull(name, nameof(name));
            Validate.ArgumentNotNull(substituteMethod, nameof(substituteMethod));

            namedSubstitutes[name] = substituteMethod;

            return this;
        }

        /// <summary>
        /// Builds a new <see cref="ToStringMethod{T}"/>.
        /// </summary>
        /// 
        /// <returns>
        /// A new instance of <see cref="ToStringMethod{T}"/>.
        /// </returns>
        public ToStringMethod<T> Build()
        {
            ValidateSubstitutions();

            List<IValue> values = new List<IValue>();

            if (useProperties)
            {
                values.AddRange(GetPropertiesAsValues());
            }

            Expression<ToStringMethod<T>> toStringExpression = new ToStringExpressionBuilder()
                .WithValues(values.Select(SubstituteIfNeeded))
                .WithAppender(DefaultAppender.AppendExpression)
                .BuildForType<T>();

            return toStringExpression.Compile();
        }

        private void ValidateSubstitutions()
        {
            foreach (KeyValuePair<string, Delegate> namedSubstitute in namedSubstitutes)
            {
                ValidateNamedSubstitute(namedSubstitute.Key, namedSubstitute.Value.GetType());
            }
        }

        private static void ValidateNamedSubstitute(string name, Type toStringMethodType)
        {
            Type substitutionType = toStringMethodType.GenericTypeArguments[0];
            PropertyInfo property = typeof(T)
                .GetProperty(name);

            if (property == null)
            {
                throw new InvalidOperationException(
                    $"Cannot substitute member named \"{name}\" because it doesn't exists in type \"{typeof(T).Name}\""
                );
            }
            else if (!substitutionType.IsAssignableFrom(property.PropertyType))
            {
                throw new InvalidOperationException(
                    $"Cannot substitute member named \"{name}\" because can't be converted from " +
                    $"type \"{property.PropertyType.Name}\" to type \"{substitutionType.Name}\""
                );
            }
        }

        private IValue SubstituteIfNeeded(IValue value)
        {
            if (namedSubstitutes.ContainsKey(value.Name))
            {
                return SubstituteValue.WithDelegate(value, namedSubstitutes[value.Name]);
            }
            else
            {
                return value;
            }
        }

        private static IEnumerable<IValue> GetPropertiesAsValues() => typeof(T)
            .GetProperties()
            .Select(property => new PropertyValue(property));
    }
}
