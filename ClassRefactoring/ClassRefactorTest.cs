using Xunit;

namespace DeveloperSample.ClassRefactoring
{
    public class ClassRefactorTest
    {
        [Theory]
        [InlineData(SwallowType.African, SwallowLoad.None, 22)]
        [InlineData(SwallowType.African, SwallowLoad.Coconut, 18)]
        [InlineData(SwallowType.European, SwallowLoad.None, 20)]
        [InlineData(SwallowType.European, SwallowLoad.Coconut, 16)]
        public void SwallowHasCorrectSpeed(SwallowType type, SwallowLoad load, double expectedSpeed)
        {
            // Arrange
            var swallowFactory = new SwallowFactory();
            var swallow = swallowFactory.GetSwallow(type);
            swallow.ApplyLoad(load);
            
            // Act
            var actualSpeed = swallow.GetAirspeedVelocity();
            
            // Assert
            Assert.Equal(expectedSpeed, actualSpeed);
        }
    }
}
