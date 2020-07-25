using CleanConnect.Common.Contracts;
using CleanConnect.Common.Model.Errors;

namespace ServiceBase.Core.Messages
{
    public class CreateTaskResponse: ResponseBase
    {
        public CreateTaskResponse(bool success) : base(success)
        {
        }

        public CreateTaskResponse(bool success, Validations errors) : base(success, errors)
        {
        }
    }
}