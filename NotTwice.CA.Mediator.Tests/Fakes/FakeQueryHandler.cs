using NotTwice.CA.Interfaces.Queries;
using NotTwice.CA.Tests.Fakes.Inputs;

namespace NotTwice.CA.Tests.Fakes
{
    #region Handlers

    [ExcludeFromCodeCoverage]
    public struct FakeQueryHandlerCanSend : IQueryHandler<FakeQuery, string>
    {
        public bool CanInteract(FakeQuery @in)
        {
            return true;
        }

        public string Interact(FakeQuery @in)
        {
            return "test";
        }
    }

    [ExcludeFromCodeCoverage]
    public struct FakeQueryHandlerCantSend : IQueryHandler<FakeQuery, string>
    {
        public bool CanInteract(FakeQuery @in)
        {
            return false;
        }

        public string Interact(FakeQuery @in)
        {
            return "test";
        }
    }

    [ExcludeFromCodeCoverage]
    public struct FakeQueryHandlerException : IQueryHandler<FakeQuery, string>
    {
        public bool CanInteract(FakeQuery @in)
        {
            return true;
        }

        public string Interact(FakeQuery @in)
        {
            throw new Exception("test");
        }
    }

    #endregion
}
