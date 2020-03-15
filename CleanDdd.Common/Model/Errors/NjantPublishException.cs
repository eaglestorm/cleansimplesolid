using System;
using CleanConnect.Common.Model.Errors;

namespace CleanDdd.Common.Model.Errors
{
    /// <summary>
    /// An exception thrown by the application to indicate that the current request should terminate
    /// </summary>
    /// <remarks>
    /// Should not be used for validation.
    /// </remarks>
    public class NjantPublishException: Exception
    {
        public NjantPublishException(
            ErrorCode errorCode,
            string message)
        :base(message)
        {
            ErrorCode = errorCode;
        }
        
        public ErrorCode ErrorCode { get; }
    }
}