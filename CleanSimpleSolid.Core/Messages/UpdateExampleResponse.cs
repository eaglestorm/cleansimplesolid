using CleanConnect.Common.Contracts;
using CleanConnect.Common.Model.Errors;

namespace ServiceBase.Core.Messages
{
    public class UpdateExampleResponse: ResponseBase
    {
        public UpdateExampleResponse(bool success)
        :base(success)
        {
            
        }

        public UpdateExampleResponse(bool success, Validations errors)
        :base(success,errors)
        {
            
        }
    }
}