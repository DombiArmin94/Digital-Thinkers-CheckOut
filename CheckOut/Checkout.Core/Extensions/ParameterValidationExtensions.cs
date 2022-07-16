using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;

namespace Checkout.Core.Extensions
{
    public static class ParameterValidationExtensions
    {
        public static void ThrowIfNull<T>(this T obj, [CallerArgumentExpression("obj")] string? parameterName = null, string? errorMessage = null, ILogger logger = null) where T : class
        {
            if (obj is null)
            {
                Throw<ArgumentNullException>(parameterName, $"{parameterName} cannot be null!", errorMessage, logger);
            }
        }

        public static void ThrowIfLessThanZero(this int source, [CallerArgumentExpression("source")] string parameterName = null, string errorMessage = null, ILogger logger = null)
        {
            if (source < 0)
            {
                Throw<ArgumentOutOfRangeException>(parameterName, $"{parameterName} cannot be less than zero", errorMessage, logger);
            }
        }

        private static void Throw<T>(string parameterName, string defaultMessage, string? errorMessage = null, ILogger logger = null) where T : ArgumentException
        {
            if (string.IsNullOrWhiteSpace(errorMessage))
            {
                if (logger != null)
                {
                    logger.LogError(defaultMessage);
                }
                var exception = Activator.CreateInstance(typeof(T), new object[] { parameterName, defaultMessage }) as T;
                throw exception;
            }
            else
            {
                if (logger != null)
                {
                    logger.LogError(errorMessage);
                }
                var exception = Activator.CreateInstance(typeof(T), new object[] { parameterName, errorMessage }) as T;
                throw exception;
            }
        }
    }
}
