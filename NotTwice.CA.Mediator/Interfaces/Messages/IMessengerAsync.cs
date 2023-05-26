using System.Threading;
using System.Threading.Tasks;

namespace NotTwice.CA.Interfaces.Messages
{
    /// <summary>
    /// Interface contract to implement for sending async messages
    /// </summary>
    /// <typeparam name="T">Type of structure designated as a message</typeparam>
    public interface IMessengerAsync<T> where T : struct, IMessage
    {
        /// <summary>
        /// Method for checking whether an async message can be sent and sending it if so
        /// </summary>
        /// <param name="message">The type of structure to be sent</param>
        Task SendAsync(T message, CancellationToken cancellationToken);

        /// <summary>
        /// Method to check if a message can be sent
        /// </summary>
        /// <param name="message">The type of structure to be sent</param>
        /// <returns>The status of the operation</returns>
        bool CanSend(T message);
    }
}
