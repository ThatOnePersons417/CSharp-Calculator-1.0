using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Calculator
{
    class CalculatorManager
    {
        private List<string> easyReadEquation;
        private Stack<char> operatorStack;

        //Creates the easyReadEquation and operatorStack
        public CalculatorManager()
        {
            ResetLists();
        }

        //Resets the easyReadEquation and operatorStack
        private void ResetLists()
        {
            operatorStack = new Stack<char>();
            easyReadEquation = new List<string>();
        }

        //Resets the easyReadEquation and operatorStack, simplifys the passed equation, and returns the solved equation
        public float CalculateEquation(string equation)
        {
            ResetLists();
            SimplifyEquation(equation);
            return SolveEquation();
        }

        //Solves the equation and returns the answer
        private float SolveEquation()
        {
            Stack<float> numbers = new Stack<float>();
            for (int i = 0; i < easyReadEquation.Count; i++)
            {
                try
                {
                    float number = float.Parse(easyReadEquation[i]);
                    numbers.Push(number);
                } catch (System.FormatException)
                {
                    numbers.Push(ApplyOperator(numbers.Pop(), numbers.Pop(), easyReadEquation[i]));
                }
            }
            return numbers.Pop();
        }

        //Simplifys the equation to a form that the computer does not have to consider order of operations
        private void SimplifyEquation(string equation)
        {
            for (int i = 0; i < equation.Length; i++)
            {
                //Number here
                if (char.IsDigit(equation[i]) || equation[i] == 'N')
                {
                    Tuple<string, int> number = GetNumber(equation.Replace('N', '-'), i);
                    easyReadEquation.Add(number.Item1);
                    i += number.Item2;
                }//Operator or decimal point here
                else
                {
                    if (equation[i] == ')')
                    {
                        while (operatorStack.Peek() != '(')
                        {
                            easyReadEquation.Add(operatorStack.Pop().ToString());
                        }
                        operatorStack.Pop();
                    }
                    else if (operatorStack.Count == 0 || GetOperatorHeiarchyLevel(equation[i]) < GetOperatorHeiarchyLevel(operatorStack.Peek()))
                    {
                        operatorStack.Push(equation[i]);
                    }
                    else
                    {
                        if (operatorStack.Peek() != '(')
                        {
                            easyReadEquation.Add(operatorStack.Pop().ToString());
                            operatorStack.Push(equation[i]);
                        }
                        else
                        {
                            operatorStack.Push(equation[i]);
                        }
                    }
                }
            }
            while (operatorStack.Count > 0)
            {
                easyReadEquation.Add(operatorStack.Pop().ToString());
            }
        }

        private float ApplyOperator(float num2, float num1, string op)
        {
            switch (op)
            {
                case "+":
                    return num1 + num2;
                case "-":
                    return num1 - num2;
                case "/":
                    return num1 / num2;
                case "*":
                    return num1 * num2;
                default:
                    return 0F;
            }
        }
        
        //Returns the heiarchy level of the passed operator
        private int GetOperatorHeiarchyLevel(char op)
        {
            switch (op)
            {
                case '(':
                    return 0;
                case ')':
                    return 0;
                case '*':
                    return 1;
                case '/':
                    return 1;
                case '+':
                    return 2;
                case '-':
                    return 2;
                default:
                    return 0;
            }
        }

        //Returns a tuple containing the number found at the current index and the length of the number
        private Tuple<string, int> GetNumber(string equation, int currentIndex, int _offset = 0)
        {
            string number = "";
            number += equation[currentIndex];
            if (currentIndex + 1 < equation.Length && (Char.IsDigit(equation[currentIndex + 1]) || equation[currentIndex + 1] == '.'))
            {
                Tuple<string, int> result = GetNumber(equation, currentIndex + 1, _offset);
                number += result.Item1;
                _offset = result.Item2 + 1;
            }
            Tuple<string, int> output = new Tuple<string, int>(number, _offset);
            return output;
        }
    }
}
