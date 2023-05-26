using NotTwice.CA.Enums;
using NotTwice.CA.Helpers;
using System;

namespace NotTwice.CA.Exceptions
{
    internal class MediatorException<T> : Exception
    {
        public MediatorException(ErrorType errorType, MediationType mediationType, Exception innerException = null)
            : base(LogHelper.BuildMediatorLog<T>(errorType, mediationType), innerException)
        {
        }
    }

    internal class MediatorException : Exception
    {
        public MediatorException(ErrorType errorType, MediationType mediationType, Exception innerException = null)
            : base(LogHelper.BuildMediatorLog(errorType, mediationType), innerException)
        {
        }
    }
}
