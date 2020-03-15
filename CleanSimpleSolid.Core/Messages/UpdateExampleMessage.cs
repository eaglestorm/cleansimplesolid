using System.Threading.Tasks;
using CleanConnect.Common.Contracts;

namespace CleanSimpleSolid.Core.Messages
{
    /// <summary>
    /// Domain event that indicates the example model has been updated.
    /// </summary>
    public class UpdateExampleMessage: IDomainEvent
    {
        /// <summary>
        /// What ever info we need in the message.
        /// </summary>
        public Task Model { get; set; }
    }
}