/*
 * Copyright (c) 2020 Vraiment
 *
 * This source code file is subject to the terms of the MIT license.
 * If a copy of the MIT license was not distributed with this file,
 * You can obtain one at https://opensource.org/licenses/MIT.
 */
using System;
using System.Collections;
using System.Reflection;

using RealStringBuilder = System.Text.StringBuilder;

namespace NUtils.MethodBuilders.ToString
{
    internal static class Reflections
    {
        public static class Object
        {
            public static readonly MethodInfo ToStringMethod
                = typeof(object).GetMethod(nameof(object.ToString));
        }

        public static class Enumerator
        {
            public static readonly MethodInfo MoveNextMethod = typeof(IEnumerator)
                .GetMethod(nameof(IEnumerator.MoveNext));
        }

        public static class String
        {
            public static readonly MethodInfo ReplaceMethod = typeof(string)
                .GetMethod(nameof(string.Replace), new Type[] { typeof(string), typeof(string) });
        }

        public static class StringBuilder
        {
            public static readonly ConstructorInfo EmptyConstructor
                = typeof(RealStringBuilder).GetConstructor(Array.Empty<Type>());

            public static readonly MethodInfo AppendBoolMethod
                = GetAppendMethod<bool>();

            public static readonly MethodInfo AppendByteMethod
                = GetAppendMethod<byte>();

            public static readonly MethodInfo AppendShortMethod
                = GetAppendMethod<short>();

            public static readonly MethodInfo AppendUnsignedShortMethod
                = GetAppendMethod<ushort>();

            public static readonly MethodInfo AppendIntMethod
                = GetAppendMethod<int>();

            public static readonly MethodInfo AppendUnsignedIntMethod
                = GetAppendMethod<uint>();

            public static readonly MethodInfo AppendLongMethod
                = GetAppendMethod<long>();

            public static readonly MethodInfo AppendUnsignedLongMethod
                = GetAppendMethod<ulong>();

            public static readonly MethodInfo AppendFloatMethod
                = GetAppendMethod<float>();

            public static readonly MethodInfo AppendDoubleMethod
                = GetAppendMethod<double>();

            public static readonly MethodInfo AppendCharMethod
                = GetAppendMethod<char>();

            public static readonly MethodInfo AppendStringMethod
                = GetAppendMethod<string>();

            public static readonly MethodInfo AppendObjectMethod
                = GetAppendMethod<object>();

            private static MethodInfo GetAppendMethod<T>() => typeof(RealStringBuilder)
                .GetMethod(nameof(RealStringBuilder.Append), new Type[] { typeof(T) });
        }
    }
}
