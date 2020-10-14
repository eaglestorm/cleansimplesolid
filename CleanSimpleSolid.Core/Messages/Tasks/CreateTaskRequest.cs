using System;
using CleanConnect.Common.Contracts;

namespace ServiceBase.Core.Messages
{
    /// <summary>
    /// Create task request.  This is basically a Property bag that enforces the separation between the
    /// controllers and the domain logic.  Defines the data required for the use case and allows testing of the use case.
    /// </summary>
    /// <remarks>
    /// Note that this message is not secured.  The user becomes the owner.
    /// </remarks>
    public class CreateTaskRequest: IMessage<CreateTaskResponse>
    {
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public DateTimeOffset? DueDate { get; set; }
        
        public DateTimeOffset? ScheduledDate { get; set; }
        
        public string Subject { get; set; }
        
    }
}