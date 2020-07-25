using CleanConnect.Common.Model.Errors;

namespace CleanConnect.Common.Model.Errors
{
    /// <summary>
    /// Indicates that the implementer needs to be checked for a valid state.
    /// </summary>
    /// <remarks>
    /// If an invalid object shouldn't be saved then this can also be checked
    /// in the repository before saving.
    /// </remarks>
    public interface IValidator
    {
        /// <summary>
        /// If in a valid state will return true.
        /// </summary>
        /// <returns></returns>
        bool IsValid();
        
        /// <summary>
        /// A list of errors that are causing the implementor to not be in a valid state.
        /// </summary>
        Validations Errors { get; }
    }
}