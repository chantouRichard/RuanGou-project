using ModernWpf.Controls;
using System;
using System.Windows;
using WpfApp.Services;
using WpfApp.Views;

namespace WpfApp.Views
{
    public partial class MainWindow : Window
    {
        private readonly ApiService _apiService = new ApiService();

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var username = LoginUsername.Text;
            var password = LoginPassword.Password;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                LoginMessage.Text = "请输入用户名和密码";
                return;
            }

            //var result = await _apiService.Login(username, password);

            //if (result.Success)
            //{
            //    // Navigate to home window
            //    var homeWindow = new HomeWindow();
            //    homeWindow.Show();
            //    this.Close();
            //}
            //else
            //{
            //    LoginMessage.Text = result.Message;
            //}
            var homeWindow = new HomeWindow();
            homeWindow.Show();
            this.Close();
        }

        private async void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            var username = RegisterUsername.Text;
            var email = RegisterEmail.Text;
            var password = RegisterPassword.Password;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                RegisterMessage.Text = "请填好所有字段";
                return;
            }

            var result = await _apiService.Register(username, email, password);

            if (result.Success)
            {
                // Navigate to home window
                var homeWindow = new HomeWindow();
                homeWindow.Show();
                this.Close();
            }
            else
            {
                RegisterMessage.Text = result.Message;
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            // 获取当前窗口
            Window currentWindow = Window.GetWindow(this);

            // 关闭当前窗口
            currentWindow.Close();

            // 如果是主窗口，退出应用程序
            if (currentWindow == Application.Current.MainWindow)
            {
                Application.Current.Shutdown();
            }
        }
    }
}