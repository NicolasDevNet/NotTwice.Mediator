using NotTwice.CA.Interfaces.Commands;

namespace NotTwice.CA.Tests.Fakes
{
    [ExcludeFromCodeCoverage]
    public struct FakeCommandAsyncCanExecute : ICommandAsync<FakeCommandAsyncCanExecute>
    {
        public bool CanExecute()
        {
            return true;
        }

        public Task ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task UndoAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }

    [ExcludeFromCodeCoverage]
    public struct FakeCommandAsyncCantExecute : ICommandAsync<FakeCommandAsyncCantExecute>
    {
        public bool CanExecute()
        {
            return false;
        }

        public Task ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task UndoAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }

    [ExcludeFromCodeCoverage]
    public struct FakeCommandAsyncException : ICommandAsync<FakeCommandAsyncException>
    {
        public bool CanExecute()
        {
            return true;
        }

        public Task ExecuteAsync(CancellationToken cancellationToken)
        {
            throw new Exception("test");
        }

        public Task UndoAsync(CancellationToken cancellationToken)
        {
            throw new Exception("test");
        }
    }
}
