using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CalculatorManager calculator;
        private string actualEquation = "";
        private string viewEquation = "";
        private bool dotClicked = false;
        private bool resultCalculated = false;
        bool negativeClicked = false;

        public MainWindow()
        {
            InitializeComponent();
            calculator = new CalculatorManager();
        }

        private void NumberClicked(int number)
        {
            UpdateEquation(number.ToString());
        }

        private void OperatorClicked(string op)
        {
            dotClicked = false;
            negativeClicked = false;
            UpdateEquation(op);
        }

        private void UpdateEquation(string chr)
        {
            if (resultCalculated == true)
            {
                Clear();
                resultCalculated = false;
            }
            if (DisplayText.ActualWidth < 350)
            {
                actualEquation += chr;
                viewEquation += chr;
            }
        }

        private void Clear()
        {
            actualEquation = "";
            viewEquation = "";
            ResulText.Text = "";
            dotClicked = false;
            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            DisplayText.Text = viewEquation;
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            Clear();
        }

        private void Nine_Click(object sender, RoutedEventArgs e)
        {
            NumberClicked(9);
            UpdateDisplay();
        }

        private void Eight_Click(object sender, RoutedEventArgs e)
        {
            NumberClicked(8);
            UpdateDisplay();
        }

        private void Seven_Click(object sender, RoutedEventArgs e)
        {
            NumberClicked(7);
            UpdateDisplay();
        }

        private void Six_Click(object sender, RoutedEventArgs e)
        {
            NumberClicked(6);
            UpdateDisplay();
        }

        private void Five_Click(object sender, RoutedEventArgs e)
        {
            NumberClicked(5);
            UpdateDisplay();
        }

        private void Four_Click(object sender, RoutedEventArgs e)
        {
            NumberClicked(4);
            UpdateDisplay();
        }

        private void Three_Click(object sender, RoutedEventArgs e)
        {
            NumberClicked(3);
            UpdateDisplay();
        }

        private void Two_Click(object sender, RoutedEventArgs e)
        {
            NumberClicked(2);
            UpdateDisplay();
        }

        private void One_Click(object sender, RoutedEventArgs e)
        {
            NumberClicked(1);
            UpdateDisplay();
        }

        private void Zero_Click(object sender, RoutedEventArgs e)
        {
            NumberClicked(0);
            UpdateDisplay();
        }

        private void Negative_Click(object sender, RoutedEventArgs e)
        {
            if (!negativeClicked)
            {
                actualEquation += "N";
                viewEquation += "-";
                negativeClicked = true;
            }
            UpdateDisplay();
        }

        private void Point_Click(object sender, RoutedEventArgs e)
        {
            if (dotClicked == false)
            {
                actualEquation += ".";
                dotClicked = true;
            }
            UpdateDisplay();
        }

        private void Multiply_Click(object sender, RoutedEventArgs e)
        {
            OperatorClicked("*");
            UpdateDisplay();
        }

        private void Divide_Click(object sender, RoutedEventArgs e)
        {
            OperatorClicked("/");
            UpdateDisplay();
        }

        private void Minus_Click(object sender, RoutedEventArgs e)
        {
            OperatorClicked("-");
            UpdateDisplay();
        }

        private void Plus_Click(object sender, RoutedEventArgs e)
        {
            OperatorClicked("+");
            UpdateDisplay();
        }

        private void Equals_Click(object sender, RoutedEventArgs e)
        {
            float calculationResult = calculator.CalculateEquation(actualEquation);
            ResulText.Text = calculationResult.ToString();
            resultCalculated = true;
            UpdateDisplay();
        }

        private void RightParenthesis_Click(object sender, RoutedEventArgs e)
        {
            OperatorClicked(")");
            UpdateDisplay();
        }

        private void LeftParenthesis_Click(object sender, RoutedEventArgs e)
        {
            OperatorClicked("(");
            UpdateDisplay();
        }
    }
}
