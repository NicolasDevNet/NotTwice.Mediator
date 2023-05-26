using NotTwice.CA.Interfaces;
using NotTwice.CA.Interfaces.Commands;
using NotTwice.CA.Interfaces.Messages;
using NotTwice.CA.Interfaces.Queries;
using System;
using System.Linq;
using System.Reflection;

namespace NotTwice.CA
{
    public static class DependencyInjection
    {
        /// <summary>
        /// Method for adding Mediator to an application without having to add it manually
        /// commands, queries and messages
        /// </summary>
        /// <param name="container">The implemented container acting as a flat pass between an existing injection system and Mediator's</param>
        /// <param name="targetAssembly">The target assembly in which the resource search is to be performed</param>
        /// <returns>The container passed as parameters</returns>
        public static IMediatorContainer AddMediator(this IMediatorContainer container, Assembly targetAssembly)
        {
            //Register Commands
            container.RegisterMediation(typeof(ICommand<>), targetAssembly);

            container.RegisterMediation(typeof(ICommandAsync<>), targetAssembly);

            //Register Messengers
            container.RegisterMediation(typeof(IMessenger<>), targetAssembly);

            container.RegisterMediation(typeof(IMessengerAsync<>), targetAssembly);

            //Register Query handlers
            container.RegisterMediation(typeof(IQueryHandler<,>), targetAssembly);

            container.RegisterMediation(typeof(IQueryHandlerAsync<,>), targetAssembly);

            //Register Mediator
            container.RegisterAsSingle<IMediator>(typeof(Mediator));

            return container;
        }

        private static IMediatorContainer RegisterMediation(this IMediatorContainer container, Type mediationType, Assembly targetAssembly)
        {
            //Register Mediations
            foreach (var mediation in targetAssembly.GetTypes().Where(p => p.GetInterfaces().Any(x =>
              x.IsGenericType && x.GetGenericTypeDefinition() == mediationType)))
            {
                var interfaceType = mediation.GetInterfaces().FirstOrDefault(x => x.IsGenericType && x.GetGenericTypeDefinition() == mediationType);

                container.RegisterAsTransient(interfaceType, mediation);
            }

            return container;
        }
    }
}
