using System.Threading;
using System.Threading.Tasks;

namespace NotTwice.CA.Interfaces.Commands
{
    /// <summary>
    /// Interface designating a structure as an executable async command.
    /// This same command can be used using Mediator.
    /// </summary>
    /// <typeparam name="T">The type of command to fill in</typeparam>
    public interface ICommandAsync<T> where T : struct, ICommandAsync<T>
    {
        /// <summary>
        /// Method for executing a command powered by the implementation
        /// </summary>
        /// <param name="cancellationToken">The cancellation token powered by Mediator</param>
        /// <returns>Returns the asynchronous task</returns>
        Task ExecuteAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Method for checking whether the command can be executed.
        /// This method is automatically called by Mediator when the order is executed.
        /// </summary>
        /// <returns>Returns check status</returns>
        bool CanExecute();

        /// <summary>
        /// Method for performing the reverse operation of the implemented async command.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token powered by Mediator</param>
        /// <returns>Returns the asynchronous task</returns>
        Task UndoAsync(CancellationToken cancellationToken);
    }
}
