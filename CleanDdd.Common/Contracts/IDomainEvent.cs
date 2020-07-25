using MediatR;

namespace CleanConnect.Common.Contracts
{
    /// <summary>
    /// Interfaces that enables auditing of all domain events.
    /// </summary>
    public interface IDomainEvent: INotification
    {
        
    }
}