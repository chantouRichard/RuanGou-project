using System;
using System.Windows;
using System.Windows.Controls;

namespace frontend.Views.Pages
{
    public partial class CalculatorPage : UserControl
    {
        private string currentInput = "0";
        private string previousInput;
        private string selectedOperator;
        private bool newInput = true;

        public CalculatorPage()
        {
            InitializeComponent();
        }

        private void UpdateDisplay()
        {
            DisplayText.Text = currentInput;
        }

        private void OnNumberClick(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var number = button.Content.ToString();

            if (currentInput == "0" || newInput)
            {
                currentInput = number;
                newInput = false;
            }
            else
            {
                currentInput += number;
            }

            UpdateDisplay();
        }

        private void OnDecimalClick(object sender, RoutedEventArgs e)
        {
            if (!currentInput.Contains("."))
            {
                currentInput += ".";
                UpdateDisplay();
            }
        }

        private void OnOperatorClick(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            selectedOperator = button.Tag.ToString();
            previousInput = currentInput;
            newInput = true;
        }

        private void OnEqualsClick(object sender, RoutedEventArgs e)
        {
            if (previousInput == null || selectedOperator == null) return;

            try
            {
                var result = Calculate(
                    double.Parse(previousInput),
                    double.Parse(currentInput),
                    selectedOperator);

                currentInput = result.ToString();
                previousInput = null;
                selectedOperator = null;
                newInput = true;
                UpdateDisplay();
            }
            catch (Exception ex)
            {
                currentInput = "Error";
                UpdateDisplay();
            }
        }

        private double Calculate(double num1, double num2, string op)
        {
            return op switch
            {
                "+" => num1 + num2,
                "-" => num1 - num2,
                "*" => num1 * num2,
                "/" => num1 / num2,
                _ => throw new InvalidOperationException("Unknown operator")
            };
        }

        private void OnClearClick(object sender, RoutedEventArgs e)
        {
            currentInput = "0";
            previousInput = null;
            selectedOperator = null;
            UpdateDisplay();
        }

        private void OnSignClick(object sender, RoutedEventArgs e)
        {
            if (currentInput != "0")
            {
                if (currentInput.StartsWith("-"))
                    currentInput = currentInput.Substring(1);
                else
                    currentInput = "-" + currentInput;

                UpdateDisplay();
            }
        }

        private void OnPercentClick(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(currentInput, out var number))
            {
                currentInput = (number / 100).ToString();
                UpdateDisplay();
            }
        }
    }
}