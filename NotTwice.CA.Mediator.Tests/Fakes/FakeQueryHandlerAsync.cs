using NotTwice.CA.Interfaces.Queries;
using NotTwice.CA.Tests.Fakes.Inputs;

namespace NotTwice.CA.Tests.Fakes
{
    #region Handlers

    [ExcludeFromCodeCoverage]
    public struct FakeQueryHandlerAsyncCanSend : IQueryHandlerAsync<FakeQuery, string>
    {
        public bool CanInteract(FakeQuery @in)
        {
            return true;
        }

        public Task<string> InteractAsync(FakeQuery @in, CancellationToken cancellationToken)
        {
            return Task.FromResult("test");
        }
    }

    [ExcludeFromCodeCoverage]
    public struct FakeQueryHandlerAsyncCantSend : IQueryHandlerAsync<FakeQuery, string>
    {
        public bool CanInteract(FakeQuery @in)
        {
            return false;
        }

        public Task<string> InteractAsync(FakeQuery @in, CancellationToken cancellationToken)
        {
            return Task.FromResult("test");
        }
    }

    [ExcludeFromCodeCoverage]
    public struct FakeQueryHandlerAsyncException : IQueryHandlerAsync<FakeQuery, string>
    {
        public bool CanInteract(FakeQuery @in)
        {
            return true;
        }

        public Task<string> InteractAsync(FakeQuery @in, CancellationToken cancellationToken)
        {
            throw new Exception("test");
        }
    }

    #endregion
}
