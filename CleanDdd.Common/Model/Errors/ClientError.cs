using CleanConnect.Common.Model.Errors;

namespace CleanDdd.Common.Model.Errors
{
    /// <summary>
    /// An error returned to the client with a 400 response code. 
    /// </summary>
    public class ClientError
    {       

        /// <summary>
        /// Create a client error
        /// </summary>
        /// <param name="errorCode"></param>
        /// <param name="message"></param>
        public ClientError(ErrorCode errorCode, string message)
        {
            ErrorCode = errorCode;
            Message = message;
        }

        public ClientError(ErrorCode errorCode, string message, string property)
        :this(errorCode, message)
        {
            Property = property;
        }
        
        /// <summary>
        /// The error code for this error.
        /// Should be used by clients when deciding what to do with the error and displaying a message for the error.
        /// </summary>
        public ErrorCode ErrorCode { get; }
        
        /// <summary>
        /// A readable message for the error.
        /// Mostly for developers and generally shouldn't be displayed to the user.
        /// </summary>
        public string Message { get; }
        
        /// <summary>
        /// The field or property that the error relates to. 
        /// </summary>
        /// <remarks>
        /// This should be the same as the name of the property on the Dto.
        /// </remarks>
        public string Property { get; }
    }
}