using NUnit.Framework;

namespace UnitTesting
{
    [TestFixture]
    public class Tests
    {
        private Calculator calculator;
        private RomanToNumerals roman;
        
        [SetUp]
        public void Initialize()
        {
            calculator = new Calculator();
            roman = new RomanToNumerals();
        }

        [Test]
        public void Should_Succeed_CalculatorAdd()
        {
            const int a = 5;
            const int b = 10;

            int result = calculator.Add(a, b);
            Assert.That(result, Is.EqualTo(15));
        }

        [Test]
        public void Should_Succeed_CalculatorSubtract()
        {
            const int a = 15;
            const int b = 7;

            int result = calculator.Subtract(a, b);
            Assert.That(result, Is.EqualTo(8));
        }

        [Test]
        public void Should_Succeed_CalculatorDivide()
        {
            const int a = 100;
            const int b = 5;

            int result = calculator.Divide(a, b);
            Assert.That(result, Is.EqualTo(20));
        }

        [Test]
        public void Should_Succeed_CalculatorMultiply()
        {
            const int a = 5;
            const int b = 5;

            int result = calculator.Multiply(a, b);
            Assert.That(result, Is.EqualTo(25));
        }

        [Test]
        public void Should_Succeed_RomanToNumerals()
        {
            // Arrange
            const string r = "XCIV"; 
            const int expected = 94;

            // Act
            int result = roman.Convert(r);
            
            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }
        
        static readonly object[] TestDataAdd =
        {
            new object[] { 2, 3, 5 },
            new object[] { -1, 1, 0 },
            new object[] { 0, 0, 0 },
            // Add more test cases as needed
        };
        
        // Parameterised Tests
        /* [TestCase(5,10,15)]
        [TestCase(10,10,20)]
        [TestCase(200, 800, 1000)]
        [TestCase(300, 800, 1000)] // Supposed to fail
        */
        [TestCaseSource(nameof(TestDataAdd))]
        public void Should_Succeed_Parameterised_CalculatorAdd(int a, int b, int expected)
        {
            int result = calculator.Add(a, b);
            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCase("XCIV", 94)]
        public void Should_Succeed_Parameterised_RomanToNumerals(string str, int expected)
        {
            int result = roman.Convert(str);
            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCase(10, 2, 11)]
        public void Should_Fail_Parameterised_CalculatorAdd(int a, int b, int expected)
        {
            int result = calculator.Add(a, b);
            Assert.That(result, !Is.EqualTo(expected));
        }
    }
}
