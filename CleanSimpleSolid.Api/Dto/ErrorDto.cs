namespace ServiceBase.Dto
{
    public class ErrorDto
    {
        /// <summary>
        /// The code for the error.
        /// </summary>
        public string Code { get; set; }
        
        /// <summary>
        /// Description of the error for client API Developers.
        /// </summary>
        public string Description { get; set; }
    }
}