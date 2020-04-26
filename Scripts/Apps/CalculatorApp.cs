using System;
using System.Windows.Automation;
using Winium.Cruciatus.Core;
using Winium.Cruciatus.Elements;
using Winium.Cruciatus;
using WindowsInput;

namespace TestAT_CSharp
{
    public class CalculatorApp
    {
        private readonly Application calculator = new Application("C:/windows/system32/calc.exe");

        #region Constructor
        public CalculatorApp()
        {
            calculator.Start();

            inputSimulator = new InputSimulator();
            calculatorWindow = CruciatusFactory.Root.FindElement(By.Name("Calculator").AndType(ControlType.Window));
            calculatorWindow.SetFocus(); // Необходимо установить фокус. Империческим путем было выведенно, что это способствует предотвращению багов.


            calculatorResults = calculatorWindow.FindElement(By.Uid("CalculatorResults"));

            // Кнопка извлечения квадратного корня
            bttnSquareRoot = calculatorWindow.FindElement(By.Name("Square root").AndType(ControlType.Button));

            // Кнопка возведения в квадрат
            bttnSquare = calculatorWindow.FindElement(By.Name("Square").AndType(ControlType.Button));
        }
        #endregion


        private readonly InputSimulator inputSimulator;

        private readonly CruciatusElement calculatorWindow;
        private readonly CruciatusElement calculatorResults;
        private readonly CruciatusElement bttnSquareRoot; // Кнопка извлечения квадратного корня
        private readonly CruciatusElement bttnSquare; // Кнопка возведения в квадрат

        // Из калькулятора нельзя так просто получить результат - результат лежит в элементе вместе с его параметрами
        private readonly string subStringInDisplayToDelite = "type: ControlType.Text, uid: CalculatorResults, name: Display is ";


        public void ExpressionEntry(string text)
        {
            calculatorWindow.SetFocus();
            inputSimulator.Keyboard.TextEntry(text);
        }

        public double GetSquareRoot()
        {
            bttnSquareRoot.Click();

            return GetCalculatorResult();
        }


        public double GetSquare()
        {
            bttnSquare.Click();

            return GetCalculatorResult();
        }


        public double GetCalculatorResult()
        {
            string CalculatorDisplayResult = calculatorResults.ToString();
            string resultAsString = SubStringDelite(CalculatorDisplayResult, subStringInDisplayToDelite);
            resultAsString = resultAsString.Replace(" ", "");

            //Работает без замены "," на "."

            return Convert.ToDouble(resultAsString);
        }


        private string SubStringDelite(string str, string subString)
        {
            return str.Replace(subString, "");
        }


        public void Close()
        {
            calculator.Close();
        }
    }
}
