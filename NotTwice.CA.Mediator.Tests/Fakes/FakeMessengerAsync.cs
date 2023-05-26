using NotTwice.CA.Interfaces.Messages;
using NotTwice.CA.Tests.Fakes.Inputs;

namespace NotTwice.CA.Tests.Fakes
{
    #region Messengers

    [ExcludeFromCodeCoverage]
    public struct FakeMessengerAsyncCanSend : IMessengerAsync<FakeMessage>
    {
        public bool CanSend(FakeMessage message)
        {
            return true;
        }

        public Task SendAsync(FakeMessage message, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }

    [ExcludeFromCodeCoverage]
    public struct FakeMessengerAsyncCantSend : IMessengerAsync<FakeMessage>
    {
        public bool CanSend(FakeMessage message)
        {
            return false;
        }

        public Task SendAsync(FakeMessage message, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }

    [ExcludeFromCodeCoverage]
    public struct FakeMessengerAsyncException : IMessengerAsync<FakeMessage>
    {
        public bool CanSend(FakeMessage message)
        {
            return true;
        }

        public Task SendAsync(FakeMessage message, CancellationToken cancellationToken)
        {
            throw new Exception("test");
        }
    }
    #endregion
}
