using System;
using System.Globalization;
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
            InitUnitConverters();
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

        private void OnBaseConvertClick(object sender, RoutedEventArgs e)
        {
            try
            {
                int fromBase = int.Parse(((ComboBoxItem)BaseFromCombo.SelectedItem).Tag.ToString());
                int toBase = int.Parse(((ComboBoxItem)BaseToCombo.SelectedItem).Tag.ToString());
                string input = BaseInput.Text.Trim();

                int value = Convert.ToInt32(input, fromBase);
                string result = Convert.ToString(value, toBase).ToUpper();

                BaseResult.Text = $"结果: {result}";
            }
            catch
            {
                BaseResult.Text = "输入有误，无法转换。";
            }
        }

        private void InitUnitConverters()
        {
            UnitTypeCombo.SelectedIndex = 0; // 默认选中长度
            PopulateUnitOptions(0);
        }

        private void OnUnitTypeChanged(object sender, SelectionChangedEventArgs e)
        {
            PopulateUnitOptions(UnitTypeCombo.SelectedIndex);
        }

        private void PopulateUnitOptions(int typeIndex)
        {
            UnitFromCombo.Items.Clear();
            UnitToCombo.Items.Clear();

            string[] options;
            switch (typeIndex)
            {
                case 0: // 长度
                    options = new[] { "米", "公里", "英尺" };
                    break;
                case 1: // 时间
                    options = new[] { "秒", "分钟", "小时" };
                    break;
                case 2: // 速度
                    options = new[] { "米/秒", "公里/小时" };
                    break;
                default:
                    options = new[] { "单位1", "单位2" };
                    break;
            }

            foreach (var item in options)
            {
                UnitFromCombo.Items.Add(new ComboBoxItem { Content = item });
                UnitToCombo.Items.Add(new ComboBoxItem { Content = item });
            }

            UnitFromCombo.SelectedIndex = 0;
            UnitToCombo.SelectedIndex = 1;
        }

        private void OnUnitConvertClick(object sender, RoutedEventArgs e)
        {
            try
            {
                double value = double.Parse(UnitInput.Text.Trim(), CultureInfo.InvariantCulture);
                string from = ((ComboBoxItem)UnitFromCombo.SelectedItem).Content.ToString();
                string to = ((ComboBoxItem)UnitToCombo.SelectedItem).Content.ToString();
                int typeIndex = UnitTypeCombo.SelectedIndex;

                double factor = 1;

                if (typeIndex == 0) // 长度
                {
                    factor = GetLengthFactor(from) / GetLengthFactor(to);
                }
                else if (typeIndex == 1) // 时间
                {
                    factor = GetTimeFactor(from) / GetTimeFactor(to);
                }
                else if (typeIndex == 2) // 速度
                {
                    if (from == "米/秒" && to == "公里/小时") factor = 3.6;
                    else if (from == "公里/小时" && to == "米/秒") factor = 1 / 3.6;
                    else factor = 1;
                }

                double result = value * factor;
                UnitResult.Text = $"结果: {result:F4}";
            }
            catch
            {
                UnitResult.Text = "输入有误，无法转换。";
            }
        }

        private double GetLengthFactor(string unit)
        {
            return unit switch
            {
                "米" => 1,
                "公里" => 1000,
                "英尺" => 0.3048,
                _ => 1
            };
        }

        private double GetTimeFactor(string unit)
        {
            return unit switch
            {
                "秒" => 1,
                "分钟" => 60,
                "小时" => 3600,
                _ => 1
            };
        }

        private void OnDdlCalculateClick(object sender, RoutedEventArgs e)
        {
            DateTime today = DateTime.Today;
            DateTime start = DdlStartDatePicker.SelectedDate ?? today;

            if (DdlEndDatePicker.SelectedDate is DateTime endDate)
            {
                int daysLeft = (endDate - start).Days;
                if (daysLeft < 0)
                {
                    DdlResult.Text = $"已过期 {Math.Abs(daysLeft)} 天！";
                }
                else if (daysLeft == 0)
                {
                    DdlResult.Text = "今天就是 DDL！加油完成任务！";
                }
                else
                {
                    DdlResult.Text = $"还有 {daysLeft} 天到 DDL，加油！";
                }
            }
            else
            {
                DdlResult.Text = "请选择一个结束日期。";
            }
        }

        private void OnBmiCalculateClick(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(BmiHeightInput.Text, out double heightCm) &&
                double.TryParse(BmiWeightInput.Text, out double weightKg))
            {
                double heightM = heightCm / 100;
                double bmi = weightKg / (heightM * heightM);
                string category = bmi switch
                {
                    < 18.5 => "偏瘦",
                    < 24 => "正常",
                    < 28 => "超重",
                    _ => "肥胖"
                };
                BmiResult.Text = $"BMI = {bmi:F1}，属于：{category}";
            }
            else
            {
                BmiResult.Text = "请输入有效的数字。";
            }
        }

    }
}