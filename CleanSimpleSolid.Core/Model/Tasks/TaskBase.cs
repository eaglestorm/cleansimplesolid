using System;
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
    public abstract class TaskBase : Base<LongIdentity, long>, IValidator
    {
        // constants.
        private const int NameMaxLength = 50;
        private const int DescriptionMaxLength = 1000;

        /// <summary>
        /// The name of the task.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// an optional description of the task.
        /// </summary>
        public string Description { get; private set; }
        
        public TaskPriority Priority { get; private set; }

        /// <summary>
        /// An optional due date.
        /// </summary>
        public DateTimeOffset? DueDate { get; private set; }

        /// <summary>
        /// The date the user has scheduled to do this task.
        /// </summary>
        public DateTimeOffset? ScheduleDate { get; private set; }

        /// <summary>
        /// Constructor with minimum required data.
        /// Would usually be called when creating a new task.
        /// </summary>
        /// <param name="name"></param>
        public TaskBase(string name)
        {
            Errors = new Validations();
            SetName(name);
            Priority = TaskPriority.Normal;
        }

        /// <summary>
        /// Constructor that sets all properties.
        /// Used when creating an object after loading from database.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="dueDate"></param>
        /// <param name="scheduledDate"></param>
        public TaskBase(LongIdentity identity, string name, string description, DateTimeOffset dueDate,
            DateTimeOffset scheduledDate, DateTimeOffset createdDate, DateTimeOffset modifiedDate)
        :base(identity,createdDate,modifiedDate)
        {
            Errors = new Validations();
            Name = name;
            Description = description;
            DueDate = dueDate;
            ScheduleDate = scheduledDate;
        }

        /// <summary>
        /// Set the name first checking that the value is valid.
        /// </summary>
        /// <param name="name"></param>
        public void SetName(string name)
        {
            var start = Errors.Count;
            if (!RegexConstants.NameRegex.IsMatch(name) || string.IsNullOrEmpty(name))
            {
                Errors.AddError(ErrorCode.InvalidName, "The name is not valid");
            }

            if (name.Length > NameMaxLength)
            {
                Errors.AddError(ErrorCode.InvalidLength, "Name too long.");
            }

            //If there are no errors set the value.  If there are errors these are communicated to the client to show the user.
            //If you wanted to save an object in an invalid state you would still set this and also save the state
            // and valid the object after loading from the database.
            if (start == Errors.Count)
            {
                Name = name;
            }
        }

        /// <summary>
        /// Set the description ensuring the value is valid.
        /// </summary>
        /// <param name="desc"></param>
        public void SetDescription(string desc)
        {
            var start = Errors.Count;
            if (!RegexConstants.DescriptionRegex.IsMatch(desc))
            {
                Errors.AddError(ErrorCode.InvalidName, "The description is not valid");
            }

            if (desc.Length > DescriptionMaxLength)
            {
                Errors.AddError(ErrorCode.InvalidLength, "Description too long.");
            }

            if (start == Errors.Count)
            {
                Description = desc;
            }
        }

        public void SetPriority(TaskPriority taskPriority)
        {
            Priority = taskPriority;
        }

        /// <summary>
        /// Set the due date and also the scheduled date if it needs to be set.
        /// </summary>
        /// <param name="dueDate"></param>
        public virtual void SetDueDate(DateTimeOffset dueDate)
        {
            var start = Errors.Count;
            if (DateTimeOffset.Now > dueDate)
            {
                Errors.AddError(ErrorCode.InvalidDueDate, "The due date is not valid.");
            }

            if (start == Errors.Count)
            {
                DueDate = dueDate;
                if (!ScheduleDate.HasValue || ScheduleDate > DueDate)
                {
                    ScheduleDate = DueDate;
                }
            }
        }

        /// <summary>
        /// Set the schedule date and ensure it is valid given the current due date.
        /// </summary>
        /// <param name="scheduledDate"></param>
        public void SetScheduledDate(DateTimeOffset scheduledDate)
        {
            var start = Errors.Count;
            if (DateTimeOffset.Now > scheduledDate || scheduledDate > DueDate)
            {
                Errors.AddError(ErrorCode.InvalidScheduledDate, "The due date is not valid.");
            }

            if (start == Errors.Count)
            {
                ScheduleDate = scheduledDate;
            }
        }

        public bool IsValid()
        {
            return Errors.Count == 0;
        }

        public Validations Errors { get; }
    }
}