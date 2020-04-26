using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestAT_CSharp
{
    [TestClass]
    public class CalculatorTestClass
    {


        [TestMethod]
        public void TestCalculatorSquareRoot()
        {
            CalculatorApp calculator = new CalculatorApp();

            int RandomNumber = new Random().Next(1, 101); // От 1 до 101, т.к. нижний предел включен, а верхний - нет

            calculator.ExpressionEntry(RandomNumber.ToString());

            double exceptedSquareRoot = Math.Sqrt(RandomNumber);
            double resultSquareRoot = calculator.GetSquareRoot();
            Assert.IsTrue(DoubleEqualTo(resultSquareRoot, exceptedSquareRoot, doubleEpsilon));


            double resultSquare = calculator.GetSquare();
            Assert.IsTrue(DoubleEqualTo(resultSquare, RandomNumber, doubleEpsilon));

            calculator.Close();
        }


        #region DoubleEquals
        private const double doubleEpsilon = 0.000001;

        private bool DoubleEqualTo(double value1, double value2, double epsilon)
        {
            return Math.Abs(value1 - value2) < epsilon;
        }
        #endregion

    }
}
