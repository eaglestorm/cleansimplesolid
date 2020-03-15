using System.Collections.Generic;
using System.Linq;
using CleanDdd.Common.Model.Errors;

namespace CleanConnect.Common.Model.Errors
{
    /// <summary>
    /// A collection of validation errors.
    /// </summary>
    public class Validations
    {
        private readonly IList<ClientError> _clientErrors = new List<ClientError>();

        /// <summary>
        /// Add a new error to the collection.
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        public void AddError(ErrorCode code, string message)
        {
            _clientErrors.Add(new ClientError(code,message));
        }
        
        /// <summary>
        /// Add a new error related to the given property.
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        /// <param name="property"></param>
        public void AddError(ErrorCode code, string message, string property)
        {
            _clientErrors.Add(new ClientError(code,message,property));
        }

        /// <summary>
        /// Read only list of the errors.
        /// </summary>
        public IEnumerable<ClientError> Errors => _clientErrors;

        /// <summary>
        /// The number of errors.
        /// </summary>
        public int Count => _clientErrors.Count;

        /// <summary>
        /// Add the list of errors to this validation object.
        /// Typically used when merging the parent and child object errors.
        /// </summary>
        /// <param name="Errors"></param>
        public void Add(IEnumerable<ClientError> Errors)
        {
            foreach (var error  in Errors)
            {
                //shouldn't be a lot of errors so performance shouldn't be bad.
                if (!Contains(error.ErrorCode))
                {
                    _clientErrors.Add(error);
                }
            }
        }

        public bool Contains(ErrorCode errorCode)
        {
            return _clientErrors.Any(x => x.ErrorCode == errorCode);
        }

        public ClientError FirstOrDefault()
        {
            return _clientErrors.FirstOrDefault();
        }
    }
}