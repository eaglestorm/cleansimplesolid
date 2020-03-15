namespace CleanConnect.Common.Contracts
{
    /// <summary>
    /// A use case request that has no response.
    /// </summary>
    public interface IMessage
    {
        
    }

    /// <summary>
    /// A use case request that has a response.
    /// </summary>
    /// <typeparam name="TResponse"></typeparam>
    public interface IMessage<out TResponse>
    {
        
    }
}