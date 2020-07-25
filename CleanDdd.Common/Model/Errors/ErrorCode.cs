
namespace CleanConnect.Common.Model.Errors
{
    /// <summary>
    /// List of error codes for excpetions.
    /// </summary>
    /// <remarks>
    /// Contains two different classes of errors.
    /// https://martinfowler.com/articles/replaceThrowWithNotification.html
    ///
    /// </remarks>
    public sealed class ErrorCode
    {        
        private readonly ErrorGroup _group;
        private int _number;
        

        //Errors not returned to the RP.
        #region Authorisation Server Errors

        public static readonly  ErrorCode InvalidMethodCall = new ErrorCode(ErrorGroup.General, 1, "This method call is not valid.");
        
        /// <summary>
        /// A users password was attempted to be changed but the new password did not match the repeated password field.
        /// </summary>
        public static readonly ErrorCode PasswordsDifferent = new ErrorCode(ErrorGroup.Validation, 201, "The supplied passwords did not match.");
        
        /// <summary>
        /// A user tried to change their password but the new password did not meet the password rules. 
        /// </summary>
        public static readonly ErrorCode PasswordInvalid = new ErrorCode(ErrorGroup.Validation, 202, "The supplied password does not meet password rules.");
        
        public static readonly ErrorCode InvalidUsername = new ErrorCode(ErrorGroup.Validation, 203, "The supplied username is not valid.");
        
        public static readonly ErrorCode InvalidName = new ErrorCode(ErrorGroup.Validation, 204, "A string contains invalid data.");
        
        public static readonly ErrorCode InvalidDate = new ErrorCode(ErrorGroup.Validation, 204, "A date is not valid.");
        
        public static readonly ErrorCode InvalidEmail = new ErrorCode(ErrorGroup.Validation, 204, "The email address is not valid.");
        
        public static readonly ErrorCode InvalidPhone = new ErrorCode(ErrorGroup.Validation, 204, "The phone number is not valid.");
        
        public static readonly ErrorCode ValueRequired = new ErrorCode(ErrorGroup.Validation, 205, "The phone number is not valid.");
        
        public static readonly ErrorCode UserLocked = new ErrorCode(ErrorGroup.Validation, 206,"The user is currently locked.");
        
        public static readonly ErrorCode IdentityAccess = new ErrorCode(ErrorGroup.General, 207, "The identity has already been set.");
        
        public static readonly ErrorCode InvalidLogin = new ErrorCode(ErrorGroup.General, 208,"The username or password you entered is not correct.");

        public static readonly ErrorCode SessionExpired = new ErrorCode(ErrorGroup.General, 209,"Your session has expired, please login again to continue.");
        
        public static readonly ErrorCode InvalidDevice = new ErrorCode(ErrorGroup.General, 210, "The device has been disabled.");
        
        public static readonly ErrorCode Unauthorized = new ErrorCode(ErrorGroup.General, 211,"You are not authorized to access this resource.");
        
        public static readonly ErrorCode Unexpected = new ErrorCode(ErrorGroup.General, 212, "An unexpected error has occured.");
        
        public static readonly ErrorCode InvalidLength = new ErrorCode(ErrorGroup.Validation, 213, "It's too long.");
        
        public static readonly ErrorCode InvalidDueDate = new ErrorCode(ErrorGroup.Validation, 214, "The due date must be in the future.");
        
        public static readonly ErrorCode InvalidScheduledDate = new ErrorCode(ErrorGroup.Validation, 215, "The scheduled date must be in the future.");
        #endregion

        
        /// <summary>
        /// Errors that are not required by the open id spec and hence not returned to the RP, i.e. password validation errors.
        /// </summary>
        /// <param name="group"></param>
        /// <param name="number"></param>
        /// <param name="message"></param>
        public ErrorCode(
            ErrorGroup @group,
            int number,
            string message)
        {
            Message = message;
            _group = group;
            _number = number;
        }

        public string Code => $"{_group.Code}{_number | 0000} ";

        /// <summary>
        /// Readable message for the error.
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// External description for the error as per the spec.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Code;
        }

        /// <summary>
        /// Internal error description for the logs, etc.
        /// </summary>
        /// <returns></returns>
        public string GetInternalDescription()
        {
            return $"Error Code: {Code},  Description: {Message}";
        }
    }
}