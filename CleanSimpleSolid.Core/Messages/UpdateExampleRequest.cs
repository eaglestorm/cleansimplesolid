using CleanConnect.Common.Contracts;

namespace ServiceBase.Core.Messages
{
    public class UpdateExampleRequest: IMessage<UpdateExampleResponse>
    {
        public long Id { get; }
        public string Property1 { get; }

        public UpdateExampleRequest(long id, string property1)
        {
            Id = id;
            Property1 = property1;
        }
    }
}