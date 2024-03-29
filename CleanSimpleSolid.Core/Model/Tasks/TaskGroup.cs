using System.Collections.Generic;
using CleanConnect.Common.Model;
using CleanConnect.Common.Model.Errors;
using CleanDdd.Common.Model.Identity;

namespace CleanSimpleSolid.Core.Model.Tasks
{
    /// <summary>
    /// A group of tasks, not really sure how this should work.
    /// </summary>
    public class TaskGroup: Base<LongIdentity, long>, IValidator
    {
        private const int NameMaxLength = 50;

        /// <summary>
        /// TODO: this needs to be a paginated collection.
        /// </summary>
        private List<CssTask> _tasks;
        
        public string Name { get; private set; }

        public IEnumerable<CssTask> Tasks => _tasks;

        public TaskGroup(string name)
        {
            Errors = new Validations();
            _tasks = new List<CssTask>();
            SetName(name);
        }

        public void SetName(string name)
        {
            var start = Errors.Count;
            if (string.IsNullOrEmpty(name))
            {
                Errors.AddError(ErrorCode.ValueRequired, "A name is required");
                return;
            }
            if (!RegexConstants.NameRegex.IsMatch(name) || string.IsNullOrEmpty(name))
            {
                Errors.AddError(ErrorCode.InvalidName, "The name is not valid");
            }

            if (name.Length > NameMaxLength)
            {
                Errors.AddError(ErrorCode.InvalidLength, "Name too long.");
            }

            if (start == Errors.Count)
            {
                Name = name;
            }
        }

        public void AddTask(string name)
        {
            _tasks.Add(new CssTask(name));
        }

        public bool IsValid()
        {
            return Errors.Count == 0;
        }

        public Validations Errors { get; }
    }
}