namespace ServiceBase.Dto
{
    public class TaskSearchDto
    {
        /// <summary>
        /// The start index for the results to return.
        /// </summary>
        public int Index { get; set; }
        
        /// <summary>
        /// The number of results to return.
        /// </summary>
        public int Size { get; set; }
    }
}