using CleanConnect.Common.Contracts;
using CleanConnect.Common.Model.Errors;

namespace ServiceBase.Core.Messages
{
    public class UpdateTaskResponse: ResponseBase
    {
        public UpdateTaskResponse(bool success)
        :base(success)
        {
            
        }

        public UpdateTaskResponse(bool success, Validations errors)
        :base(success,errors)
        {
            
        }
    }
}