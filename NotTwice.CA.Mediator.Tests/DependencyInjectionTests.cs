using Moq;
using NotTwice.CA.Interfaces;
using System.Reflection;

namespace NotTwice.CA.Tests
{
    [ExcludeFromCodeCoverage]
    public class DependencyInjectionTests
    {
        private readonly Mock<IMediatorContainer> _mediatorContainerStub;

        public DependencyInjectionTests()
        {
            _mediatorContainerStub = new Mock<IMediatorContainer>();
        }

        [Fact]
        public void AddMediator_Test()
        {
            int registerTransientCount = 0;

            //Arrange
            _mediatorContainerStub.Setup(p => p.RegisterAsTransient(It.IsAny<Type>(), It.IsAny<Type>()))
                .Callback(() => registerTransientCount++);

            _mediatorContainerStub.Setup(p => p.RegisterAsSingle<It.IsAnyType>(It.IsAny<Type>()));
            
            //Act
            _mediatorContainerStub.Object.AddMediator(Assembly.GetExecutingAssembly());

            //Assert
            Assert.Equal(18, registerTransientCount);

            //Verify
            _mediatorContainerStub.Verify(p => p.RegisterAsSingle<It.IsAnyType>(It.IsAny<Type>()), Times.Once);
            _mediatorContainerStub.Verify(p => p.RegisterAsTransient(It.IsAny<Type>(), It.IsAny<Type>()), Times.AtLeastOnce);
        }
    }
}
