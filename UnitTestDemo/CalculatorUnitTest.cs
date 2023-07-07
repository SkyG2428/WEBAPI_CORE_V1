using Microsoft.VisualStudio.TestTools.UnitTesting;




namespace UnitTestDemo
{
    public class Calculator
    {
        public int Add(int x, int y)
        {
            // x = x + 1;
            return x + y;
        }
    }


    [TestClass]
    public class CalculatorUnitTest
    {
        [TestMethod]
        public void TestAdd()
        {
            // A: Arragement
            Calculator calc = new Calculator(); // OBJ
            int num1 = 3, num2 = 4;

            // A: Action 
            var result = calc.Add(num1, num2);

            // A: Assertion
            Assert.AreEqual(num1 + num2, result);
        }
    }
}
