using NotTwice.CA.Interfaces.Messages;
using NotTwice.CA.Tests.Fakes.Inputs;

namespace NotTwice.CA.Tests.Fakes
{
    #region Messengers

    [ExcludeFromCodeCoverage]
    public struct FakeMessengerCanSend : IMessenger<FakeMessage>
    {
        public bool CanSend(FakeMessage message)
        {
            return true;
        }

        public void Send(FakeMessage message)
        {
            return;
        }
    }

    [ExcludeFromCodeCoverage]
    public struct FakeMessengerCantSend : IMessenger<FakeMessage>
    {
        public bool CanSend(FakeMessage message)
        {
            return false;
        }

        public void Send(FakeMessage message)
        {
            return;
        }
    }

    [ExcludeFromCodeCoverage]
    public struct FakeMessengerException : IMessenger<FakeMessage>
    {
        public bool CanSend(FakeMessage message)
        {
            return true;
        }

        public void Send(FakeMessage message)
        {
            throw new Exception("test");
        }
    }
    #endregion
}
