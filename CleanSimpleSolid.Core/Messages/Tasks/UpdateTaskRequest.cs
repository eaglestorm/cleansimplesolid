using System;
using CleanConnect.Common.Contracts;

namespace ServiceBase.Core.Messages
{
    public class UpdateTaskRequest: ISecuredMessage<UpdateTaskResponse>
    {
        public long Id { get; }

        public string Name { get; set; }
        
        public long CreatedBy { get; set; }
        
        public string Description { get; set; }
        
        public DateTimeOffset DueDate { get; set; }
        
        public DateTimeOffset ScheduledDate { get; set; }

        public UpdateTaskRequest(long id, string subject)
        {
            Id = id;
            Subject = subject;
        }

        public string Subject { get; }
    }
}