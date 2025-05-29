using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json;
using frontend.Services;
using frontend.Controls.Converters;
using System.IO;

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
            CityTextBox.Text = "武汉";
            GetWeather_Click(null,null);
        }

        private void WeatherVideoPlayer_MediaEnded(object sender, RoutedEventArgs e)
        {
            // 使用 Dispatcher 确保播放操作在下一帧执行
            Dispatcher.BeginInvoke(new Action(() =>
            {
                WeatherVideoPlayer.Position = TimeSpan.Zero;
                WeatherVideoPlayer.LoadedBehavior = MediaState.Play;
            }), System.Windows.Threading.DispatcherPriority.Render);
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

                //播放视频
                // 获取天气状态
                string weather = weatherData?.weather?.ToString().ToLower();

                // 根据天气设置视频路径
                string videoPath = "Assets/Video/";

                switch (weather)
                {
                    case "晴":
                        videoPath += "sun.mp4";
                        break;
                    case "阴":
                        videoPath += "yintian.mp4";
                        break;
                    case "多云":
                        videoPath += "cloudy.mp4";
                        break;
                    case "雨":
                    case "小雨":
                    case "中雨":
                    case "大雨":
                        videoPath += "rain.mp4";
                        break;
                    // 可以继续添加其他天气类型
                    default:
                        //VideoCard.Visibility = Visibility.Visible;
                        return;
                }

                // 播放视频
                Dispatcher.Invoke(() =>
                {
                    if (File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, videoPath)))
                    {
                        Uri videoUri = new Uri(videoPath, UriKind.RelativeOrAbsolute);
                        WeatherVideoPlayer.Source = videoUri;
                        //VideoCard.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        //VideoCard.Visibility = Visibility.Visible;
                    }
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"请求失败: {ex.Message}");
            }
        }
    }
}
