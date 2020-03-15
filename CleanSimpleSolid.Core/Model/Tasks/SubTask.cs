using CleanConnect.Common.Model.Errors;
using CleanDdd.Common.Model.Identity;

namespace CleanSimpleSolid.Core.Model.Tasks
{
    public class SubTask: Base<LongIdentity, long>, IValidator
    {
        private readonly string _name;

        public SubTask(string name)
        {
            _name = name;
            Errors = new Validations();
        }

        public bool IsValid()
        {
            return Errors.Count == 0;
        }

        public Validations Errors { get; }
    }
}