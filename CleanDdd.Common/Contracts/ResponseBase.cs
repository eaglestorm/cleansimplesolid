using System.Collections.Generic;
using CleanConnect.Common.Model.Errors;

namespace CleanConnect.Common.Contracts
{
    public abstract class ResponseBase
    {
        public ResponseBase(bool success)
        {
            Success = success;
        }
        
        public ResponseBase(bool success, Validations errors)
        {
            Success = success;
            Errors = errors;
        }
       
        public bool Success { get; }
        public Validations Errors { get; }
    }
}