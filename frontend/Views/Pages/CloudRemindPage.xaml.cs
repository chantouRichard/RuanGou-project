using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using frontend.Models;

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

        public class CountdownDayItem
        {
            public string Name { get; set; }
            public DateTime TargetDate { get; set; }
            public int DaysLeft => (TargetDate - DateTime.Now.Date).Days;
            public override string ToString() => $"{Name} - 剩余 {DaysLeft} 天";
        }

        private ObservableCollection<CountdownDayItem> _countdownDays = new ObservableCollection<CountdownDayItem>();

        #endregion

        #region 书籍推荐相关

        private readonly HttpClient _httpClient = new HttpClient { BaseAddress = new Uri("https://localhost:5166/") };

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
            LoadCountdownDays();
            CountdownDaysListView.ItemsSource = _countdownDays;
            #endregion

            #region 书籍推荐初始化
            
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

        private void LoadCountdownDays()
        {
            // 示例数据，实际项目中可从数据库或配置文件加载
            _countdownDays.Clear();
            _countdownDays.Add(new CountdownDayItem { Name = "毕业", TargetDate = new DateTime(DateTime.Now.Year, 7, 1) });
            _countdownDays.Add(new CountdownDayItem { Name = "项目截止", TargetDate = new DateTime(DateTime.Now.Year, 12, 31) });
            _countdownDays.Add(new CountdownDayItem { Name = "生日", TargetDate = new DateTime(DateTime.Now.Year, 10, 15) });

            RefreshCountdownDays();
        }

        // 刷新倒数日剩余天数显示，建议定时更新
        private DispatcherTimer _countdownDaysTimer;

        private void StartCountdownDaysTimer()
        {
            if (_countdownDaysTimer == null)
            {
                _countdownDaysTimer = new DispatcherTimer();
                _countdownDaysTimer.Interval = TimeSpan.FromHours(1); // 每小时刷新一次
                _countdownDaysTimer.Tick += (s, e) => RefreshCountdownDays();
                _countdownDaysTimer.Start();
            }
        }

        private void RefreshCountdownDays()
        {
            // 触发界面刷新（因为DaysLeft是计算属性）
            // 这里调用CollectionView刷新
            var view = System.Windows.Data.CollectionViewSource.GetDefaultView(_countdownDays);
            if (view != null)
            {
                view.Refresh();
            }
        }
        #endregion

        #region 书籍逻辑
        private async void RecommendButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var book = await _httpClient.GetFromJsonAsync<BookItem>("api/books/recommend");
                if (book != null)
                {

                    BooksListView.ItemsSource = new List<BookItem> { book };
                }
                else
                    MessageBox.Show("没有推荐的书籍！");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"请求失败: {ex.Message}");
            }
        }

        private async void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var query = SearchBox.Text;
                if (string.IsNullOrWhiteSpace(query))
                {
                    MessageBox.Show("请输入搜索关键词！");
                    return;
                }

                var books = await _httpClient.GetFromJsonAsync<List<BookItem>>($"api/books/search?query={Uri.EscapeDataString(query)}");
                BooksListView.ItemsSource = books ?? new List<BookItem>();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"请求失败: {ex.Message}");
            }
        }
    }
    #endregion
}

