using Moq;
using NotTwice.CA.Interfaces;
using Serilog;

namespace NotTwice.CA.Tests
{
    [ExcludeFromCodeCoverage]
    public partial class MediatorTests
    {
        private readonly Mock<IMediatorContainer> _mediatorContainerStub;
        private readonly Mock<ILogger> _loggerStub;

        public MediatorTests()
        {
            _mediatorContainerStub = new Mock<IMediatorContainer>();
            _loggerStub = new Mock<ILogger>();
        }

        #region Constructor

        [Fact]
        public void Constructor_Test()
        {
            //Act
            var actual = new Mediator(_mediatorContainerStub.Object, _loggerStub.Object);

            //Assert
            Assert.NotNull(actual);
        }

        [Fact]
        public void Constructor_Logger_Null_Test()
        {
            //Act
            var actual = new Mediator(_mediatorContainerStub.Object);

            //Assert
            Assert.NotNull(actual);
        }

        [Fact]
        public void Constructor_Mediator_Container_Null_Test()
        {
            //Act
            var actual = Assert.ThrowsAny<Exception>(() => new Mediator(null, _loggerStub.Object));

            //Assert
            Assert.NotNull(actual);
            Assert.Equal("A parameter is missing to use the mediator correctly. | Error code: 3 | Mediation type: Mediator", actual.Message);

            Assert.NotNull(actual.InnerException);
            Assert.Equal("Value cannot be null. (Parameter 'mediatorContainer')", actual.InnerException.Message);
        }

        #endregion

    }
}
