namespace CleanConnect.Common.Contracts
{
    /// <summary>
    /// Indicates the access to the model must be validated for the given user.
    /// </summary>
    public interface ISecuredMessage: IMessage
    {
        /// <summary>
        /// The open id connect subject identifier.
        /// This is what we know about the user from the JWT identity token. 
        /// </summary>
        string Subject { get;  }
    }
    
    public interface ISecuredMessage<out TResponse>: IMessage<TResponse>
    {
        /// <summary>
        /// The open id connect subject identifier.
        /// This is what we know about the user from the JWT identity token. 
        /// </summary>
        string Subject { get;  }
    }
}