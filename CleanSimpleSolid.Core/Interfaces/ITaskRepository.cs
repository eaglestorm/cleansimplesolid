using System.Collections.Generic;
using System.Threading.Tasks;
using CleanSimpleSolid.Core.Model.Tasks;

namespace CleanSimpleSolid.Core.Interfaces
{
    /// <summary>
    /// Interface for the repository is in the core so the core has no (or minimal) dependencies.
    /// </summary>
    public interface ITaskRepository
    {
        Task<Todo> Get(long id);
        
        Task<IList<Todo>> Get(int index, int size);

        Task<Todo> Save(Todo exampleModel);
    }
}