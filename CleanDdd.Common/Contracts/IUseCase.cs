using System.Threading.Tasks;

namespace CleanConnect.Common.Contracts
{
    /// <summary>
    /// Defines a use case with a request and response.
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public interface IUseCase< in TRequest, TResponse>
     where TRequest : IMessage<TResponse>
    {
        /// <summary>
        /// Process the use case and return the response.
        /// </summary>
        /// <remarks>
        /// Currently Task<T> is not co-variant so we can't make this async.
        /// see https://stackoverflow.com/questions/30996986/why-is-taskt-not-co-variant 
        /// </remarks>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<TResponse> Process(TRequest request);
    }
    
    /// <summary>
    /// Defines a use case with a request but no response.
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    public interface IUseCase< in TRequest>
        where TRequest : IMessage
    {
        /// <summary>
        /// Process the request.
        /// </summary>
        /// <param name="request"></param>
        Task Process(TRequest request);
    }
}