using frontend.Models;
using frontend.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace frontend.Views.Pages
{
    public partial class CloudRemindPage : Page
    {
        #region 番茄钟相关

        private DispatcherTimer _pomodoroTimer;
        private TimeSpan _pomodoroTimeLeft;
        private int _workMinutes = 25;
        private int _restMinutes = 5;
        private bool _pomodoroIsWorking = true; // true=工作，false=休息
        private bool _pomodoroIsRunning = false;

        #endregion

        #region 倒计时相关

        private DispatcherTimer _countdownTimer;
        private int _countdownSecondsLeft;
        private bool _countdownIsRunning = false;

        #endregion

        #region 倒数日相关

        private ObservableCollection<CountdownDay> countdownDays = new ObservableCollection<CountdownDay>();
        private readonly HttpClient httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:5166/") };


        #endregion


        public CloudRemindPage()
        {
            InitializeComponent();

            #region 番茄钟初始化
            _pomodoroTimer = new DispatcherTimer();
            _pomodoroTimer.Interval = TimeSpan.FromSeconds(1);
            _pomodoroTimer.Tick += PomodoroTimer_Tick;
            ResetPomodoro();
            #endregion

            #region 倒计时初始化
            _countdownTimer = new DispatcherTimer();
            _countdownTimer.Interval = TimeSpan.FromSeconds(1);
            _countdownTimer.Tick += CountdownTimer_Tick;
            ResetCountdown();
            #endregion

            #region 倒数日初始化
            CountdownListBox.ItemsSource = countdownDays;
            DatePicker.SelectedDate = DateTime.Today;
            Loaded += CloudRemindPage_Loaded;
            #endregion

        }

        #region 番茄钟逻辑

        private void PomodoroTimer_Tick(object sender, EventArgs e)
        {
            if (_pomodoroTimeLeft.TotalSeconds > 0)
            {
                _pomodoroTimeLeft = _pomodoroTimeLeft.Subtract(TimeSpan.FromSeconds(1));
                UpdatePomodoroDisplay();
            }
            else
            {
                // 状态切换
                _pomodoroIsWorking = !_pomodoroIsWorking;

                if (_pomodoroIsWorking)
                {
                    PomodoroStatusTextBlock.Text = "状态：工作时间";
                    _pomodoroTimeLeft = TimeSpan.FromMinutes(_workMinutes);
                    PomodoroTimeTextBlock.Foreground = System.Windows.Media.Brushes.Red;
                }
                else
                {
                    PomodoroStatusTextBlock.Text = "状态：休息时间";
                    _pomodoroTimeLeft = TimeSpan.FromMinutes(_restMinutes);
                    PomodoroTimeTextBlock.Foreground = System.Windows.Media.Brushes.Green;
                }
                UpdatePomodoroDisplay();
            }
        }

        private void UpdatePomodoroDisplay()
        {
            PomodoroTimeTextBlock.Text = _pomodoroTimeLeft.ToString(@"mm\:ss");
        }

        private void ResetPomodoro()
        {
            if (!int.TryParse(WorkTimeTextBox.Text, out _workMinutes) || _workMinutes <= 0)
                _workMinutes = 25;
            if (!int.TryParse(RestTimeTextBox.Text, out _restMinutes) || _restMinutes <= 0)
                _restMinutes = 5;

            _pomodoroIsWorking = true;
            _pomodoroTimeLeft = TimeSpan.FromMinutes(_workMinutes);
            PomodoroStatusTextBlock.Text = "状态：等待开始";
            PomodoroTimeTextBlock.Text = _pomodoroTimeLeft.ToString(@"mm\:ss");
            PomodoroTimeTextBlock.Foreground = System.Windows.Media.Brushes.Red;

            _pomodoroTimer.Stop();
            _pomodoroIsRunning = false;
            PomodoroStartButton.IsEnabled = true;
            PomodoroPauseButton.IsEnabled = false;
        }

        private void PomodoroStartButton_Click(object sender, RoutedEventArgs e)
        {
            if (!_pomodoroIsRunning)
            {
                _pomodoroTimer.Start();
                _pomodoroIsRunning = true;
                PomodoroStatusTextBlock.Text = _pomodoroIsWorking ? "状态：工作时间" : "状态：休息时间";
                PomodoroStartButton.IsEnabled = false;
                PomodoroPauseButton.IsEnabled = true;
            }
        }

        private void PomodoroPauseButton_Click(object sender, RoutedEventArgs e)
        {
            if (_pomodoroIsRunning)
            {
                _pomodoroTimer.Stop();
                _pomodoroIsRunning = false;
                PomodoroStatusTextBlock.Text = "状态：已暂停";
                PomodoroStartButton.IsEnabled = true;
                PomodoroPauseButton.IsEnabled = false;
            }
        }

        private void PomodoroResetButton_Click(object sender, RoutedEventArgs e)
        {
            ResetPomodoro();
        }

        #endregion

        #region 倒计时逻辑

        private void CountdownTimer_Tick(object sender, EventArgs e)
        {
            if (_countdownSecondsLeft > 0)
            {
                _countdownSecondsLeft--;
                UpdateCountdownDisplay();
            }
            else
            {
                _countdownTimer.Stop();
                _countdownIsRunning = false;
                CountdownStartButton.IsEnabled = true;
                CountdownPauseButton.IsEnabled = false;
                CountdownTimeTextBlock.Text = "倒计时结束！";
            }
        }

        private void UpdateCountdownDisplay()
        {
            CountdownTimeTextBlock.Text = $"剩余：{_countdownSecondsLeft}s";
        }

        private void ResetCountdown()
        {
            _countdownTimer.Stop();
            _countdownIsRunning = false;
            CountdownTimeTextBlock.Text = "剩余：0s";
            CountdownStartButton.IsEnabled = true;
            CountdownPauseButton.IsEnabled = false;
        }

        private void CountdownStartButton_Click(object sender, RoutedEventArgs e)
        {
            if (!_countdownIsRunning)
            {
                if (int.TryParse(CountdownInputTextBox.Text, out int seconds) && seconds > 0)
                {
                    _countdownSecondsLeft = seconds;
                    UpdateCountdownDisplay();
                    _countdownTimer.Start();
                    _countdownIsRunning = true;
                    CountdownStartButton.IsEnabled = false;
                    CountdownPauseButton.IsEnabled = true;
                }
                else
                {
                    MessageBox.Show("请输入有效的秒数！");
                }
            }
        }

        private void CountdownPauseButton_Click(object sender, RoutedEventArgs e)
        {
            if (_countdownIsRunning)
            {
                _countdownTimer.Stop();
                _countdownIsRunning = false;
                CountdownStartButton.IsEnabled = true;
                CountdownPauseButton.IsEnabled = false;
            }
        }

        private void CountdownResetButton_Click(object sender, RoutedEventArgs e)
        {
            ResetCountdown();
        }

        #endregion

        #region 倒数日逻辑
        private async void CloudRemindPage_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadCountdownDays();
        }

        private async Task LoadCountdownDays()
        {
            try
            {
                var list = await httpClient.GetFromJsonAsync<CountdownDay[]>("api/CountdownDays");
                countdownDays.Clear();
                if (list != null)
                {
                    foreach (var item in list)
                    {
                        countdownDays.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载倒数日失败：" + ex.Message);
            }
        }

        private async void AddButton_Click(object sender, RoutedEventArgs e)
        {
            string name = NameTextBox.Text.Trim();
            DateTime? targetDate = DatePicker.SelectedDate;

            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("请输入纪念日名称");
                return;
            }
            if (targetDate == null)
            {
                MessageBox.Show("请选择日期");
                return;
            }

            var newCountdown = new CountdownDay
            {
                Name = name,
                TargetDate = targetDate.Value
            };

            try
            {
                var response = await httpClient.PostAsJsonAsync("api/CountdownDays", newCountdown);
                if (response.IsSuccessStatusCode)
                {
                    var created = await response.Content.ReadFromJsonAsync<CountdownDay>();
                    if (created != null)
                    {
                        countdownDays.Add(created);
                        NameTextBox.Text = "";
                        DatePicker.SelectedDate = DateTime.Today;
                    }
                }
                else
                {
                    MessageBox.Show("添加倒数日失败：" + response.ReasonPhrase);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("添加倒数日异常：" + ex.Message);
            }
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is CountdownDay item)
            {
                var confirm = MessageBox.Show($"确定要删除 \"{item.Name}\" 吗？", "确认删除", MessageBoxButton.YesNo);
                if (confirm != MessageBoxResult.Yes) return;

                try
                {
                    var response = await httpClient.DeleteAsync($"api/CountdownDays/{item.Id}");
                    if (response.IsSuccessStatusCode)
                    {
                        countdownDays.Remove(item);
                    }
                    else
                    {
                        MessageBox.Show("删除倒数日失败：" + response.ReasonPhrase);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("删除倒数日异常：" + ex.Message);
                }
            }
        }

        private async void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is CountdownDay item)
            {
                try
                {
                    var response = await httpClient.PutAsJsonAsync($"api/CountdownDays/{item.Id}", item);
                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("更新成功");
                        // 因为我们直接绑定 Name 和 TargetDate，界面会自动更新
                    }
                    else
                    {
                        MessageBox.Show("更新失败：" + response.ReasonPhrase);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("更新异常：" + ex.Message);
                }
            }
        }




        #endregion

    }
}

