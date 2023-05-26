using Moq;
using NotTwice.CA.Interfaces.Messages;
using NotTwice.CA.Tests.Fakes.Inputs;

namespace NotTwice.CA.Tests
{
    public partial class MediatorTests
    {
        #region Messages

        #region TrySend

        [Fact]
        public void TrySend_FromContainer_Success_Test()
        {
            //Arrange
            Mediator sut = new Mediator(_mediatorContainerStub.Object);

            FakeMessage input = new FakeMessage();

            IMessenger<FakeMessage> fakeMessenger = new FakeMessengerCanSend();

            _mediatorContainerStub.Setup(p => p.TryResolve(out fakeMessenger))
                .Returns(true);

            //Act
            var actual = sut.TrySend(input);

            //Assert
            Assert.True(actual);

            //Verify
            _mediatorContainerStub.Verify(p => p.TryResolve(out fakeMessenger), Times.Once);
        }

        [Fact]
        public void TrySend_FromContainer_Failed_Test()
        {
            //Arrange
            Mediator sut = new Mediator(_mediatorContainerStub.Object);

            FakeMessage input = new FakeMessage();

            IMessenger<FakeMessage> fakeMessenger = new FakeMessengerCantSend();

            _mediatorContainerStub.Setup(p => p.TryResolve(out fakeMessenger))
                .Returns(true);

            //Act
            var actual = sut.TrySend(input);

            //Assert
            Assert.False(actual);

            //Verify
            _mediatorContainerStub.Verify(p => p.TryResolve(out fakeMessenger), Times.Once);
        }

        [Fact]
        public void TrySend_FromContainer_Exception_Test()
        {
            //Arrange
            Mediator sut = new Mediator(_mediatorContainerStub.Object);

            FakeMessage input = new FakeMessage();

            IMessenger<FakeMessage> fakeMessenger = new FakeMessengerException();

            _mediatorContainerStub.Setup(p => p.TryResolve(out fakeMessenger))
                .Returns(true);

            //Act
            var actual = sut.TrySend(input);

            //Assert
            Assert.False(actual);

            //Verify
            _mediatorContainerStub.Verify(p => p.TryResolve(out fakeMessenger), Times.Once);
        }

        [Fact]
        public void TrySend_FromContainer_CantResolve_Test()
        {
            //Arrange
            Mediator sut = new Mediator(_mediatorContainerStub.Object);

            FakeMessage input = new FakeMessage();

            IMessenger<FakeMessage> fakeMessenger = new FakeMessengerException();

            _mediatorContainerStub.Setup(p => p.TryResolve(out fakeMessenger))
                .Returns(false);

            //Act
            var actual = Assert.ThrowsAny<Exception>(() => sut.TrySend(input));

            //Assert
            Assert.NotNull(actual);

            Assert.Equal("An error occurred during the mediation instance recovery process. Check your instance container. | Type sent: IMessenger`1 | Error code: 2 | Mediation type: Messenger", actual.Message);

            //Verify
            _mediatorContainerStub.Verify(p => p.TryResolve(out fakeMessenger), Times.Once);
        }

        [Fact]
        public void TrySend_FromDictionary_Success_Test()
        {
            //Arrange
            Mediator sut = new Mediator(_mediatorContainerStub.Object);

            FakeMessage input = new FakeMessage();

            IMessenger<FakeMessage> fakeMessenger = new FakeMessengerCanSend();

            _mediatorContainerStub.Setup(p => p.TryResolve(out fakeMessenger))
                .Returns(true);

            //First call to store the command in dictionary
            sut.TrySend(input);

            //Act
            var actual = sut.TrySend(input);

            //Assert
            Assert.True(actual);

            //Verify
            _mediatorContainerStub.Verify(p => p.TryResolve(out fakeMessenger), Times.Once);
        }

        #endregion

        #region TrySendAsync

        [Fact]
        public async Task TrySendAsync_FromContainer_Success_Test()
        {
            //Arrange
            Mediator sut = new Mediator(_mediatorContainerStub.Object);

            FakeMessage input = new FakeMessage();

            IMessengerAsync<FakeMessage> fakeMessenger = new FakeMessengerAsyncCanSend();

            _mediatorContainerStub.Setup(p => p.TryResolve(out fakeMessenger))
                .Returns(true);

            //Act
            var actual = await sut.TrySendAsync(input);

            //Assert
            Assert.True(actual);

            //Verify
            _mediatorContainerStub.Verify(p => p.TryResolve(out fakeMessenger), Times.Once);
        }

        [Fact]
        public async Task TrySendAsync_FromContainer_Failed_Test()
        {
            //Arrange
            Mediator sut = new Mediator(_mediatorContainerStub.Object);

            FakeMessage input = new FakeMessage();

            IMessengerAsync<FakeMessage> fakeMessenger = new FakeMessengerAsyncCantSend();

            _mediatorContainerStub.Setup(p => p.TryResolve(out fakeMessenger))
                .Returns(true);

            //Act
            var actual = await sut.TrySendAsync(input);

            //Assert
            Assert.False(actual);

            //Verify
            _mediatorContainerStub.Verify(p => p.TryResolve(out fakeMessenger), Times.Once);
        }

        [Fact]
        public async Task TrySendAsync_FromContainer_Exception_Test()
        {
            //Arrange
            Mediator sut = new Mediator(_mediatorContainerStub.Object);

            FakeMessage input = new FakeMessage();

            IMessengerAsync<FakeMessage> fakeMessenger = new FakeMessengerAsyncException();

            _mediatorContainerStub.Setup(p => p.TryResolve(out fakeMessenger))
                .Returns(true);

            //Act
            var actual = await sut.TrySendAsync(input);

            //Assert
            Assert.False(actual);

            //Verify
            _mediatorContainerStub.Verify(p => p.TryResolve(out fakeMessenger), Times.Once);
        }

        [Fact]
        public async Task TrySendAsync_FromContainer_CantResolve_Test()
        {
            //Arrange
            Mediator sut = new Mediator(_mediatorContainerStub.Object);

            FakeMessage input = new FakeMessage();

            IMessengerAsync<FakeMessage> fakeMessenger = new FakeMessengerAsyncException();

            _mediatorContainerStub.Setup(p => p.TryResolve(out fakeMessenger))
                .Returns(false);

            //Act
            var actual = await Assert.ThrowsAnyAsync<Exception>(async () => await sut.TrySendAsync(input));

            //Assert
            Assert.NotNull(actual);

            Assert.Equal("An error occurred during the mediation instance recovery process. Check your instance container. | Type sent: IMessengerAsync`1 | Error code: 2 | Mediation type: MessengerAsync", actual.Message);

            //Verify
            _mediatorContainerStub.Verify(p => p.TryResolve(out fakeMessenger), Times.Once);
        }

        [Fact]
        public async Task TrySendAsync_FromDictionary_Success_Test()
        {
            //Arrange
            Mediator sut = new Mediator(_mediatorContainerStub.Object);

            FakeMessage input = new FakeMessage();

            IMessengerAsync<FakeMessage> fakeMessenger = new FakeMessengerAsyncCanSend();

            _mediatorContainerStub.Setup(p => p.TryResolve(out fakeMessenger))
                .Returns(true);

            //First call to store the command in dictionary
            await sut.TrySendAsync(input);

            //Act
            var actual = await sut.TrySendAsync(input);

            //Assert
            Assert.True(actual);

            //Verify
            _mediatorContainerStub.Verify(p => p.TryResolve(out fakeMessenger), Times.Once);
        }

        #endregion

        #endregion

    }
}
