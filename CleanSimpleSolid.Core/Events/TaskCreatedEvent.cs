using CleanDdd.Common.Model.Identity;
using CleanSimpleSolid.Core.Model.Tasks;

namespace CleanSimpleSolid.Core.Events
{
    public class TaskCreatedEvent: IAuditableEvent<CssTask, LongIdentity, long>
    {
        public TaskCreatedEvent(long user, CssTask model)
        {
            UserId = user;
            Model = model;
        }
        
        public Events Event => Events.TaskCreated;
        public long UserId { get; }
        public CssTask Model { get; }
        
        public string Description => $"Created Task {Model.Name}";
    }
}