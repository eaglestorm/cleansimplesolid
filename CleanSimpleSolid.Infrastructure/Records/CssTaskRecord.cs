using System;

namespace ServiceBase.Infrastructure.Records
{
    public class CssTaskRecord
    {
        public long Id { get; set; }
        
        /// <summary>
        /// When the model was created.
        /// </summary>
        /// <remarks>
        /// If you don't care when it was created or modified then it's likely not an entity and just a value object.
        /// </remarks>
        public DateTimeOffset CreatedDate { get; }
        
        /// <summary>
        /// The last time the model was modified.
        /// </summary>
        public DateTimeOffset ModifiedDate { get; }
        
        /// <summary>
        /// The name of the task.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// an optional description of the task.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// An optional due date.
        /// </summary>
        public DateTimeOffset? DueDate { get; set; }

        /// <summary>
        /// The date the user has scheduled to do this task.
        /// </summary>
        public DateTimeOffset? ScheduleDate { get; set; }
    }
}