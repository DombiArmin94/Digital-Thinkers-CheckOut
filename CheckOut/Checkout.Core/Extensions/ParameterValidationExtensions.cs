using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Checkout.Core.Extensions
{
    public static class ParameterValidationExtensions
    {
        public static void ThrowIfNull<T>(this T obj, [CallerArgumentExpression("obj")] string? parameterName = null, string? errorMessage = null) where T : class
        {
            if (obj is null)
            {
                Throw<ArgumentNullException>(parameterName, $"{parameterName} cannot be null!", errorMessage);
            }
        }

        private static void Throw<T>(string parameterName, string defaultMessage, string? errorMessage = null) where T : ArgumentException
        {
            if (string.IsNullOrWhiteSpace(errorMessage))
            {
                var exception = Activator.CreateInstance(typeof(T), new object[] { parameterName, defaultMessage }) as T;
                throw exception;
            }
            else
            {
                var exception = Activator.CreateInstance(typeof(T), new object[] { parameterName, errorMessage }) as T;
                throw exception;
            }
        }
    }
}
