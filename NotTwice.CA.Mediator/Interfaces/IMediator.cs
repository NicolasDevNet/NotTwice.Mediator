using NotTwice.CA.Interfaces.Commands;
using NotTwice.CA.Interfaces.Messages;
using NotTwice.CA.Interfaces.Queries;
using System.Threading.Tasks;

namespace NotTwice.CA.Interfaces
{
    /// <summary>
    /// Interface implemented by the <see cref="Mediator"/> class to perform actions
    /// between different application layers.
    /// </summary>
    public interface IMediator
    {
        /// <summary>
        /// Method for checking whether a command can be executed and then executing it if it is.
        /// </summary>
        /// <typeparam name="T">Type of command structure</typeparam>
        /// <returns>Operation status</returns>
        bool TryExecute<T>() where T : struct, ICommand<T>;

        /// <summary>
        /// Method for checking whether an asynchronous command can be executed, and then executing it if it is.
        /// </summary>
        /// <typeparam name="T">Type of command structure</typeparam>
        /// <returns>Operation status</returns>
        Task<bool> TryExecuteAsync<T>() where T : struct, ICommandAsync<T>;

        /// <summary>
        /// Method for checking whether a request can be executed and then executing it if it is.
        /// </summary>
        /// <typeparam name="TIn">Query input structure type</typeparam>
        /// <typeparam name="TOut">Type of output expected in response to sending this request</typeparam>
        /// <param name="in">Request input structure</param>
        /// <param name="out">Object resulting from sending the request</param>
        /// <returns>Operation status</returns>
        bool TryInteract<TIn, TOut>(TIn @in, out TOut @out) where TIn : struct, IQuery<TOut>;

        /// <summary>
        /// Method for checking whether an asynchronous request can be executed, and then executing it if it is.
        /// </summary>
        /// <typeparam name="TIn">Query input structure type</typeparam>
        /// <typeparam name="TOut">Type of output expected in response to sending this request</typeparam>
        /// <param name="in">Request input structure</param>
        /// <returns>Status of the operation and object resulting from sending the request</returns>
        Task<(bool success, TOut result)> TryInteractAsync<TIn, TOut>(TIn @in) where TIn : struct, IQuery<TOut>;

        /// <summary>
        /// Method used to check whether a message can be sent and to send it if so.
        /// </summary>
        /// <typeparam name="T">Type of message input structure</typeparam>
        /// <param name="in">Message input structure</param>
        /// <returns>Operation status</returns>
        bool TrySend<T>(T @in) where T : struct, IMessage;

        /// <summary>
        /// Method used to check whether an asynchronous message can be sent, and to send it if so.
        /// </summary>
        /// <typeparam name="T">Type of message input structure</typeparam>
        /// <param name="in">Message input structure</param>
        /// <returns>Operation status</returns>
        Task<bool> TrySendAsync<T>(T @in) where T : struct, IMessage;

        /// <summary>
        /// Method for performing the reverse of a command execution.
        /// </summary>
        /// <typeparam name="T">Type of command input structure</typeparam>
        /// <returns>Operation status</returns>
        bool TryUndo<T>() where T : struct, ICommand<T>;

        /// <summary>
        /// Method for performing the reverse of an async command execution.
        /// </summary>
        /// <typeparam name="T">Type of command input structure</typeparam>
        /// <returns>Operation status</returns>
        Task<bool> TryUndoAsync<T>() where T : struct, ICommandAsync<T>;
    }
}