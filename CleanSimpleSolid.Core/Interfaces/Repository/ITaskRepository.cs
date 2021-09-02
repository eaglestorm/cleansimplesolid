using System.Collections.Generic;
using System.Threading.Tasks;
using CleanSimpleSolid.Core.Model.Tasks;

namespace CleanSimpleSolid.Core.Interfaces.Repository
{
    /// <summary>
    /// Interface for the repository is in the core so the core has no (or minimal) dependencies.
    /// </summary>
    public interface ITaskRepository
    {
        Task<CssTask> Get(long id, long user);
        
        Task<IList<CssTask>> Get(long user, int index, int size);

        Task<CssTask> Save(CssTask cssTask);
    }
}