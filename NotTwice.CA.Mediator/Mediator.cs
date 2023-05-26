using NotTwice.CA.Enums;
using NotTwice.CA.Exceptions;
using NotTwice.CA.Extentions;
using NotTwice.CA.Interfaces;
using NotTwice.CA.Interfaces.Commands;
using NotTwice.CA.Interfaces.Queries;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NotTwice.CA.Interfaces.Messages;

namespace NotTwice.CA
{
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public sealed class Mediator : IMediator
    {
        private readonly IMediatorContainer _container;
        private readonly ILogger _logger;

        #region Collections

        private Dictionary<Type, object> _commands;
        private Dictionary<Type, object> _asyncCommands;

        private Dictionary<Type, object> _queryHandlers;
        private Dictionary<Type, object> _asyncQueryHandlers;

        private Dictionary<Type, object> _messengers;
        private Dictionary<Type, object> _asyncMessengers;

        #endregion


        public Mediator(IMediatorContainer mediatorContainer, ILogger logger = null)
        {
            if (mediatorContainer == null)
                throw new MediatorException(ErrorType.FailedToInitMediator, MediationType.None, new ArgumentNullException(nameof(mediatorContainer)));

            _container = mediatorContainer;
            _logger = logger;

            _commands = new Dictionary<Type, object>();
            _asyncCommands = new Dictionary<Type, object>();

            _queryHandlers = new Dictionary<Type, object>();
            _asyncQueryHandlers = new Dictionary<Type, object>();

            _messengers = new Dictionary<Type, object>();
            _asyncMessengers = new Dictionary<Type, object>();
        }

        #region Commands

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public bool TryExecute<T>()
            where T : struct, ICommand<T>
        {
            T mediation = RetrieveMediation<T>(_commands, MediationType.Command);

            if (!mediation.CanExecute())
                return false;

            try
            {
                mediation.Execute();
                return true;
            }
            catch (Exception ex)
            {
                _logger?.LogMediatorErrorAsInformation(ErrorType.CommandFailed, MediationType.Command, ex);
                return false;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public bool TryUndo<T>()
            where T : struct, ICommand<T>
        {
            T mediation = RetrieveMediation<T>(_commands, MediationType.Command);

            try
            {
                mediation.Undo();
                return true;
            }
            catch (Exception ex)
            {
                _logger?.LogMediatorErrorAsInformation(ErrorType.CommandFailed, MediationType.Command, ex);
                return false;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public async Task<bool> TryExecuteAsync<T>()
            where T : struct, ICommandAsync<T>
        {
            T mediation = RetrieveMediation<T>(_asyncCommands, MediationType.CommandAsync);

            if (!mediation.CanExecute())
                return false;

            try
            {
                await mediation.ExecuteAsync(new CancellationToken());
                return true;
            }
            catch (Exception ex)
            {
                _logger?.LogMediatorErrorAsInformation(ErrorType.CommandFailed, MediationType.CommandAsync, ex);
                return false;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public async Task<bool> TryUndoAsync<T>()
            where T : struct, ICommandAsync<T>
        {
            T mediation = RetrieveMediation<T>(_asyncCommands, MediationType.CommandAsync);

            try
            {
                await mediation.UndoAsync(new CancellationToken());
                return true;
            }
            catch (Exception ex)
            {
                _logger?.LogMediatorErrorAsInformation(ErrorType.CommandFailed, MediationType.CommandAsync, ex);
                return false;
            }
        }

        #endregion

        #region Query

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public bool TryInteract<TIn, TOut>(TIn @in, out TOut @out)
            where TIn : struct, IQuery<TOut>
        {
            IQueryHandler<TIn, TOut> mediation = RetrieveMediation<IQueryHandler<TIn, TOut>>(_queryHandlers, MediationType.Query);

            @out = default(TOut);

            if (!mediation.CanInteract(@in))
                return false;

            try
            {
                @out = mediation.Interact(@in);
                return true;
            }
            catch (Exception ex)
            {
                _logger?.LogMediatorErrorAsInformation(ErrorType.QueryFailed, MediationType.Query, ex);
                return false;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public async Task<(bool success, TOut result)> TryInteractAsync<TIn, TOut>(TIn @in)
            where TIn : struct, IQuery<TOut>
        {
            IQueryHandlerAsync<TIn, TOut> mediation = RetrieveMediation<IQueryHandlerAsync<TIn, TOut>>(_asyncQueryHandlers, MediationType.QueryAsync);

            if (!mediation.CanInteract(@in))
                return (false, default(TOut));

            try
            {
                return (true, await mediation.InteractAsync(@in, new CancellationToken()));
            }
            catch (Exception ex)
            {
                _logger?.LogMediatorErrorAsInformation(ErrorType.QueryFailed, MediationType.QueryAsync, ex);
                return (false, default(TOut));
            }
        }

        #endregion

        #region Messages

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public bool TrySend<T>(T @in)
             where T : struct, IMessage
        {
            IMessenger<T> mediation = RetrieveMediation<IMessenger<T>>(_messengers, MediationType.Messenger);

            if (!mediation.CanSend(@in))
                return false;

            try
            {
                mediation.Send(@in);
                return true;
            }
            catch (Exception ex)
            {
                _logger?.LogMediatorErrorAsInformation(ErrorType.MessageFailed, MediationType.Messenger, ex);
                return false;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public async Task<bool> TrySendAsync<T>(T @in)
            where T : struct, IMessage
        {
            IMessengerAsync<T> mediation = RetrieveMediation<IMessengerAsync<T>>(_asyncMessengers, MediationType.MessengerAsync);

            if (!mediation.CanSend(@in))
                return false;

            try
            {
                await mediation.SendAsync(@in, new CancellationToken());
                return true;
            }
            catch (Exception ex)
            {
                _logger?.LogMediatorErrorAsInformation(ErrorType.MessageFailed, MediationType.MessengerAsync, ex);
                return false;
            }
        }

        #endregion

        #region Private

        private T RetrieveMediation<T>(Dictionary<Type, object> mediationsDictionary, MediationType mediationType)
        {
            T mediation;

            if (mediationsDictionary.TryGetValue(typeof(T), out object foundResource)
                && foundResource is T instance)
                mediation = instance;
            else
            {
                if (!_container.TryResolve(out mediation))
                    throw new MediatorException<T>(ErrorType.FailedToRetrieveInstance, mediationType);
                else
                    mediationsDictionary.Add(typeof(T), mediation);
            }

            return mediation;
        }

        #endregion
    }
}
