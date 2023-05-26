using NotTwice.CA.Interfaces.Commands;

namespace NotTwice.CA.Tests.Fakes
{
    [ExcludeFromCodeCoverage]
    public struct FakeCommandCanExecute : ICommand<FakeCommandCanExecute>
    {
        public bool CanExecute()
        {
            return true;
        }

        public void Execute()
        {
            return;
        }

        public void Undo()
        {
            return;
        }
    }

    [ExcludeFromCodeCoverage]
    public struct FakeCommandCantExecute : ICommand<FakeCommandCantExecute>
    {
        public bool CanExecute()
        {
            return false;
        }

        public void Execute()
        {
            return;
        }

        public void Undo()
        {
            return;
        }
    }

    [ExcludeFromCodeCoverage]
    public struct FakeCommandException : ICommand<FakeCommandException>
    {
        public bool CanExecute()
        {
            return true;
        }

        public void Execute()
        {
            throw new Exception("test");
        }

        public void Undo()
        {
            throw new Exception("test");
        }
    }
}
