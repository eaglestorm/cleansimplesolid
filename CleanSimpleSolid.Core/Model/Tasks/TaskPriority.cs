namespace CleanSimpleSolid.Core.Model.Tasks
{
    /// <summary>
    /// How important is the task.
    /// </summary>
    public enum TaskPriority
    {
        /// <summary>
        /// not very
        /// </summary>
        Low,
        
        /// <summary>
        /// no more or less important that other tasks.
        /// </summary>
        Normal,
        
        /// <summary>
        /// More important than other tasks.
        /// </summary>
        High
    }
}