using System;

namespace NotTwice.CA.Interfaces
{
    /// <summary>
    /// Interface contract with a flat pass class
    /// </summary>
    public interface IMediatorContainer
    {
        /// <summary>
        /// Méthode à implémenter permettant de récupérer une intance à partir d'un conteneur existant
        /// </summary>
        /// <typeparam name="T">Le type de l'instance que l'on souhaite récupérer</typeparam>
        /// <param name="instance">L'instance retrouvée</param>
        /// <returns>Le succès de l'opération</returns>
        bool TryResolve<T>(out T instance);

        /// <summary>
        /// Method to be implemented to register an implementation type and interface contract
        /// in the target container transiently
        /// </summary>
        /// <param name="contractType">The interface contract type</param>
        /// <param name="implementationType">The type of implementation</param>
        void RegisterAsTransient(Type contractType ,Type implementationType);

        /// <summary>
        /// Method to be implemented for registering a single instance of a given type and its interface
        /// </summary>
        /// <typeparam name="TContract">The contract type</typeparam>
        /// <param name="implementationType">The type of implementation</param>
        void RegisterAsSingle<TContract>(Type implementationType);
    }
}
