using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestApp.UnitTests
{

    // MSTest
    
    [TestClass]
    public class MathCalculatorUnitTests
    {
        private MathCalculator mathCalculator;

        [TestInitialize]
        public void Setup()
        {
            // Arrange
            mathCalculator = new MathCalculator();
        }


        // Method_Scenario_Behavior


        [TestMethod]
        public void Add_WhenCalled_ReturnsTheSumOfArguments()
        {
            // Arrange

            // Act
            var result = mathCalculator.Add(1, 2);

            // Assert
            Assert.AreEqual(3, result);

        }

        [TestMethod]
        public void Max_FirstArgumentIsGreater_ReturnsTheFirstArgument()
        {
            // Arrange

            // Act
            var result = mathCalculator.Max(2, 1);

            // Assert
            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public void Max_SecondsArgumentIsGreater_ReturnsTheSecondArgument()
        {
            // Arrange

            // Act
            var result = mathCalculator.Max(1, 2);

            // Assert
            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public void Max_ArgumentsAreEqual_ReturnsTheSameArgument()
        {
            // Arrange

            // Act
            var result = mathCalculator.Max(1, 1);

            // Assert
            Assert.AreEqual(1, result);
        }
    }
}
