using Moq;
using NotTwice.CA.Interfaces.Queries;
using NotTwice.CA.Tests.Fakes.Inputs;

namespace NotTwice.CA.Tests
{
    public partial class MediatorTests
    {
        #region Query

        #region TryInteract

        [Fact]
        public void TryInteract_FromContainer_Success_Test()
        {
            //Arrange
            Mediator sut = new Mediator(_mediatorContainerStub.Object);

            FakeQuery input = new FakeQuery();

            IQueryHandler<FakeQuery, string> fakeQueryHandler = new FakeQueryHandlerCanSend();

            _mediatorContainerStub.Setup(p => p.TryResolve(out fakeQueryHandler))
                .Returns(true);

            //Act
            var actual = sut.TryInteract(input, out string result);

            //Assert
            Assert.True(actual);

            Assert.Equal("test", result);

            //Verify
            _mediatorContainerStub.Verify(p => p.TryResolve(out fakeQueryHandler), Times.Once);
        }

        [Fact]
        public void TryInteract_FromContainer_Failed_Test()
        {
            //Arrange
            Mediator sut = new Mediator(_mediatorContainerStub.Object);

            FakeQuery input = new FakeQuery();

            IQueryHandler<FakeQuery, string> fakeQueryHandler = new FakeQueryHandlerCantSend();

            _mediatorContainerStub.Setup(p => p.TryResolve(out fakeQueryHandler))
                .Returns(true);

            //Act
            var actual = sut.TryInteract(input, out string result);

            //Assert
            Assert.False(actual);

            Assert.Null(result);

            //Verify
            _mediatorContainerStub.Verify(p => p.TryResolve(out fakeQueryHandler), Times.Once);
        }

        [Fact]
        public void TryInteract_FromContainer_Exception_Test()
        {
            //Arrange
            Mediator sut = new Mediator(_mediatorContainerStub.Object);

            FakeQuery input = new FakeQuery();

            IQueryHandler<FakeQuery, string> fakeQueryHandler = new FakeQueryHandlerException();

            _mediatorContainerStub.Setup(p => p.TryResolve(out fakeQueryHandler))
                .Returns(true);

            //Act
            var actual = sut.TryInteract(input, out string result);

            //Assert
            Assert.False(actual);

            Assert.Null(result);

            //Verify
            _mediatorContainerStub.Verify(p => p.TryResolve(out fakeQueryHandler), Times.Once);
        }

        [Fact]
        public void TryInteract_FromContainer_CantResolve_Test()
        {
            //Arrange
            Mediator sut = new Mediator(_mediatorContainerStub.Object);

            FakeQuery input = new FakeQuery();

            IQueryHandler<FakeQuery, string> fakeQueryHandler = new FakeQueryHandlerException();

            _mediatorContainerStub.Setup(p => p.TryResolve(out fakeQueryHandler))
                .Returns(false);

            //Act
            var actual = Assert.ThrowsAny<Exception>(() => sut.TryInteract(input, out string result));

            //Assert
            Assert.NotNull(actual);

            Assert.Equal("An error occurred during the mediation instance recovery process. Check your instance container. | Type sent: IQueryHandler`2 | Error code: 2 | Mediation type: Query", actual.Message);

            //Verify
            _mediatorContainerStub.Verify(p => p.TryResolve(out fakeQueryHandler), Times.Once);
        }

        [Fact]
        public void TryInteract_FromDictionary_Success_Test()
        {
            //Arrange
            Mediator sut = new Mediator(_mediatorContainerStub.Object);

            FakeQuery input = new FakeQuery();

            IQueryHandler<FakeQuery, string> fakeQueryHandler = new FakeQueryHandlerCanSend();

            _mediatorContainerStub.Setup(p => p.TryResolve(out fakeQueryHandler))
                .Returns(true);

            //First call to store the command in dictionary
            sut.TryInteract(input, out string result);

            //Act
            var actual = sut.TryInteract(input, out result);

            //Assert
            Assert.True(actual);

            Assert.Equal("test", result);

            //Verify
            _mediatorContainerStub.Verify(p => p.TryResolve(out fakeQueryHandler), Times.Once);
        }

        #endregion

        #region TryInteractAsync

        [Fact]
        public async Task TryInteractAsync_FromContainer_Success_Test()
        {
            //Arrange
            Mediator sut = new Mediator(_mediatorContainerStub.Object);

            FakeQuery input = new FakeQuery();

            IQueryHandlerAsync<FakeQuery, string> fakeQueryHandler = new FakeQueryHandlerAsyncCanSend();

            _mediatorContainerStub.Setup(p => p.TryResolve(out fakeQueryHandler))
                .Returns(true);

            //Act
            var actual = await sut.TryInteractAsync<FakeQuery, string>(input);

            //Assert
            Assert.True(actual.success);

            Assert.Equal("test", actual.result);

            //Verify
            _mediatorContainerStub.Verify(p => p.TryResolve(out fakeQueryHandler), Times.Once);
        }

        [Fact]
        public async Task TryInteractAsync_FromContainer_Failed_Test()
        {
            //Arrange
            Mediator sut = new Mediator(_mediatorContainerStub.Object);

            FakeQuery input = new FakeQuery();

            IQueryHandlerAsync<FakeQuery, string> fakeQueryHandler = new FakeQueryHandlerAsyncCantSend();

            _mediatorContainerStub.Setup(p => p.TryResolve(out fakeQueryHandler))
                .Returns(true);

            //Act
            var actual = await sut.TryInteractAsync<FakeQuery, string>(input);

            //Assert
            Assert.False(actual.success);

            Assert.Null(actual.result);

            //Verify
            _mediatorContainerStub.Verify(p => p.TryResolve(out fakeQueryHandler), Times.Once);
        }

        [Fact]
        public async Task TryInteractAsync_FromContainer_Exception_Test()
        {
            //Arrange
            Mediator sut = new Mediator(_mediatorContainerStub.Object);

            FakeQuery input = new FakeQuery();

            IQueryHandlerAsync<FakeQuery, string> fakeQueryHandler = new FakeQueryHandlerAsyncException();

            _mediatorContainerStub.Setup(p => p.TryResolve(out fakeQueryHandler))
                .Returns(true);

            //Act
            var actual = await sut.TryInteractAsync<FakeQuery, string>(input);

            //Assert
            Assert.False(actual.success);

            Assert.Null(actual.result);

            //Verify
            _mediatorContainerStub.Verify(p => p.TryResolve(out fakeQueryHandler), Times.Once);
        }

        [Fact]
        public async Task TryInteractAsync_FromContainer_CantResolve_Test()
        {
            //Arrange
            Mediator sut = new Mediator(_mediatorContainerStub.Object);

            FakeQuery input = new FakeQuery();

            IQueryHandlerAsync<FakeQuery, string> fakeQueryHandler = new FakeQueryHandlerAsyncException();

            _mediatorContainerStub.Setup(p => p.TryResolve(out fakeQueryHandler))
                .Returns(false);

            //Act
            var actual = await Assert.ThrowsAnyAsync<Exception>(async () => await sut.TryInteractAsync<FakeQuery, string>(input));

            //Assert
            Assert.NotNull(actual);

            Assert.Equal("An error occurred during the mediation instance recovery process. Check your instance container. | Type sent: IQueryHandlerAsync`2 | Error code: 2 | Mediation type: QueryAsync", actual.Message);

            //Verify
            _mediatorContainerStub.Verify(p => p.TryResolve(out fakeQueryHandler), Times.Once);
        }

        [Fact]
        public async Task TryInteractAsync_FromDictionary_Success_Test()
        {
            //Arrange
            Mediator sut = new Mediator(_mediatorContainerStub.Object);

            FakeQuery input = new FakeQuery();

            IQueryHandlerAsync<FakeQuery, string> fakeQueryHandler = new FakeQueryHandlerAsyncCanSend();

            _mediatorContainerStub.Setup(p => p.TryResolve(out fakeQueryHandler))
                .Returns(true);

            //First call to store the command in dictionary
            await sut.TryInteractAsync<FakeQuery, string>(input);

            //Act
            var actual = await sut.TryInteractAsync<FakeQuery, string>(input);

            //Assert
            Assert.True(actual.success);

            Assert.Equal("test", actual.result);

            //Verify
            _mediatorContainerStub.Verify(p => p.TryResolve(out fakeQueryHandler), Times.Once);
        }

        #endregion

        #endregion
    }
}
