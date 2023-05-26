using Moq;

namespace NotTwice.CA.Tests
{
    public partial class MediatorTests
    {
        #region Commands

        #region TryExecute

        [Fact]
        public void TryExecute_FromContainer_Success_Test()
        {
            //Arrange
            Mediator sut = new Mediator(_mediatorContainerStub.Object);

            _mediatorContainerStub.Setup(p => p.TryResolve(out It.Ref<FakeCommandCanExecute>.IsAny))
                .Returns(true);

            //Act
            var actual = sut.TryExecute<FakeCommandCanExecute>();

            //Assert
            Assert.True(actual);

            //Verify
            _mediatorContainerStub.Verify(p => p.TryResolve(out It.Ref<FakeCommandCanExecute>.IsAny), Times.Once);
        }

        [Fact]
        public void TryExecute_FromContainer_Failed_Test()
        {
            //Arrange
            Mediator sut = new Mediator(_mediatorContainerStub.Object);

            _mediatorContainerStub.Setup(p => p.TryResolve(out It.Ref<FakeCommandCantExecute>.IsAny))
                .Returns(true);

            //Act
            var actual = sut.TryExecute<FakeCommandCantExecute>();

            //Assert
            Assert.False(actual);

            //Verify
            _mediatorContainerStub.Verify(p => p.TryResolve(out It.Ref<FakeCommandCantExecute>.IsAny), Times.Once);
        }

        [Fact]
        public void TryExecute_FromContainer_Exception_Test()
        {
            //Arrange
            Mediator sut = new Mediator(_mediatorContainerStub.Object);

            _mediatorContainerStub.Setup(p => p.TryResolve(out It.Ref<FakeCommandException>.IsAny))
                .Returns(true);

            //Act
            var actual = sut.TryExecute<FakeCommandException>();

            //Assert
            Assert.False(actual);

            //Verify
            _mediatorContainerStub.Verify(p => p.TryResolve(out It.Ref<FakeCommandException>.IsAny), Times.Once);
        }

        [Fact]
        public void TryExecute_FromContainer_CantResolve_Test()
        {
            //Arrange
            Mediator sut = new Mediator(_mediatorContainerStub.Object);

            _mediatorContainerStub.Setup(p => p.TryResolve(out It.Ref<FakeCommandException>.IsAny))
                .Returns(false);

            //Act
            var actual = Assert.ThrowsAny<Exception>(() => sut.TryExecute<FakeCommandException>());

            //Assert
            Assert.NotNull(actual);

            Assert.Equal("An error occurred during the mediation instance recovery process. Check your instance container. | Type sent: FakeCommandException | Error code: 2 | Mediation type: Command", actual.Message);

            //Verify
            _mediatorContainerStub.Verify(p => p.TryResolve(out It.Ref<FakeCommandException>.IsAny), Times.Once);
        }

        [Fact]
        public void TryExecute_FromDictionary_Success_Test()
        {
            //Arrange
            Mediator sut = new Mediator(_mediatorContainerStub.Object);

            _mediatorContainerStub.Setup(p => p.TryResolve(out It.Ref<FakeCommandCanExecute>.IsAny))
                .Returns(true);

            //First call to store the command in dictionary
            sut.TryExecute<FakeCommandCanExecute>();

            //Act
            var actual = sut.TryExecute<FakeCommandCanExecute>();

            //Assert
            Assert.True(actual);

            //Verify
            _mediatorContainerStub.Verify(p => p.TryResolve(out It.Ref<FakeCommandCanExecute>.IsAny), Times.Once);
        }

        #endregion

        #region TryExecuteAsync

        [Fact]
        public async Task TryExecuteAsync_FromContainer_Success_Test()
        {
            //Arrange
            Mediator sut = new Mediator(_mediatorContainerStub.Object);

            _mediatorContainerStub.Setup(p => p.TryResolve(out It.Ref<FakeCommandAsyncCanExecute>.IsAny))
                .Returns(true);

            //Act
            var actual = await sut.TryExecuteAsync<FakeCommandAsyncCanExecute>();

            //Assert
            Assert.True(actual);

            //Verify
            _mediatorContainerStub.Verify(p => p.TryResolve(out It.Ref<FakeCommandAsyncCanExecute>.IsAny), Times.Once);
        }

        [Fact]
        public async Task TryExecuteAsync_FromContainer_Failed_Test()
        {
            //Arrange
            Mediator sut = new Mediator(_mediatorContainerStub.Object);

            _mediatorContainerStub.Setup(p => p.TryResolve(out It.Ref<FakeCommandAsyncCantExecute>.IsAny))
                .Returns(true);

            //Act
            var actual = await sut.TryExecuteAsync<FakeCommandAsyncCantExecute>();

            //Assert
            Assert.False(actual);

            //Verify
            _mediatorContainerStub.Verify(p => p.TryResolve(out It.Ref<FakeCommandAsyncCantExecute>.IsAny), Times.Once);
        }

        [Fact]
        public async Task TryExecuteAsync_FromContainer_Exception_Test()
        {
            //Arrange
            Mediator sut = new Mediator(_mediatorContainerStub.Object);

            _mediatorContainerStub.Setup(p => p.TryResolve(out It.Ref<FakeCommandAsyncException>.IsAny))
                .Returns(true);

            //Act
            var actual = await sut.TryExecuteAsync<FakeCommandAsyncException>();

            //Assert
            Assert.False(actual);

            //Verify
            _mediatorContainerStub.Verify(p => p.TryResolve(out It.Ref<FakeCommandAsyncException>.IsAny), Times.Once);
        }

        [Fact]
        public async Task TryExecuteAsync_FromContainer_CantResolve_Test()
        {
            //Arrange
            Mediator sut = new Mediator(_mediatorContainerStub.Object);

            _mediatorContainerStub.Setup(p => p.TryResolve(out It.Ref<FakeCommandAsyncException>.IsAny))
                .Returns(false);

            //Act
            var actual = await Assert.ThrowsAnyAsync<Exception>(async () => await sut.TryExecuteAsync<FakeCommandAsyncException>());

            //Assert
            Assert.NotNull(actual);

            Assert.Equal("An error occurred during the mediation instance recovery process. Check your instance container. | Type sent: FakeCommandAsyncException | Error code: 2 | Mediation type: CommandAsync", actual.Message);

            //Verify
            _mediatorContainerStub.Verify(p => p.TryResolve(out It.Ref<FakeCommandAsyncException>.IsAny), Times.Once);
        }

        [Fact]
        public async Task TryExecuteAsync_FromDictionary_Success_Test()
        {
            //Arrange
            Mediator sut = new Mediator(_mediatorContainerStub.Object);

            _mediatorContainerStub.Setup(p => p.TryResolve(out It.Ref<FakeCommandAsyncCanExecute>.IsAny))
                .Returns(true);

            //First call to store the command in dictionary
            await sut.TryExecuteAsync<FakeCommandAsyncCanExecute>();

            //Act
            var actual = await sut.TryExecuteAsync<FakeCommandAsyncCanExecute>();

            //Assert
            Assert.True(actual);

            //Verify
            _mediatorContainerStub.Verify(p => p.TryResolve(out It.Ref<FakeCommandAsyncCanExecute>.IsAny), Times.Once);
        }

        #endregion

        #region TryUndo

        [Fact]
        public void TryUndo_FromContainer_Success_Test()
        {
            //Arrange
            Mediator sut = new Mediator(_mediatorContainerStub.Object);

            _mediatorContainerStub.Setup(p => p.TryResolve(out It.Ref<FakeCommandCanExecute>.IsAny))
                .Returns(true);

            //Act
            var actual = sut.TryUndo<FakeCommandCanExecute>();

            //Assert
            Assert.True(actual);

            //Verify
            _mediatorContainerStub.Verify(p => p.TryResolve(out It.Ref<FakeCommandCanExecute>.IsAny), Times.Once);
        }

        [Fact]
        public void TryUndo_FromContainer_Failed_Test()
        {
            //Arrange
            Mediator sut = new Mediator(_mediatorContainerStub.Object);

            _mediatorContainerStub.Setup(p => p.TryResolve(out It.Ref<FakeCommandCantExecute>.IsAny))
                .Returns(true);

            //Act
            var actual = sut.TryUndo<FakeCommandCantExecute>();

            //Assert
            Assert.True(actual);

            //Verify
            _mediatorContainerStub.Verify(p => p.TryResolve(out It.Ref<FakeCommandCantExecute>.IsAny), Times.Once);
        }

        [Fact]
        public void TryUndo_FromContainer_Exception_Test()
        {
            //Arrange
            Mediator sut = new Mediator(_mediatorContainerStub.Object);

            _mediatorContainerStub.Setup(p => p.TryResolve(out It.Ref<FakeCommandException>.IsAny))
                .Returns(true);

            //Act
            var actual = sut.TryUndo<FakeCommandException>();

            //Assert
            Assert.False(actual);

            //Verify
            _mediatorContainerStub.Verify(p => p.TryResolve(out It.Ref<FakeCommandException>.IsAny), Times.Once);
        }

        [Fact]
        public void TryUndo_FromContainer_CantResolve_Test()
        {
            //Arrange
            Mediator sut = new Mediator(_mediatorContainerStub.Object);

            _mediatorContainerStub.Setup(p => p.TryResolve(out It.Ref<FakeCommandException>.IsAny))
                .Returns(false);

            //Act
            var actual = Assert.ThrowsAny<Exception>(() => sut.TryUndo<FakeCommandException>());

            //Assert
            Assert.NotNull(actual);

            Assert.Equal("An error occurred during the mediation instance recovery process. Check your instance container. | Type sent: FakeCommandException | Error code: 2 | Mediation type: Command", actual.Message);

            //Verify
            _mediatorContainerStub.Verify(p => p.TryResolve(out It.Ref<FakeCommandException>.IsAny), Times.Once);
        }

        [Fact]
        public void TryUndo_FromDictionary_Success_Test()
        {
            //Arrange
            Mediator sut = new Mediator(_mediatorContainerStub.Object);

            _mediatorContainerStub.Setup(p => p.TryResolve(out It.Ref<FakeCommandCanExecute>.IsAny))
                .Returns(true);

            //First call to store the command in dictionary
            sut.TryUndo<FakeCommandCanExecute>();

            //Act
            var actual = sut.TryUndo<FakeCommandCanExecute>();

            //Assert
            Assert.True(actual);

            //Verify
            _mediatorContainerStub.Verify(p => p.TryResolve(out It.Ref<FakeCommandCanExecute>.IsAny), Times.Once);
        }

        #endregion

        #region TryUndoAsync

        [Fact]
        public async Task TryUndoAsync_FromContainer_Success_Test()
        {
            //Arrange
            Mediator sut = new Mediator(_mediatorContainerStub.Object);

            _mediatorContainerStub.Setup(p => p.TryResolve(out It.Ref<FakeCommandAsyncCanExecute>.IsAny))
                .Returns(true);

            //Act
            var actual = await sut.TryUndoAsync<FakeCommandAsyncCanExecute>();

            //Assert
            Assert.True(actual);

            //Verify
            _mediatorContainerStub.Verify(p => p.TryResolve(out It.Ref<FakeCommandAsyncCanExecute>.IsAny), Times.Once);
        }

        [Fact]
        public async Task TryUndoAsync_FromContainer_Failed_Test()
        {
            //Arrange
            Mediator sut = new Mediator(_mediatorContainerStub.Object);

            _mediatorContainerStub.Setup(p => p.TryResolve(out It.Ref<FakeCommandAsyncCantExecute>.IsAny))
                .Returns(true);

            //Act
            var actual = await sut.TryUndoAsync<FakeCommandAsyncCantExecute>();

            //Assert
            Assert.True(actual);

            //Verify
            _mediatorContainerStub.Verify(p => p.TryResolve(out It.Ref<FakeCommandAsyncCantExecute>.IsAny), Times.Once);
        }

        [Fact]
        public async Task TryUndoAsync_FromContainer_Exception_Test()
        {
            //Arrange
            Mediator sut = new Mediator(_mediatorContainerStub.Object);

            _mediatorContainerStub.Setup(p => p.TryResolve(out It.Ref<FakeCommandAsyncException>.IsAny))
                .Returns(true);

            //Act
            var actual = await sut.TryUndoAsync<FakeCommandAsyncException>();

            //Assert
            Assert.False(actual);

            //Verify
            _mediatorContainerStub.Verify(p => p.TryResolve(out It.Ref<FakeCommandAsyncException>.IsAny), Times.Once);
        }

        [Fact]
        public async Task TryUndoAsync_FromContainer_CantResolve_Test()
        {
            //Arrange
            Mediator sut = new Mediator(_mediatorContainerStub.Object);

            _mediatorContainerStub.Setup(p => p.TryResolve(out It.Ref<FakeCommandAsyncException>.IsAny))
                .Returns(false);

            //Act
            var actual = await Assert.ThrowsAnyAsync<Exception>(async () => await sut.TryUndoAsync<FakeCommandAsyncException>());

            //Assert
            Assert.NotNull(actual);

            Assert.Equal("An error occurred during the mediation instance recovery process. Check your instance container. | Type sent: FakeCommandAsyncException | Error code: 2 | Mediation type: CommandAsync", actual.Message);

            //Verify
            _mediatorContainerStub.Verify(p => p.TryResolve(out It.Ref<FakeCommandAsyncException>.IsAny), Times.Once);
        }

        [Fact]
        public async Task TryUndoAsync_FromDictionary_Success_Test()
        {
            //Arrange
            Mediator sut = new Mediator(_mediatorContainerStub.Object);

            _mediatorContainerStub.Setup(p => p.TryResolve(out It.Ref<FakeCommandAsyncCanExecute>.IsAny))
                .Returns(true);

            //First call to store the command in dictionary
            await sut.TryUndoAsync<FakeCommandAsyncCanExecute>();

            //Act
            var actual = await sut.TryUndoAsync<FakeCommandAsyncCanExecute>();

            //Assert
            Assert.True(actual);

            //Verify
            _mediatorContainerStub.Verify(p => p.TryResolve(out It.Ref<FakeCommandAsyncCanExecute>.IsAny), Times.Once);
        }

        #endregion

        #endregion
    }
}
