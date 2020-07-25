using System;
using System.Collections.Generic;
using System.Linq;
using CleanConnect.Common.Model;
using CleanConnect.Common.Model.Errors;
using CleanDdd.Common.Model.Identity;

namespace CleanSimpleSolid.Core.Model.Tasks
{
    /// <summary>
    /// A user task.
    /// This object is responsible for ensuring it's state is always valid and if it's not valid
    /// communicating it's invalid state.  This is the reason for the isvalid method.
    /// Whether you then save an object in an invalid state depends on the business rules you are implementing.
    /// </summary>
    /// <remarks>
    /// Named changed from task to avoid name conflicts with async threading tasks.
    /// </remarks>
    public class CssTask : TaskBase
    {
        private List<SubTask> _subTasks = new List<SubTask>();
        
        public IEnumerable<SubTask> Children => _subTasks;

        /// <summary>
        /// Constructor with minimum required data.
        /// Would usually be called when creating a new task.
        /// </summary>
        /// <param name="name"></param>
        public CssTask(string name)
        :base(name)
        {
        
        }

        /// <summary>
        /// Constructor that sets all properties.
        /// Used when creating an object after loading from database.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="dueDate"></param>
        /// <param name="scheduledDate"></param>
        public CssTask(LongIdentity identity, string name, string description, DateTimeOffset dueDate,
            DateTimeOffset scheduledDate, DateTimeOffset createdDate, DateTimeOffset modifiedDate, List<SubTask> children)
        :base(identity,name,description,dueDate,scheduledDate,createdDate,modifiedDate)
        {
            _subTasks = children;
        }

        /// <summary>
        /// Set the due date of the task validating against the due date of any subtasks.
        /// </summary>
        /// <param name="dueDate"></param>
        public override void SetDueDate(DateTimeOffset dueDate)
        {
            if (Children.Any(x=> x.DueDate > dueDate))
            {
                Errors.AddError(ErrorCode.InvalidDate, "The due date can't be less than the due date of the children.");
            }
            else
            {
                base.SetDueDate(dueDate);
            }
        }
    }
}