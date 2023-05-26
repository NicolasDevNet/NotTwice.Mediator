using NotTwice.CA.Constants;
using NotTwice.CA.Enums;

namespace NotTwice.CA.Helpers
{
    internal static class ErrorTypeHelper
    {
        public static string GetErrorMessage(ErrorType errorType)
        {
            switch (errorType) 
            {
                case ErrorType.CommandFailed:
                    return MediatorConstants.MsgErr01;
                case ErrorType.FailedToRetrieveInstance:
                    return MediatorConstants.MsgErr02;
                case ErrorType.FailedToInitMediator:
                    return MediatorConstants.MsgErr03;
                case ErrorType.MissingHandler:
                    return MediatorConstants.MsgErr04;
                case ErrorType.QueryFailed:
                    return MediatorConstants.MsgErr05;
                case ErrorType.MessageFailed:
                    return MediatorConstants.MsgErr06;
                default:
                    return string.Empty;
            }
        }
    }
}
