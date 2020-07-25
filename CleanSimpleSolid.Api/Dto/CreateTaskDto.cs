using System.ComponentModel.DataAnnotations;

namespace ServiceBase.Dto
{
    /// <summary>
    /// Request to create a task.
    /// </summary>
    public class CreateTaskDto
    {
        [StringLength(100)]
        [RegularExpression(RegexConstants.NameRegex)]
        public string Name { get; set; }
        
        //TODO: valid tags.
        public string Description { get; set; }
        
        
    }
}