using NotTwice.CA.Enums;
using NotTwice.CA.Helpers;
using Serilog;
using System;

namespace NotTwice.CA.Extentions
{
    internal static class LoggerExtentions
    {
        public static void LogMediatorErrorAsInformation<T>(this ILogger logger, ErrorType errorType, MediationType mediationType)
            => logger.Information(LogHelper.BuildMediatorLog<T>(errorType, mediationType));

        public static void LogMediatorErrorAsInformation(this ILogger logger, ErrorType errorType, MediationType mediationType)
            => logger.Information(LogHelper.BuildMediatorLog(errorType, mediationType));

        public static void LogMediatorErrorAsInformation<T>(this ILogger logger, ErrorType errorType, MediationType mediationType, Exception exception)
            => logger.Information(exception.Message + " | " + LogHelper.BuildMediatorLog<T>(errorType, mediationType));

        public static void LogMediatorErrorAsInformation(this ILogger logger, ErrorType errorType, MediationType mediationType, Exception exception)
            => logger.Information(exception.Message + " | " + LogHelper.BuildMediatorLog(errorType, mediationType));
    }
}
