using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json;
using frontend.Services;
using frontend.Controls.Converters;

namespace frontend.Views.Pages
{
    public partial class DailyPage : Page
    {
        private readonly WeatherService _weatherService = new WeatherService();

        public DailyPage()
        {
            InitializeComponent();
            // 添加焦点事件处理
            CityTextBox.GotFocus += (s, e) => CityTextBox.SelectAll();
            CityTextBox.PreviewMouseLeftButtonDown += (s, e) =>
            {
                if (!CityTextBox.IsKeyboardFocusWithin)
                {
                    e.Handled = true;
                    CityTextBox.Focus();
                }
            };
        }

        private async void GetWeather_Click(object sender, RoutedEventArgs e)
        {
            var cityName = CityTextBox.Text.Trim();
            if (string.IsNullOrEmpty(cityName))
            {
                MessageBox.Show("请输入城市名称");
                return;
            }

            try
            {
                // 1. 获取城市编码
                var cityCodeJson = await _weatherService.GetCityCodeAsync(cityName);
                dynamic cityCodeData = JsonConvert.DeserializeObject(cityCodeJson);
                string cityCode = cityCodeData?.adcode;

                if (string.IsNullOrEmpty(cityCode))
                {
                    MessageBox.Show("城市不存在，请检查输入");
                    return;
                }

                // 2. 获取天气数据
                var weatherJson = await _weatherService.GetWeatherAsync(cityCode);
                dynamic weatherData = JsonConvert.DeserializeObject(weatherJson);

                // 3. 更新UI
                Dispatcher.Invoke(() =>
                {
                    CityText.Text = $"城市: {weatherData?.city}";
                    WeatherText.Text = $"天气: {weatherData?.weather}";
                    TempText.Text = $"温度: {weatherData?.temperature}°C";
                    WindText.Text = $"风力: {weatherData?.windpower}级";
                    HumidityText.Text = $"湿度: {weatherData?.humidity}%";
                    TimeText.Text = $"更新时间: {weatherData?.reporttime}";
                    WeatherPanel.Visibility = Visibility.Visible;
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"请求失败: {ex.Message}");
            }
        }
    }
}
